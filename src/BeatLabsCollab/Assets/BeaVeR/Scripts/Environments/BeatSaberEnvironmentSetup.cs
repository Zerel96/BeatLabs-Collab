using UnityEngine;
using UnityEngine.Rendering;

namespace BeaVeR.Environments
{

  public class BeatSaberEnvironmentSetup : MonoBehaviour
  {
#pragma warning disable 649

    [SerializeField]
    private Color _cameraClearColor;

    [SerializeField]
    private Color _ambientColor;

    [SerializeField]
    private Light _sunSource;

#pragma warning restore 649

    private void OnEnable()
    {
      if (Camera.main != null)
      {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
      }

      RenderSettings.sun = _sunSource;

      RenderSettings.ambientMode = AmbientMode.Flat;
      RenderSettings.ambientLight = _ambientColor;
      RenderSettings.skybox = null;

      RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
    }

    private void Update()
    {
      // do nothing
    }
  }
}
