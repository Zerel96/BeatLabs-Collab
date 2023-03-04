using System;

namespace BeatLabs.Utils
{
  public static class RandomUtils
  {
    private static readonly Random _rand = new Random();

    public static T PickRandomItem<T>(T[] array)
    {
      return array[_rand.Next(array.Length)];
    }
  }
}
