using System.ComponentModel;
using iSoft.Database.Models;

namespace iSoft.Database.DTO
{
  public class AuditLogDTO
  {
    [DisplayName("Thời gian")]
    public DateTime OccurredAt { get; set; }

    [DisplayName("Thao tác")]
    public string Action { get; set; } = string.Empty;

    [DisplayName("Đối tượng")]
    public string EntityType { get; set; } = string.Empty;

    [DisplayName("Mã dữ liệu")]
    public string? EntityId { get; set; }

    [DisplayName("Tên dữ liệu")]
    public string? EntityName { get; set; }

    [DisplayName("Tài khoản")]
    public string? Username { get; set; }

    [DisplayName("Người thao tác")]
    public string? UserDisplayName { get; set; }

    [DisplayName("Màn hình")]
    public string? Source { get; set; }

    [DisplayName("Lý do")]
    public string? Reason { get; set; }

    [DisplayName("Kết quả")]
    public string Result { get; set; } = string.Empty;

    [DisplayName("Dữ liệu trước")]
    public string? BeforeData { get; set; }

    [DisplayName("Dữ liệu sau")]
    public string? AfterData { get; set; }

    [DisplayName("Chi tiết lỗi")]
    public string? ErrorMessage { get; set; }

    public static AuditLogDTO FromModel(AuditLog log)
    {
      return new AuditLogDTO
      {
        OccurredAt = DateTime.SpecifyKind(log.OccurredAt, DateTimeKind.Utc)
          .ToLocalTime(),
        Action = TranslateAction(log.Action),
        EntityType = log.EntityType,
        EntityId = log.EntityId,
        EntityName = log.EntityName,
        Username = log.Username,
        UserDisplayName = log.UserDisplayName,
        Source = log.Source,
        Reason = log.Reason,
        Result = log.IsSuccess ? "Thành công" : "Thất bại",
        BeforeData = log.BeforeData,
        AfterData = log.AfterData,
        ErrorMessage = log.ErrorMessage
      };
    }

    private static string TranslateAction(string action) => action switch
    {
      AuditActions.Create => "Thêm mới",
      AuditActions.Update => "Cập nhật",
      AuditActions.Delete => "Xóa",
      AuditActions.Restore => "Khôi phục",
      AuditActions.Import => "Import",
      AuditActions.InvalidOperation => "Thao tác không hợp lệ",
      AuditActions.Error => "Lỗi",
      _ => action
    };
  }
}
