using ApiSyncData.Resp;
using iSoft.Database.Models;

namespace ApiSyncData
{
  internal static class ServerSnapshot
  {
    internal static void Validate<T>(List<T>? rows, int? total) where T : IServerRecord
    {
      if (rows == null || total != rows.Count)
        throw new InvalidOperationException($"{typeof(T).Name}: cần ListData đầy đủ và TotalRecord khớp.");
      var ids = new HashSet<Guid>();
      foreach (var row in rows)
      {
        if (row == null || !row.Id.HasValue || row.Id == Guid.Empty || !ids.Add(row.Id.Value))
          throw new InvalidOperationException($"{typeof(T).Name}: Id thiếu, không hợp lệ hoặc trùng lặp.");
        if (row.IsDelete == true)
          throw new InvalidOperationException($"{typeof(T).Name}: danh sách phải chỉ chứa bản ghi chưa xóa.");
      }
    }

    internal static List<TEntity> Apply<TSource, TEntity>(
      List<TSource> rows, List<TEntity> locals, Action<TSource, TEntity> map,
      CancellationToken token, IEnumerable<Guid>? snapshotIds = null)
      where TSource : IServerRecord
      where TEntity : BaseModel, new()
    {
      var byId = locals.ToDictionary(x => x.Id);
      var ids = snapshotIds?.ToHashSet() ?? new HashSet<Guid>();
      var added = new List<TEntity>();
      foreach (var row in rows)
      {
        token.ThrowIfCancellationRequested();
        var id = row.Id!.Value;
        ids.Add(id);
        if (!byId.TryGetValue(id, out var local))
        {
          local = new TEntity { Id = id };
          added.Add(local);
          byId.Add(id, local);
        }
        map(row, local);
        // Database lưu timestamp tới microsecond; chuẩn hóa để EF không nhận
        // phần tick dư là một thay đổi mới trong mọi chu kỳ đồng bộ.
        local.CreatedAt = NormalizeTimestamp(row.CreatedAt);
        local.UpdatedAt = NormalizeTimestamp(row.UpdatedAt);
        local.DeletedFlag = false;
        local.SyncFlag = true;
      }
      foreach (var local in locals)
      {
        token.ThrowIfCancellationRequested();
        if (local.SyncFlag && !ids.Contains(local.Id) && !local.DeletedFlag)
        {
          local.DeletedFlag = true;
          local.UpdatedAt = DateTime.UtcNow;
        }
      }
      return added;
    }

    private static DateTime? NormalizeTimestamp(DateTime? value)
    {
      if (!value.HasValue)
        return null;

      const long ticksPerMicrosecond = 10;
      var utcDateTime = value.Value.Kind switch
      {
        DateTimeKind.Utc => value.Value,
        DateTimeKind.Local => value.Value.ToUniversalTime(),
        _ => DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
      };
      var normalizedTicks = utcDateTime.Ticks - utcDateTime.Ticks % ticksPerMicrosecond;
      return new DateTime(normalizedTicks, DateTimeKind.Utc);
    }
  }
}
