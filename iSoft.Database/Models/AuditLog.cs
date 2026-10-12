namespace iSoft.Database.Models
{
  /// <summary>
  /// Nhật ký bất biến phục vụ truy vết các thao tác quan trọng trong hệ thống.
  /// Không tạo khóa ngoại để nhật ký vẫn được giữ khi dữ liệu nguồn bị xóa.
  /// </summary>
  public class AuditLog
  {
    public Guid Id { get; set; }
    public DateTime OccurredAt { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public string? EntityName { get; set; }
    public Guid? UserId { get; set; }
    public string? Username { get; set; }
    public string? UserDisplayName { get; set; }
    public string? Source { get; set; }
    public string? Reason { get; set; }
    public string? BeforeData { get; set; }
    public string? AfterData { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
  }

  public static class AuditActions
  {
    public const string Create = "CREATE";
    public const string Update = "UPDATE";
    public const string Delete = "DELETE";
    public const string Restore = "RESTORE";
    public const string Import = "IMPORT";
    public const string InvalidOperation = "INVALID_OPERATION";
    public const string Error = "ERROR";
  }
}
