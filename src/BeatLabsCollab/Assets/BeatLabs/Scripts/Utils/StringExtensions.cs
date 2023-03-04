using System;

namespace BeatLabs.Utils
{
  public static class StringExtensions
  {
    public static bool IsEnum<TEnum>(this string s, TEnum enumValue)
      where TEnum : struct
    {
      if (string.IsNullOrEmpty(s))
      {
        return false;
      }

      return Enum.TryParse(s, true, out TEnum parsedEnumValue) && parsedEnumValue.Equals(enumValue);
    }

    public static bool EqualsIgnoreCase(this string s, string otherString)
    {
      return string.Equals(s, otherString, StringComparison.OrdinalIgnoreCase);
    }
  }
}
