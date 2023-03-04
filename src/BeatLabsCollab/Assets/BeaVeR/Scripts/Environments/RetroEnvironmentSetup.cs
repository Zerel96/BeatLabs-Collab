using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BeaVeR.Environments
{
  public class RetroEnvironmentSetup : MonoBehaviour
  {
#pragma warning disable 649

    [SerializeField]
    private Material _skybox;

    [SerializeField]
    private Light _sunSource;

#pragma warning restore 649

    private void OnEnable()
    {
      if (Camera.main != null)
      {
        Camera.main.clearFlags = CameraClearFlags.Skybox;
      }

      RenderSettings.sun = _sunSource;

      RenderSettings.ambientMode = AmbientMode.Skybox;
      RenderSettings.skybox = _skybox;
      RenderSettings.ambientLight = new Color();

      RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
    }

    private void Update()
    {
      // do nothing
    }
  }
}
