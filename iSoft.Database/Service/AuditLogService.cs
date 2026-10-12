using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Service
{
  public class AuditLogService
  {
    public async Task<(List<AuditLog> Items, int TotalRecords)> SearchAsync(
      DateTime fromUtc,
      DateTime toUtc,
      string? action,
      int pageNumber,
      int pageSize)
    {
      await using var context = new MySqlDbContext();
      var query = context.AuditLogs!
        .AsNoTracking()
        .Where(log => log.OccurredAt >= fromUtc && log.OccurredAt <= toUtc);

      if (!string.IsNullOrWhiteSpace(action))
        query = query.Where(log => log.Action == action);

      var totalRecords = await query.CountAsync().ConfigureAwait(false);
      var items = await query
        .OrderByDescending(log => log.OccurredAt)
        .ThenByDescending(log => log.Id)
        .Skip((Math.Max(1, pageNumber) - 1) * Math.Max(1, pageSize))
        .Take(Math.Max(1, pageSize))
        .ToListAsync()
        .ConfigureAwait(false);

      return (items, totalRecords);
    }

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
