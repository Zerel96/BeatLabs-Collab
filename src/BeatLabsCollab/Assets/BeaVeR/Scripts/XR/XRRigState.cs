using BeaVeR.Game.Domain;
using UnityEngine;

namespace BeaVeR.XR
{
  public class XRRigState : MonoBehaviour
  {
    public static XRRigState Instance { get; private set; }

    public Saber rightSaber;
    public Saber leftSaber;

    private bool _areSabersActivated;

    private void Awake()
    {
      Instance = this;
    }

    public bool AreSabersActivated
    {
      get => _areSabersActivated;

      set
      {
        rightSaber.IsActivated = value;
        leftSaber.IsActivated = value;
      }
    }
  }
}
