using UnityEngine.InputSystem;

namespace BeatLabs.UI.Input
{
  public static class InputUtils
  {
    public static bool HasInputActionBeenPerformed(InputAction.CallbackContext callbackContext)
    {
      return callbackContext.phase == InputActionPhase.Performed;
    }
  }
}
