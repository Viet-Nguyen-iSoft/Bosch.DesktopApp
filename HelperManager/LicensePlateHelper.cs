using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperManager
{
  public static class LicensePlateHelper
  {
    // Accepted weighing template: XXYXXXX or XXYXXXXX.
    // X is a digit and Y is exactly one letter.
    private static readonly System.Text.RegularExpressions.Regex VietnamCarLicensePlateRegex = new(
      @"^(?<province>\d{2})(?<series>[A-Z])-?(?<number>\d{4}|\d{5}|\d{3}\.\d{2})$",
      System.Text.RegularExpressions.RegexOptions.Compiled |
      System.Text.RegularExpressions.RegexOptions.CultureInvariant |
      System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    /// <summary>
    /// Kiểm tra và chuẩn hóa biển số dùng tại trạm cân theo mẫu
    /// XXYXXXX hoặc XXYXXXXX (X là số, Y là chữ).
    /// Chấp nhận chữ thường/chữ hoa, khoảng trắng, dấu '-' và dấu chấm phân nhóm.
    /// Ví dụ hợp lệ: 30A-1234, 30A1234, 30A-12345, 30A12345, 30A-123.45.
    /// Không chấp nhận nhiều hơn một chữ cái hoặc chữ số trong phần seri.
    /// </summary>
    /// <returns>
    /// IsValid cho biết dữ liệu có hợp lệ hay không; Plate là biển số đã được chuẩn hóa,
    /// hoặc chuỗi rỗng nếu dữ liệu không hợp lệ.
    /// </returns>
    public static (bool IsValid, string Plate) IsValidVietnamLicensePlate(string? licensePlate)
    {
      if (string.IsNullOrWhiteSpace(licensePlate))
      {
        return (false, string.Empty);
      }

      string normalizedLicensePlate = System.Text.RegularExpressions.Regex.Replace(
        licensePlate.Trim(), @"\s+", string.Empty).ToUpperInvariant();

      System.Text.RegularExpressions.Match match =
        VietnamCarLicensePlateRegex.Match(normalizedLicensePlate);

      if (!match.Success)
      {
        return (false, string.Empty);
      }

      string prefix = match.Groups["province"].Value + match.Groups["series"].Value;
      string number = match.Groups["number"].Value.Replace(".", string.Empty);
      string formattedNumber = number.Length == 5
        ? $"{number[..3]}.{number[3..]}"
        : number;

      return (true, $"{prefix}-{formattedNumber}");
    }
  }
}
