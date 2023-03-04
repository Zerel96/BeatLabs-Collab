using BeatLabs.Utils;
using BeaVeR.Game.Domain;
using BeaVeR.XR;
using ImmSoft.UnityToolbelt.Utils;
using UnityEngine;

namespace BeaVeR.Playground.BlockSlicing
{
  public class BlockSlicingController : MonoBehaviour
  {
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value null

    [SerializeField]
    private GameObject _blockArchetype;

    [SerializeField]
    private string _leftNotesLayer;

    [SerializeField]
    private string _rightNotesLayer;

    [SerializeField]
    private Material _leftBlockMaterial;

    [SerializeField]
    private Material _rightBlockMaterial;

    [SerializeField]
    private Vector3 _baseBlockPosition;

    [SerializeField]
    private float _blockPositionOffsetXAmount;

    [SerializeField]
    private float _blockAngle;

    [SerializeField]
    private bool _blockCutDirectionMatters;

    [SerializeField]
    private Saber _leftSaber;

    [SerializeField]
    private Saber _rightSaber;

    [SerializeField]
    private AudioSource _blockAudioSource;

#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value null

    private Block _block1;
    private Block _block2;

    private void Start()
    {
      XRRigState.Instance.AreSabersActivated = true;

      _block1 = SpawnBlock1();
      _block2 = SpawnBlock2();
    }

    private void OnEnable()
    {
      _leftSaber.CollidedWithBlock += OnSaber_CollidedWithBlock;
      _rightSaber.CollidedWithBlock += OnSaber_CollidedWithBlock;
    }

    private void OnDisable()
    {
      _leftSaber.CollidedWithBlock -= OnSaber_CollidedWithBlock;
      _rightSaber.CollidedWithBlock -= OnSaber_CollidedWithBlock;
    }

    private void Update()
    {
      // do nothing
    }

    private Block SpawnBlock1()
    {
      return SpawnBlock(_leftNotesLayer, _leftBlockMaterial, new Vector3(-_blockPositionOffsetXAmount, 0.0f, 0.0f));
    }

    private Block SpawnBlock2()
    {
      return SpawnBlock(_rightNotesLayer, _rightBlockMaterial, new Vector3(_blockPositionOffsetXAmount, 0.0f, 0.0f));
    }

    private Block SpawnBlock(string layerName, Material material, Vector3 positionOffset)
    {
      GameObject blockGameObject = Instantiate(_blockArchetype, this.gameObject.transform);

      blockGameObject.transform.position = _baseBlockPosition + positionOffset;

      Block block = blockGameObject.GetComponentInChildrenSafe<Block>();

      block.Initialize(
        gameController: null,
        speed: 1.0f,
        spawnRadius: 1.0f,
        despawnRadius: 1.0f,
        target: null,
        targetTimeInSeconds: 0.0f,
        layer: LayerMask.NameToLayer(layerName),
        material: material,
        angle: _blockAngle,
        cutDirectionMatters: _blockCutDirectionMatters,
        _blockAudioSource,
        hitSoundName: null);

      block.IsActivated = true;

      block.EnterIncomingState();

      return block;
    }

    private void OnSaber_CollidedWithBlock(Saber saber, Block block)
    {
      if (!saber.CanHitBlock(block))
      {
        return;
      }

      saber.SliceBlock(block);

      this.RunDelayed(2.0f, () =>
        {
          Destroy(block.gameObject);

          if (block == _block1)
          {
            _block1 = SpawnBlock1();
          }
          else if (block == _block2)
          {
            _block2 = SpawnBlock2();
          }
        });
    }
  }
}
