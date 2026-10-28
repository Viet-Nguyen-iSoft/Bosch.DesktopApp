using ApiSyncData.Resp;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSyncData
{
  internal static class DocumentDataServerSyncService
  {
    private static readonly SemaphoreSlim SyncLock = new(1, 1);

    internal static async Task<MasterDataChangedEventArgs?> SyncAsync(
      DocumentAPI response,
      CancellationToken token = default)
    {
      ArgumentNullException.ThrowIfNull(response);

      var rows = response.Data?.ListData
        ?? throw new InvalidOperationException(
          "Document: response không có Data.ListData.");
      if (response.Data.TotalRecord != rows.Count)
      {
        throw new InvalidOperationException(
          "Document: TotalRecord không khớp với số phần tử ListData.");
      }

      var sourceIds = new HashSet<Guid>();
      foreach (var row in rows)
      {
        if (!row.Id.HasValue || row.Id == Guid.Empty || !sourceIds.Add(row.Id.Value))
        {
          throw new InvalidOperationException(
            "Document: Id thiếu, không hợp lệ hoặc trùng lặp.");
        }
      }

      if (rows.Count == 0)
        return null;

      await SyncLock.WaitAsync(token).ConfigureAwait(false);
      try
      {
        await using var db = new MySqlDbContext();
        var localRows = await db.Set<DocumentData>()
          .Where(document => sourceIds.Contains(document.Id))
          .ToListAsync(token)
          .ConfigureAwait(false);
        var locals = new Dictionary<Guid, DocumentData>();
        foreach (var local in localRows)
        {
          if (!locals.TryAdd(local.Id, local))
          {
            throw new InvalidOperationException(
              $"Document: trùng local Id {local.Id}.");
          }
        }

        foreach (var source in rows)
        {
          token.ThrowIfCancellationRequested();
          var sourceId = source.Id!.Value;
          if (!locals.TryGetValue(sourceId, out var local))
          {
            local = new DocumentData
            {
              Id = sourceId
            };
            db.Set<DocumentData>().Add(local);
            locals.Add(sourceId, local);
          }

          local.Document = source.Name;
          local.Date = NormalizeTimestamp(source.DocumentDate);
          local.Description = source.Description;
          local.CreatedAt = NormalizeTimestamp(source.CreatedAt);
          local.UpdatedAt = NormalizeTimestamp(source.UpdatedAt);
          local.DeletedFlag = source.IsDelete == true;
          local.SyncFlag = true;
        }

        db.ChangeTracker.DetectChanges();
        var changedEntries = db.ChangeTracker.Entries<DocumentData>()
          .Where(entry => entry.State is EntityState.Added or EntityState.Modified)
          .ToList();
        if (changedEntries.Count == 0)
          return null;

        var addedIds = changedEntries
          .Where(entry => entry.State == EntityState.Added && !entry.Entity.DeletedFlag)
          .Select(entry => entry.Entity.Id)
          .ToList();
        var deletedIds = changedEntries
          .Where(entry => entry.Entity.DeletedFlag &&
            (entry.State == EntityState.Added ||
             entry.Property(nameof(BaseModel.DeletedFlag)).IsModified))
          .Select(entry => entry.Entity.Id)
          .ToList();
        var deletedIdSet = deletedIds.ToHashSet();
        var updatedIds = changedEntries
          .Where(entry => entry.State == EntityState.Modified &&
            !deletedIdSet.Contains(entry.Entity.Id))
          .Select(entry => entry.Entity.Id)
          .ToList();

        int affectedRows = await db.SaveChangesAsync(token).ConfigureAwait(false);
        if (affectedRows == 0)
          return null;

        return new MasterDataChangedEventArgs(
          typeof(DocumentData), addedIds, updatedIds, deletedIds, affectedRows);
      }
      finally
      {
        SyncLock.Release();
      }
    }

    private static Guid? NormalizeId(Guid? id) =>
      id.HasValue && id != Guid.Empty ? id : null;

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
      long normalizedTicks = utcDateTime.Ticks - utcDateTime.Ticks % ticksPerMicrosecond;
      return new DateTime(normalizedTicks, DateTimeKind.Utc);
    }
  }
}
