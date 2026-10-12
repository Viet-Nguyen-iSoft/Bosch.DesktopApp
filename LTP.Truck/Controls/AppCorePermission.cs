using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using iSoft.Database.Models;
using LTP.Truck.Services;

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
          string.IsNullOrWhiteSpace(permissionCode))
      {
        return false;
      }

      if (string.Equals(
        _userCurrent.Username,
        HelperManager.PasswordPolicy.BuiltInAccount,
        StringComparison.OrdinalIgnoreCase))
      {
        return true;
      }

      if (string.IsNullOrWhiteSpace(_userCurrent.Role))
        return false;

      try
      {
        var normalizedCode = permissionCode.Trim();
        var permissions = JArray.Parse(_userCurrent.Role);

        var hasPermission = permissions.Any(permission =>
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

        if (!hasPermission)
        {
          _ = AuditTrail.WriteAsync(
            AuditActions.InvalidOperation,
            "Permission",
            entityId: normalizedCode,
            entityName: "Truy cập chức năng không được cấp quyền",
            source: nameof(CheckPermission),
            reason: $"Tài khoản không có quyền {normalizedCode}",
            isSuccess: false);
        }

        return hasPermission;
      }
      catch (JsonException)
      {
        _ = AuditTrail.WriteAsync(
          AuditActions.InvalidOperation,
          "Permission",
          entityId: permissionCode,
          entityName: "Dữ liệu phân quyền không hợp lệ",
          source: nameof(CheckPermission),
          reason: "Không đọc được danh sách quyền của tài khoản",
          isSuccess: false);
        return false;
      }
    }
  }
}
