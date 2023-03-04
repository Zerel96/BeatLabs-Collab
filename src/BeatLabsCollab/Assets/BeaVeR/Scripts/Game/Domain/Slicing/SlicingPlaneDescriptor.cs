using UnityEngine;

namespace BeaVeR.Game.Domain.Slicing
{
  public struct SlicingPlaneDescriptor
  {
    public SlicingPlaneDescriptor(Vector3 position, Vector3 normal)
    {
      Position = position;
      Normal = normal;
    }

    public static SlicingPlaneDescriptor FromTransform(Transform transform)
    {
      return new SlicingPlaneDescriptor(transform.position, transform.up);
    }

    public Vector3 Position { get; private set; }

    public Vector3 Normal { get; private set; }
  }
}
