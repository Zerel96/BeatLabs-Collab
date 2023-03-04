using System;
using BeatLabs.Utils;
using BeaVeR.Core.Exceptions;
using BeaVeR.Game.Domain.Slicing;
using ImmSoft.UnityToolbelt.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BeaVeR.Game.Domain
{
  public class Block : MonoBehaviour
  {
    public enum BlockState
    {
      Void,
      Unspawned,
      Incoming,
      AtTarget,
      BeingDestroyed,
      Destroyed,
      Outgoing,
      Despawned,
    }

    private const float _DefaultDestroyDelayInSeconds = 2.0f;

#pragma warning disable 649

    [SerializeField]
    private GameObject _arrow;

    [SerializeField]
    private GameObject _dot;

#pragma warning restore 649

    private int _layer;
    private Material _material;
    private float _angle;
    private bool _cutDirectionMatters;

    private Renderer[] _renderers;
    private Collider[] _colliders;

    private bool _isActivated;
    private BlockState _currentState;

    public void Initialize(
      GameController gameController,
      float speed,
      float spawnRadius,
      float despawnRadius,
      GameObject target,
      float targetTimeInSeconds,
      int layer,
      Material material,
      float angle,
      bool cutDirectionMatters,
      AudioSource audioSource,
      string hitSoundName = null)
    {
      _layer = layer;
      _material = material;
      _angle = angle;
      _cutDirectionMatters = cutDirectionMatters;

      _isActivated = false;
      _currentState = BlockState.Void;

      TransitionGameStateTo(BlockState.Unspawned);
    }

    private void Awake()
    {
      _renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
      _colliders = GetComponentsInChildren<Collider>(includeInactive: true);
    }

    private void Start()
    {
      gameObject.SetLayerRecursively(_layer);

      _renderers[0].sharedMaterial = _material;

      if (_cutDirectionMatters)
      {
        _arrow.SetActive(true);
        _dot.SetActive(false);

        this.transform.Rotate(0.0f, 0.0f, -_angle);
      }
      else
      {
        _arrow.SetActive(false);
        _dot.SetActive(true);
      }
    }

    private void FixedUpdate()
    {
      // redacted
    }

    public bool TryHitWithSaber(Saber saber)
    {
      if (!(_currentState == BlockState.Incoming ||
            _currentState == BlockState.AtTarget ||
            _currentState == BlockState.Outgoing
         ))
      {
        return false;
      }

      if (!saber.CanHitBlock(this))
      {
        return false;
      }

      TransitionGameStateTo(BlockState.BeingDestroyed);

      ApplyBlockHitStrategy(saber);

      float destroyDelayInSeconds = _DefaultDestroyDelayInSeconds;

      // TODO: 2022-12-31 - Immortal - HI - I think that removing the audio source from individual blocks (and
      // using one from the game controller for example) will allow us to get rid of this waiting

      AudioClip blockHitAudioClip = PlayHitSound();

      if (blockHitAudioClip != null && blockHitAudioClip.length > destroyDelayInSeconds)
      {
        destroyDelayInSeconds = blockHitAudioClip.length;
      }

      this.RunDelayed(destroyDelayInSeconds, () =>
        {
          TransitionGameStateTo(BlockState.Destroyed);
        });

      return true;
    }

    public SlicingPlaneDescriptor GetCenterSlicingPlane()
    {
      Vector3 planePosition = transform.position;
      Vector3 planeNormal = transform.up;

      if (_cutDirectionMatters)
      {
        // perpendicular to cut direction
        planeNormal = Quaternion.AngleAxis(-90.0f, Vector3.forward) * planeNormal;
      }
      else
      {
        float randomAngle = Random.Range(0.0f, 360.0f);

        planeNormal = Quaternion.AngleAxis(randomAngle, Vector3.forward) * planeNormal;
      }

      return new SlicingPlaneDescriptor(planePosition, planeNormal);
    }

    public void DestroyInternals()
    {
      Destroy(this.gameObject);
    }

    /// <summary>
    /// Only for tests/playgrounds.
    /// </summary>
    internal void EnterIncomingState()
    {
      TransitionGameStateTo(BlockState.Incoming);
    }

    /// <remarks>
    /// During each transition we first set _currentState to newState, so that potential subscribers
    /// of events know about the state change.
    /// </remarks>
    /// <param name="newState"></param>
    private void TransitionGameStateTo(BlockState newState)
    {
      var transitionNotDefinedException =
        new NoTransitionDefinedException(_currentState.ToString(), newState.ToString());

      switch (newState)
      {
        case BlockState.Unspawned:
          {
            if (_currentState == BlockState.Void)
            {
              _currentState = newState;

              SetRenderersAndCollidersEnabled(false);
            }
            else
            {
              throw transitionNotDefinedException;
            }

            break;
          }

        case BlockState.Incoming:
          {
            if (_currentState == BlockState.Unspawned)
            {
              _currentState = newState;

              SetRenderersAndCollidersEnabled(true);
            }
            else
            {
              throw transitionNotDefinedException;
            }

            break;
          }

        case BlockState.AtTarget:
        case BlockState.BeingDestroyed:
        case BlockState.Destroyed:
        case BlockState.Outgoing:
        case BlockState.Despawned:
          {
            throw transitionNotDefinedException;
          }

        default:
          {
            throw new ArgumentOutOfRangeException($"Unknown block state: '{newState}'.");
          }
      }
    }

    // TODO: 2022-12-31 - Immortal - HI - use strategy pattern
    private void ApplyBlockHitStrategy(Saber saber)
    {
      //ApplyBlockHitStrategy_Baseball(saber);
      ApplyBlockHitStrategy_Slicer(saber);
    }

    private void ApplyBlockHitStrategy_Baseball(Saber saber)
    {
      //SetRenderersAndCollidersEnabled(false);

      Rigidbody saberRigidbody = saber.gameObject.GetComponentSafe<Rigidbody>();
      Rigidbody blockRigidbody = this.gameObject.AddComponent<Rigidbody>();

      blockRigidbody.useGravity = false;

      Vector3 hitForce;

      //hitForce = 5.0f * UnityEngine.Random.onUnitSphere;
      //hitForce = saberRigidbody.velocity;

      hitForce = Vector3.down;
      hitForce = Quaternion.Euler(0.0f, _angle >= 180.0f ? -30.0f : 30.0f, -_angle) * hitForce;
      hitForce = hitForce * 10.0f;

      blockRigidbody.AddForce(hitForce, ForceMode.Impulse);
    }

    private void ApplyBlockHitStrategy_Slicer(Saber saber)
    {
      saber.SliceBlock(this);

      SetRenderersAndCollidersEnabled(false);
    }

    private void SetRenderersAndCollidersEnabled(bool enabled)
    {
      foreach (Renderer renderer in _renderers)
      {
        renderer.enabled = enabled;
      }

      foreach (Collider collider in _colliders)
      {
        collider.enabled = enabled;
      }
    }

    private AudioClip PlayHitSound()
    {
      // redacted
      return null;
    }

    public bool IsActivated
    {
      get => _isActivated;

      set
      {
        foreach (Collider collider in _colliders)
        {
          collider.enabled = value;
        }

        _isActivated = value;
      }
    }
  }
}
