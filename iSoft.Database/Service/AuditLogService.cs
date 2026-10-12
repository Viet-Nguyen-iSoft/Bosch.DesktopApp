using iSoft.Database.DbContexts;
using iSoft.Database.Models;

namespace iSoft.Database.Service
{
  public class AuditLogService
  {
    /// <summary>
    /// Ghi audit theo cơ chế best-effort: lỗi audit không được làm hỏng nghiệp vụ chính.
    /// </summary>
    public async Task<bool> WriteAsync(AuditLog auditLog)
    {
      try
      {
        auditLog.Id = auditLog.Id == Guid.Empty ? Guid.NewGuid() : auditLog.Id;
        auditLog.OccurredAt = auditLog.OccurredAt == default
          ? DateTime.UtcNow
          : auditLog.OccurredAt;

        await using var context = new MySqlDbContext();
        await context.AuditLogs!.AddAsync(auditLog).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
