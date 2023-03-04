using UnityEngine;

namespace BeatLabs.Scripts.Utils
{
  public static class LayerMaskExtensions
  {
    public static bool ContainsLayer(this LayerMask layerMask, int layer)
    {
      return (layerMask & (1 << layer)) != 0;
    }
  }
}
