using BeaVeR.Core;
using UnityEngine;

namespace BeaVeR.Game
{
  // TODO: 2022-11-16 - Immortal - HI - does it really have to be an IController? we don't even use any input with Activate(input)
  public class GameController : MonoBehaviour, IController<EmptyControllerInput>
  {
    public void Activate(EmptyControllerInput input)
    {
      if (IsActivated)
      {
        return;
      }

      SetIsActivated(true);
    }

    public void Deactivate()
    {
      if (!IsActivated)
      {
        return;
      }

      SetIsActivated(false);
    }

    private void SetIsActivated(bool isActivated)
    {
      this.gameObject.SetActive(isActivated);

      IsActivated = isActivated;
    }

    public bool IsActivated { get; private set; }
  }
}
