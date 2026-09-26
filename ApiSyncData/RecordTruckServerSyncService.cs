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
        var locals = await db.Set<RecordTruck>()
          .Where(record => sourceIds.Contains(record.Id))
          .ToDictionaryAsync(record => record.Id, token)
          .ConfigureAwait(false);

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

          Map(source, local);
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
      RecordTruck target)
    {
      target.NoLabelAuto = string.IsNullOrWhiteSpace(source.NoLabelAuto)
        ? source.SerialCode
        : source.NoLabelAuto;
      target.LicensePlate = LicensePlateRepository.Normalize(
        string.IsNullOrWhiteSpace(source.LicensePlate) ? source.Plate : source.LicensePlate);
      target.NetTime01 = source.Net01;
      target.TareTime01 = source.Tare01;
      target.NetTime02 = source.Net02;
      target.TareTime02 = source.Tare02;
      target.EnumTypeDataTruck = GetLocalStatus(source);
      target.WeighInAt = NormalizeTimestamp(source.WeighInAt);
      target.ClientId = NormalizeId(source.ClientId);
      target.TypeGoodsId = NormalizeId(source.TypeGoodsId);
      target.WarehouseId = NormalizeId(source.WarehouseId);
      target.UserId = NormalizeId(source.UserId);
      target.StationId = NormalizeId(source.StationId);
      target.CreatedAt = NormalizeTimestamp(source.CreatedAt);
      target.UpdatedAt = NormalizeTimestamp(source.UpdatedAt);
      target.DeletedFlag = source.IsDelete == true;
      target.EnableFlag = source.IsDelete != true;
      target.SyncFlag = true;
      target.IdSrc = source.Id;
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
