using System;
using System.Collections.Generic;
using System.Linq;

namespace BeatLabs.Utils
{
  public static class EnumerableExtensions
  {
    public static int? FirstIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
      int? result =
        enumerable
          .Select((item, index) => (item, index))
          .Where(t => predicate(t.item))
          .Select(t => (int?)t.index)
          .FirstOrDefault();

      return result;
    }
  }
}
