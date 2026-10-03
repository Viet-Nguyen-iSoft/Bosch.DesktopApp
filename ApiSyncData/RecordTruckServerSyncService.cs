using ApiSyncData.Resp;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using Microsoft.EntityFrameworkCore;
using static iSoft.Database.EnumData;

namespace ApiSyncData
{
  internal static class RecordTruckServerSyncService
  {
    private static readonly SemaphoreSlim SyncLock = new(1, 1);

    internal static async Task<MasterDataChangedEventArgs?> SyncAsync(
      RecordTruckAPI response,
      CancellationToken token = default)
    {
      var rows = response.Data?.ListData
        ?? throw new InvalidOperationException(
          "RecordTruckFromServer: response không có Data.ListData.");
      if (response.Data!.TotalRecord != rows.Count)
      {
        throw new InvalidOperationException(
          "RecordTruckFromServer: TotalRecord không khớp với số phần tử ListData.");
      }

      var sourceIds = new HashSet<Guid>();
      foreach (var row in rows)
      {
        if (!row.Id.HasValue || row.Id == Guid.Empty || !sourceIds.Add(row.Id.Value))
        {
          throw new InvalidOperationException(
            "RecordTruckFromServer: Id thiếu, không hợp lệ hoặc trùng lặp.");
        }
      }

      await SyncLock.WaitAsync(token).ConfigureAwait(false);
      try
      {
        await using var db = new MySqlDbContext();
        var references = await LoadReferencesAsync(db, rows, token)
          .ConfigureAwait(false);
        var localRows = await db.Set<RecordTruck>()
          .Where(record =>
            sourceIds.Contains(record.Id) ||
            (record.IdSrc.HasValue && sourceIds.Contains(record.IdSrc.Value)))
          .ToListAsync(token)
          .ConfigureAwait(false);
        var locals = new Dictionary<Guid, RecordTruck>();
        foreach (var localRow in localRows)
        {
          var sourceId = NormalizeId(localRow.IdSrc) ?? localRow.Id;
          if (!locals.TryAdd(sourceId, localRow))
          {
            throw new InvalidOperationException(
              $"RecordTruckFromServer: nhiều bản ghi local cùng liên kết server Id {sourceId}.");
          }
        }

        foreach (var source in rows)
        {
          token.ThrowIfCancellationRequested();
          var sourceId = source.Id!.Value;
          if (!locals.TryGetValue(sourceId, out var local))
          {
            local = new RecordTruck
            {
              Id = sourceId,
              IdSrc = sourceId,
              SyncFlag = true
            };
            db.Set<RecordTruck>().Add(local);
            locals.Add(sourceId, local);
          }

          Map(source, local, references);
        }

        db.ChangeTracker.DetectChanges();
        var changedEntries = db.ChangeTracker.Entries<RecordTruck>()
          .Where(entry => entry.State is EntityState.Added or EntityState.Modified)
          .ToList();
        if (changedEntries.Count == 0)
          return null;

        var addedIds = changedEntries
          .Where(entry => entry.State == EntityState.Added)
          .Select(entry => entry.Entity.Id)
          .ToList();
        var updatedIds = changedEntries
          .Where(entry => entry.State == EntityState.Modified)
          .Select(entry => entry.Entity.Id)
          .ToList();
        var deletedIds = changedEntries
          .Where(entry => entry.State == EntityState.Modified &&
            entry.Property(nameof(RecordTruck.DeletedFlag)).IsModified &&
            entry.Entity.DeletedFlag)
          .Select(entry => entry.Entity.Id)
          .ToList();

        var affectedRows = await db.SaveChangesAsync(token).ConfigureAwait(false);
        if (affectedRows == 0)
          return null;

        return new MasterDataChangedEventArgs(
          typeof(RecordTruck),
          addedIds,
          updatedIds.Except(deletedIds).ToList(),
          deletedIds,
          affectedRows);
      }
      finally
      {
        SyncLock.Release();
      }
    }

