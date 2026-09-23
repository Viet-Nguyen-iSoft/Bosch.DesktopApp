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
      var linked = locals.Where(x => x.IdSrc.HasValue && x.IdSrc != Guid.Empty).ToList();
      var byId = linked.ToDictionary(x => x.IdSrc!.Value);
      var unlinkedByLocalId = locals
        .Where(x => !x.IdSrc.HasValue || x.IdSrc == Guid.Empty)
        .Where(x => x.Id != Guid.Empty)
        .ToDictionary(x => x.Id);
      var ids = snapshotIds?.ToHashSet() ?? new HashSet<Guid>();
      var added = new List<TEntity>();
      foreach (var row in rows)
      {
        token.ThrowIfCancellationRequested();
        var id = row.Id!.Value;
        ids.Add(id);
        if (!byId.TryGetValue(id, out var local))
        {
          // Dữ liệu cũ có thể đã dùng Id từ server làm khóa chính nhưng chưa gắn IdSrc.
          // Nhận lại bản ghi đó để update, tránh insert trùng khóa chính.
          if (unlinkedByLocalId.Remove(id, out local))
            local.IdSrc = id;
          else
          {
            // Bản ghi được tạo từ server dùng luôn server Id làm khóa chính local.
            local = new TEntity { Id = id, IdSrc = id };
            added.Add(local);
          }
          byId.Add(id, local);
        }
        map(row, local);
        // Database lưu timestamp tới microsecond; chuẩn hóa để EF không nhận
        // phần tick dư là một thay đổi mới trong mọi chu kỳ đồng bộ.
        local.CreatedAt = NormalizeTimestamp(row.CreatedAt);
        local.UpdatedAt = NormalizeTimestamp(row.UpdatedAt);
        local.DeletedFlag = false;
      }
      foreach (var local in linked)
      {
        token.ThrowIfCancellationRequested();
        if (!ids.Contains(local.IdSrc!.Value) && !local.DeletedFlag)
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
