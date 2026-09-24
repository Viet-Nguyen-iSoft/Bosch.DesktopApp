using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace HelperManager
{
  public static class TextSearchHelper
  {
    public static bool Contains(string? source, string? searchText)
    {
      if (string.IsNullOrWhiteSpace(searchText))
        return true;
      if (string.IsNullOrWhiteSpace(source))
        return false;

      return Normalize(source).Contains(Normalize(searchText), StringComparison.Ordinal);
    }

    public static List<T> FilterBrowsableProperties<T>(IEnumerable<T>? values, string? searchText)
    {
      if (values == null)
        return new List<T>();
      if (string.IsNullOrWhiteSpace(searchText))
        return values.ToList();

      var searchableProperties = TypeDescriptor.GetProperties(typeof(T))
        .Cast<PropertyDescriptor>()
        .Where(property => property.IsBrowsable)
        .ToArray();

      return values.Where(item => searchableProperties.Any(property =>
        Contains(property.GetValue(item)?.ToString(), searchText))).ToList();
    }

    private static string Normalize(string value)
    {
      string decomposed = value.Trim().Normalize(NormalizationForm.FormD);
      var result = new StringBuilder(decomposed.Length);

      foreach (char character in decomposed)
      {
        if (CharUnicodeInfo.GetUnicodeCategory(character) == UnicodeCategory.NonSpacingMark)
          continue;

        result.Append(character switch
        {
          'đ' or 'Đ' => 'D',
          _ => char.ToUpperInvariant(character),
        });
      }

      return result.ToString().Normalize(NormalizationForm.FormC);
    }
  }
}
