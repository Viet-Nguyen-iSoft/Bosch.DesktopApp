using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LTP.Truck.Services
{
  internal static class AuditTrail
  {
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
      ReferenceHandler = ReferenceHandler.IgnoreCycles,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static async Task WriteAsync(
      string action,
      string entityType,
      object? entityId = null,
      string? entityName = null,
      string? source = null,
      string? reason = null,
      object? before = null,
      object? after = null,
      bool isSuccess = true,
      Exception? exception = null)
    {
      try
      {
        var user = AppCore.Ins._userCurrent;
        await new AuditLogService().WriteAsync(new AuditLog
        {
          Action = action,
          EntityType = entityType,
          EntityId = entityId?.ToString(),
          EntityName = entityName,
          UserId = user?.Id,
          Username = user?.Username,
          UserDisplayName = string.IsNullOrWhiteSpace(user?.DisplayName)
            ? user?.FullName
            : user.DisplayName,
          Source = source,
          Reason = reason,
          BeforeData = Serialize(before),
          AfterData = Serialize(after),
          IsSuccess = isSuccess,
          ErrorMessage = exception?.ToString()
        }).ConfigureAwait(false);
      }
      catch
      {
        // Audit không được phép làm gián đoạn thao tác nghiệp vụ.
      }
    }

    private static string? Serialize(object? value)
    {
      if (value == null)
        return null;

      try
      {
        return JsonSerializer.Serialize(value, JsonOptions);
      }
      catch
      {
        return value.ToString();
      }
    }
  }
}
