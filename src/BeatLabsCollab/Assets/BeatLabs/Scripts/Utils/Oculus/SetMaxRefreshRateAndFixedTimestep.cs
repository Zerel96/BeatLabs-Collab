using System.Collections;
using System.Linq;
using UnityEngine;
using OculusPerformance = Unity.XR.Oculus.Performance;

namespace BeatLabs.Utils.Oculus
{
  // TODO: 2022-04-28 - Immortal - more robust
  public class SetMaxRefreshRateAndFixedTimestep : MonoBehaviour
  {
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value null

    [SerializeField]
    [Tooltip("How often we set the desired refresh rate (in case it has changed). In seconds.")]
    private float _updateFrequency = 5.0f;

    [SerializeField]
    [Tooltip("Leave at 0 for the maximum rate available.")]
    private int _desiredRate = 0;

    [SerializeField]
    [Tooltip("Refresh rate that we will fallback to in case the desired rate is not available.")]
    private int _fallbackRate = 90;

#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value null

    private void Start()
    {
#if !UNITY_EDITOR
      StartCoroutine(UpdateRefreshRateAndTimestep());
#endif
    }

    private IEnumerator UpdateRefreshRateAndTimestep()
    {
      while (true)
      {
        DoUpdateRefreshRateAndTimestep();

        yield return new WaitForSeconds(_updateFrequency);
      }
    }

    private void DoUpdateRefreshRateAndTimestep()
    {
      if (!OculusPerformance.TryGetDisplayRefreshRate(out float currentRate))
      {
        Debug.LogWarning("Couldn't get current Oculus display refresh rate. We won't try to change anything.");

        return;
      }

      float newRate;

      if (_desiredRate == 0)
      {
        if (OculusPerformance.TryGetAvailableDisplayRefreshRates(out float[] availableRates))
        {
          newRate = availableRates.Max();
        }
        else
        {
          Debug.LogWarning("Couldn't get get available Oculus display refresh rates. We'll try to use the fallback one.");

          newRate = _fallbackRate;
        }
      }
      else
      {
        newRate = _desiredRate;
      }

      if (currentRate < newRate)
      {
        if (OculusPerformance.TrySetDisplayRefreshRate(newRate))
        {
          // TODO: 2022-04-28 - Immortal - do we want to have the timestep even lower than refresh rate?
          Time.fixedDeltaTime = 1.0f / newRate;
          Time.maximumDeltaTime = 1.0f / newRate;
        }
        else
        {
          Debug.LogWarning($"Couldn't set Oculus display refresh rate to {newRate}.");
        }
      }
    }

    private void Update()
    {
      // do nothing
    }
  }
}