    private static void Map(
      ListDatumRecordTruck source,
      RecordTruck target,
      ReferenceIds references)
    {
      target.NoLabelAuto = source.NoLabelAuto;
      target.NoLabelManual = source.NoLabelManual;
      target.LicensePlate = LicensePlateRepository.Normalize(
        string.IsNullOrWhiteSpace(source.LicensePlate) ? source.Plate : source.LicensePlate);
      target.NetTime01 = source.Net01;
      target.TareTime01 = source.Tare01;
      target.NetTime02 = source.Net02;
      target.TareTime02 = source.Tare02;
      target.EnumTypeDataTruck = GetLocalStatus(source);
      target.WeighInAt = NormalizeTimestamp(source.WeighInAt);
      target.WeighOutAt = NormalizeTimestamp(source.WeighOutAt);

      target.ClientId = ResolveLocalId(
        source.Id, source.ClientId, references.Clients, nameof(source.ClientId));
      target.TypeGoodsId = ResolveLocalId(
        source.Id, source.TypeGoodsId, references.TypeGoods, nameof(source.TypeGoodsId));
      target.WarehouseId = ResolveLocalId(
        source.Id, source.WarehouseId, references.Warehouses, nameof(source.WarehouseId));
      target.UserId = ResolveLocalId(
        source.Id, source.UserId, references.Users, nameof(source.UserId));
      target.StationId = ResolveLocalId(
        source.Id, source.StationId, references.Stations, nameof(source.StationId));
      target.CreatedAt = NormalizeTimestamp(source.CreatedAt);
      target.UpdatedAt = NormalizeTimestamp(source.UpdatedAt);
      target.DeletedFlag = source.IsDelete == true;
      target.EnableFlag = source.IsDelete != true;
      target.SyncFlag = true;
      target.IdSrc = source.Id;
      target.Note = source.Document;
      target.NameDriver = source.NameDriver;
      target.IdCard = source.IdCard;
      target.ReasonDelete = source.ReasonDelete;
    }

    private static EnumTypeDataTruck GetLocalStatus(ListDatumRecordTruck source)
    {
      if (source.IsDelete == true)
        return EnumTypeDataTruck.Delete;
      if (source.Net02 != 0 || source.Tare02 != 0)
        return EnumTypeDataTruck.DoneTime02;
      if (source.Net01 != 0 || source.Tare01 != 0)
        return EnumTypeDataTruck.DoneTime01;
      return EnumTypeDataTruck.None;
    }

    private static Guid? NormalizeId(Guid? id) =>
      id.HasValue && id != Guid.Empty ? id : null;

    private static Guid? ResolveLocalId(
      Guid? recordTruckId,
      Guid? sourceId,
      IReadOnlyDictionary<Guid, Guid> localIds,
      string relationName)
    {
      var normalizedId = NormalizeId(sourceId);
      if (!normalizedId.HasValue)
        return null;

      if (localIds.TryGetValue(normalizedId.Value, out var localId))
        return localId;

      System.Diagnostics.Trace.TraceWarning(
        $"RecordTruck {recordTruckId}: {relationName} {normalizedId} " +
        "không tồn tại trong master data local; khóa ngoại được để trống.");
      return null;
    }

    private static async Task<ReferenceIds> LoadReferencesAsync(
      MySqlDbContext db,
      IReadOnlyCollection<ListDatumRecordTruck> rows,
      CancellationToken token)
    {
      return new ReferenceIds(
        await LoadLocalIdsAsync<Client>(db, rows.Select(row => row.ClientId), token)
          .ConfigureAwait(false),
        await LoadLocalIdsAsync<TypeGoods>(db, rows.Select(row => row.TypeGoodsId), token)
          .ConfigureAwait(false),
        await LoadLocalIdsAsync<Warehouse>(db, rows.Select(row => row.WarehouseId), token)
          .ConfigureAwait(false),
        await LoadLocalIdsAsync<User>(db, rows.Select(row => row.UserId), token)
          .ConfigureAwait(false),
        await LoadLocalIdsAsync<Station>(db, rows.Select(row => row.StationId), token)
          .ConfigureAwait(false));
    }

    private static async Task<Dictionary<Guid, Guid>> LoadLocalIdsAsync<TEntity>(
      MySqlDbContext db,
      IEnumerable<Guid?> sourceIds,
      CancellationToken token)
      where TEntity : BaseModel
    {
      var ids = sourceIds
        .Where(id => id.HasValue && id != Guid.Empty)
        .Select(id => id!.Value)
        .Distinct()
        .ToList();
      if (ids.Count == 0)
        return new Dictionary<Guid, Guid>();

      var entities = await db.Set<TEntity>()
        .AsNoTracking()
        .Where(entity =>
          (entity.IdSrc.HasValue && ids.Contains(entity.IdSrc.Value)) ||
          ids.Contains(entity.Id))
        .Select(entity => new { entity.Id, entity.IdSrc })
        .ToListAsync(token)
        .ConfigureAwait(false);

      var result = new Dictionary<Guid, Guid>();
      foreach (var entity in entities)
      {
        var sourceId = NormalizeId(entity.IdSrc) ?? entity.Id;
        result.TryAdd(sourceId, entity.Id);
      }

      return result;
    }

    private sealed record ReferenceIds(
      IReadOnlyDictionary<Guid, Guid> Clients,
      IReadOnlyDictionary<Guid, Guid> TypeGoods,
      IReadOnlyDictionary<Guid, Guid> Warehouses,
      IReadOnlyDictionary<Guid, Guid> Users,
      IReadOnlyDictionary<Guid, Guid> Stations);

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
