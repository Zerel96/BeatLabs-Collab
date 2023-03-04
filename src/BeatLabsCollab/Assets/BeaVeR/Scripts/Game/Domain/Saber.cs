using System;
using BeatLabs.Scripts.Utils;
using BeatLabs.Utils;
using BeaVeR.Game.Domain.Slicing;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace BeaVeR.Game.Domain
{
  public class Saber : MonoBehaviour
  {
    private const float _MaxAngularVelocity = float.MaxValue;

    public event Action<Saber, Block> CollidedWithBlock;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value null

    [SerializeField]
    private LayerMask _hittableLayerMask;

    [SerializeField]
    private XRBaseController _xrController;

#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value null

    private bool _isActivated;
    private Rigidbody _rigidbody;
    private SaberSlicer _saberSlicer;

    private void Awake()
    {
      _rigidbody = gameObject.GetComponentSafe<Rigidbody>();
      _saberSlicer = gameObject.GetComponentSafe<SaberSlicer>();
    }

    private void Start()
    {
      _rigidbody.maxAngularVelocity = _MaxAngularVelocity;
    }

    private void Update()
    {
      // do nothing
    }

    private void OnTriggerEnter(Collider other)
    {
      if (!_hittableLayerMask.ContainsLayer(other.gameObject.layer))
      {
        return;
      }

      Block block = other.gameObject.GetComponentInParent<Block>();

      if (block != null)
      {
        RaiseCollidedWithBlockEvent(block);
      }
    }

    public bool CanHitBlock(Block block)
    {
      return _hittableLayerMask.ContainsLayer(block.gameObject.layer);
    }

    public bool SliceBlock(Block block)
    {
      return _saberSlicer.SliceBlock(block);
    }

    private void RaiseCollidedWithBlockEvent(Block block)
    {
      CollidedWithBlock?.Invoke(this, block);
    }

    public bool IsActivated
    {
      get => _isActivated;

      set
      {
        _isActivated = value;

        // NOTE: we don't disable the object itself, just its children
        // because otherwise we would lose the grabbable state

        foreach (Transform childTransform in gameObject.transform)
        {
          childTransform.gameObject.SetActive(_isActivated);
        }
      }
    }
  }
}
