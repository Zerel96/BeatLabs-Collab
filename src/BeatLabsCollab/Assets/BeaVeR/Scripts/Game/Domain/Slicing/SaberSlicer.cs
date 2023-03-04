using BeatLabs.Utils;
using EzySlice;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BeaVeR.Game.Domain.Slicing
{
  /// <remarks>
  /// Based on PlaneUsageExampleEditor from EzySliceExamples.
  /// </remarks>
  public class SaberSlicer : MonoBehaviour
  {
    private const float _RotationSlerpSpeed = 25.0f;

    private const bool _SliceRecursively = true;

    private const float _SliceForceMagnitudeMultiplier = 1.0f;
    private const float _MinSliceForceMagnitude = 0.5f;
    private const float _MaxSliceForceMagnitude = 5.0f;

    private const float _SplitForceMagnitudeMultiplier = _SliceForceMagnitudeMultiplier / 4.0f;
    private const float _MinSplitForceMagnitude = _MinSliceForceMagnitude / 4.0f;
    private const float _MaxSplitForceMagnitude = _MaxSliceForceMagnitude / 4.0f;

    private const float _TorqueMultiplier = 2.0f;

    private const float _VelocityVectorScaleMultiplierForVisualization = 2.0f;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value null

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private GameObject _velocityVector;

    [SerializeField]
    private SlicingPlane _slicingPlane;

#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value null

    private void Start()
    {
      CommonUtils.EnsureNotNull(_rigidbody, nameof(_rigidbody));
      CommonUtils.EnsureNotNull(_velocityVector, nameof(_velocityVector));
      CommonUtils.EnsureNotNull(_slicingPlane, nameof(_slicingPlane));
    }

    private void FixedUpdate()
    {
      Vector3 velocity = _rigidbody.velocity;

      if (velocity == Vector3.zero)
      {
        _velocityVector.transform.localScale =
          new Vector3(
            1.0f,
            1.0f,
            0.0f
          );

        return;
      }

      Quaternion newRotation = Quaternion.LookRotation(velocity);
      float slerpRatio = _RotationSlerpSpeed * velocity.magnitude * Time.fixedDeltaTime;

      Quaternion newSmoothRotation =
        Quaternion.Slerp(
          a: _velocityVector.transform.rotation,
          b: newRotation,
          t: slerpRatio
        );

      _velocityVector.transform.rotation = newSmoothRotation;

      _velocityVector.transform.localScale =
        new Vector3(
          1.0f,
          1.0f,
          velocity.magnitude * _VelocityVectorScaleMultiplierForVisualization
        );

      _slicingPlane.transform.localEulerAngles =
        new Vector3(
          _slicingPlane.transform.localEulerAngles.x,
          _velocityVector.transform.localEulerAngles.y,
          _slicingPlane.transform.localEulerAngles.z);
    }

    public bool SliceBlock(Block block)
    {
      GameObject objectToSlice =
        block.gameObject.GetChildren()
          .FirstOrDefault(go => go.HasComponent<MeshFilter>() && go.HasComponent<MeshRenderer>());

      if (objectToSlice == null)
      {
        Debug.LogWarning("No object with MeshFilter and MeshRenderer to slice.");

        return false;
      }

      Material crossSectionMaterial = objectToSlice.GetComponent<MeshRenderer>().material;

      GameObject lowerHullObject;
      GameObject upperHullObject;

      if (_SliceRecursively)
      {
        (lowerHullObject, upperHullObject) =
          SliceObjectRecursive(
            SlicingPlaneDescriptor.FromTransform(_slicingPlane.transform),
            objectToSlice,
            crossSectionMaterial
          );

        if (lowerHullObject == null || upperHullObject == null)
        {
          // if we couldn't slice through the _slicingPlane, let's slice through the block's center

          (lowerHullObject, upperHullObject) =
            SliceObjectRecursive(
              block.GetCenterSlicingPlane(),
              objectToSlice,
              crossSectionMaterial
            );
        }
      }
      else
      {
#pragma warning disable CS0162 // Unreachable code detected
        (lowerHullObject, upperHullObject) =
          SliceObjectNonRecursive(
            SlicingPlaneDescriptor.FromTransform(_slicingPlane.transform),
            objectToSlice,
            crossSectionMaterial
          );

        if (lowerHullObject == null || upperHullObject == null)
        {
          // if we couldn't slice through the _slicingPlane, let's slice through the block's center

          (lowerHullObject, upperHullObject) =
            SliceObjectNonRecursive(
              block.GetCenterSlicingPlane(),
              objectToSlice,
              crossSectionMaterial
            );
        }
#pragma warning restore CS0162 // Unreachable code detected
      }

      if (lowerHullObject == null || upperHullObject == null)
      {
        return false;
      }

      if (lowerHullObject != null)
      {
        lowerHullObject.transform.SetParent(objectToSlice.transform.parent, worldPositionStays: false);

        Rigidbody lowerHullRigidbody = lowerHullObject.AddComponent<Rigidbody>();

        InitializeHullRigidbody(block, lowerHullRigidbody, isLowerHull: true);
      }

      if (upperHullObject != null)
      {
        upperHullObject.transform.SetParent(objectToSlice.transform.parent, worldPositionStays: false);

        Rigidbody upperHullRigidbody = upperHullObject.AddComponent<Rigidbody>();

        InitializeHullRigidbody(block, upperHullRigidbody, isLowerHull: false);
      }

      objectToSlice.SetActive(false);

      return true;
    }

    private static (GameObject, GameObject) SliceObjectNonRecursive(SlicingPlaneDescriptor slicingPlaneDescriptor, GameObject objectToSlice, Material crossSectionMaterial)
    {
      SlicedHull slicedHull =
        objectToSlice.Slice(
          slicingPlaneDescriptor.Position,
          slicingPlaneDescriptor.Normal,
          crossSectionMaterial
        );

      if (slicedHull == null)
      {
        return (null, null);
      }

      GameObject lowerHullObject = slicedHull.CreateLowerHull(objectToSlice, crossSectionMaterial);
      GameObject upperHullObject = slicedHull.CreateUpperHull(objectToSlice, crossSectionMaterial);

      return (lowerHullObject, upperHullObject);
    }

    private static (GameObject, GameObject) SliceObjectRecursive(SlicingPlaneDescriptor slicingPlaneDescriptor, GameObject objectToSlice, Material crossSectionMaterial)
    {
      // === FUNCS

      static void attachObjectToRandomHull(GameObject gameObject, GameObject lowerHull = null, GameObject upperHull = null)
      {
        // TODO: 2022-12-30 - Immortal - HI - we sliced the parent but the slicing plane didn't touch the child
        // we could attach the child to either lower or upper parent hull (random?)
        // or move the slicing plane to the center of the child object and slice it that way
        // or attach to the bigger hull
        // or attach to lower or upper hull depending on the cutting direction
        // or attach to the hull the object is colliding with (if any)
        // phew...

        GameObject objectToAttachTo = null;

        if (lowerHull != null && upperHull != null)
        {
          bool attachToLower = Random.Range(-1, 1) < 0;

          objectToAttachTo = (attachToLower ? lowerHull : upperHull);
        }
        else
        {
          objectToAttachTo = (lowerHull != null ? lowerHull : upperHull);
        }

        if (objectToAttachTo != null)
        {
          gameObject.transform.SetParent(objectToAttachTo.transform, worldPositionStays: false);
        }
      };

      // === BODY

      if (!objectToSlice.activeInHierarchy)
      {
        return (null, null);
      }

      if (!objectToSlice.HasComponent<MeshFilter>() || !objectToSlice.HasComponent<MeshRenderer>())
      {
        return (null, null);
      }

      // slice the object
      SlicedHull finalHull =
        objectToSlice.Slice(
          slicingPlaneDescriptor.Position,
          slicingPlaneDescriptor.Normal,
          crossSectionMaterial
        );

      if (finalHull == null)
      {
        return (null, null);
      }

      GameObject lowerParent = finalHull.CreateLowerHull(objectToSlice, crossSectionMaterial);
      GameObject upperParent = finalHull.CreateUpperHull(objectToSlice, crossSectionMaterial);

      // NOTE: we need to get all the children first because we'll be modifying the object we iterate on
      List<GameObject> children = objectToSlice.GetChildren().ToList();

      foreach (GameObject child in children)
      {
        if (child == null || child.transform == null)
        {
          continue;
        }

        // slice the child object recursively
        (GameObject lowerChild, GameObject upperChild) =
          SliceObjectRecursive(slicingPlaneDescriptor, child, crossSectionMaterial);

        if (lowerChild != null || upperChild != null)
        {
          // attach the lower hull of the child if available
          if (lowerChild != null && lowerParent != null)
          {
            lowerChild.transform.SetParent(lowerParent.transform, false);
          }

          // attach the upper hull of this child if available
          if (upperChild != null && upperParent != null)
          {
            upperChild.transform.SetParent(upperParent.transform, false);
          }
        }
        else
        {
          // TODO: 2022-12-31 - Immortal - HI - disappearing arrows/dots
          //MeshRenderer mrp = child.transform.parent.GetComponent<MeshRenderer>();
          //MeshRenderer mrc = child.GetComponent<MeshRenderer>();

          //Debug.Log("MRP: " + mrp.enabled);
          //Debug.Log("MRC: " + mrc.enabled);

          attachObjectToRandomHull(child, lowerParent, upperParent);
        }
      }

      return (lowerParent, upperParent);
    }

    private void InitializeHullRigidbody(Block block, Rigidbody rigidbody, bool isLowerHull)
    {
      float velocityMagnitude =
        _velocityVector.transform.localScale.z / _VelocityVectorScaleMultiplierForVisualization;

      // === FUNCS

      void applyImpulseForce(Vector3 forceDirection, float forceMagnitudeMultiplier, float minForceMagnitude, float maxForceMagnitude)
      {
        float forceMagnitude =
          forceMagnitudeMultiplier * velocityMagnitude;

        if (!Mathf.Approximately(forceMagnitude, 0.0f))
        {
          forceMagnitude =
            Mathf.Min(
              Mathf.Max(forceMagnitude, minForceMagnitude),
              maxForceMagnitude
            );
        }
        else
        {
          forceDirection = Quaternion.AngleAxis(-180.0f, Vector3.forward) * block.transform.up;

          forceMagnitude = (minForceMagnitude + maxForceMagnitude) / 2.0f;
        }

        Vector3 forceVector =
          forceMagnitudeMultiplier * forceMagnitude * forceDirection;

        rigidbody.AddForce(forceVector, ForceMode.Impulse);
      };

      // === BODY

      Vector3 sliceForceDirection =
        _velocityVector.transform.rotation * Vector3.forward;

      applyImpulseForce(
        sliceForceDirection,
        _SliceForceMagnitudeMultiplier,
        _MinSliceForceMagnitude,
        _MaxSliceForceMagnitude
      );

      Vector3 slicingPlaneNormal = _slicingPlane.transform.up;
      Vector3 splitForceDirection = (isLowerHull ? -1 : 1) * slicingPlaneNormal;

      applyImpulseForce(
        splitForceDirection,
        _SplitForceMagnitudeMultiplier,
        _MinSplitForceMagnitude,
        _MaxSplitForceMagnitude
      );

      // TODO: 2022-12-30 - Immortal - ME - we could use velocity magnitude here; and clamp
      rigidbody.AddTorque(_TorqueMultiplier * Random.onUnitSphere, ForceMode.Impulse);
    }
  }
}
