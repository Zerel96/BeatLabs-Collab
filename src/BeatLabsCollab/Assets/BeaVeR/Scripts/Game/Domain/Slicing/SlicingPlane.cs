using UnityEngine;
using EzySlice;

namespace BeaVeR.Game.Domain.Slicing
{
  // TODO: 2022-12-31 - Immortal - HI - I don't think we need this at all; or at least rename and then rename SlicingPlaneDescriptor
  /// <remarks>
  /// Based on PlaneUsageExample from EzySliceExamples.
  /// </remarks>
  public class SlicingPlane : MonoBehaviour
  {
    private void Update()
    {
      // do nothing
    }

    /// <summary>
    /// This function will slice the provided object by the plane defined in this
    /// GameObject. We use the GameObject this script is attached to define the position
    /// and direction of our cutting Plane. Results are then returned to the user.
    /// </summary>
    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
      // slice the provided object using the transforms of this object
      return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }

#if UNITY_EDITOR

    /// <summary>
    /// This is for Visual debugging purposes in the editor.
    /// </summary>
    public void OnDrawGizmos()
    {
      var cuttingPlane = new EzySlice.Plane();

      // the plane will be set to the same coordinates as the object that this
      // script is attached to
      // NOTE -> Debug Gizmo drawing only works if we pass the transform

      cuttingPlane.Compute(transform);

      // draw gizmos for the plane
      // NOTE -> Debug Gizmo drawing is ONLY available in editor mode. Do NOT try
      // to run this in the final build or you'll get crashes (most likey)

      cuttingPlane.OnDebugDraw();
    }

#endif
  }
}
