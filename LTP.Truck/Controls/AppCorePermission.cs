using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LTP.Truck.Controls
{
  public partial class AppCore
  {
    /// <summary>
    /// Kiểm tra user đang đăng nhập có quyền tương ứng với mã quyền hay không.
    /// </summary>
    /// <param name="permissionCode">Mã quyền cần kiểm tra.</param>
    /// <returns>
    /// true nếu danh sách quyền của user có mã quyền; ngược lại trả về false.
    /// </returns>
    public bool CheckPermission(string? permissionCode)
    {
      if (_userCurrent == null ||
          string.IsNullOrWhiteSpace(permissionCode) ||
          string.IsNullOrWhiteSpace(_userCurrent.Role))
      {
        return false;
      }

      try
      {
        var normalizedCode = permissionCode.Trim();
        var permissions = JArray.Parse(_userCurrent.Role);

        return permissions.Any(permission =>
        {
          // Dữ liệu cũ lưu trực tiếp danh sách mã: ["0043", "0044"].
          var code = permission.Type == JTokenType.String
            ? permission.Value<string>()
            // Đồng thời hỗ trợ danh sách permission object có trường Code.
            : (permission as JObject)?["Code"]?.Value<string>();

          return string.Equals(
            code?.Trim(),
            normalizedCode,
            StringComparison.OrdinalIgnoreCase);
        });
      }
      catch (JsonException)
      {
        return false;
      }
    }
  }
}
