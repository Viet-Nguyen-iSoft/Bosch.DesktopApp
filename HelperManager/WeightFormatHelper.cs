using System.Globalization;

namespace HelperManager
{
  public static class WeightFormatHelper
  {
    public static string Format(double value, int decimalPlaces = 0)
    {
      return value.ToString($"N{decimalPlaces}", CultureInfo.InvariantCulture);
    }
  }
}
