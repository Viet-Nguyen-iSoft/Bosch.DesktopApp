using System.Reflection;

namespace LTP.Truck.Custom
{
  internal static class DataGridViewPerformance
  {
    private static readonly PropertyInfo? DoubleBufferedProperty =
      typeof(DataGridView).GetProperty(
        "DoubleBuffered",
        BindingFlags.Instance | BindingFlags.NonPublic);

    public static void EnableDoubleBuffering(DataGridView grid)
    {
      DoubleBufferedProperty?.SetValue(grid, true);
    }
  }
}
