using UnityEngine;

namespace BeaVeR.XR
{
  public class ResetCameraOffset : MonoBehaviour
  {
    private void Start()
    {
      this.transform.position = Vector3.zero;
      this.transform.rotation = Quaternion.identity;
      this.transform.localScale = Vector3.one;
    }
  }
}
