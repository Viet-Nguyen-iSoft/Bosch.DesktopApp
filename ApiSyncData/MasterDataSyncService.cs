using ApiSyncData.Resp;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiSyncData
{
  public static class MasterDataSyncService
  {
    private static readonly SemaphoreSlim SyncLock = new(1, 1);

    public static Task<MasterDataChangedEventArgs?> SyncCategoryTaresAsync(
      CategoryTareAPI response,
      CancellationToken token = default)
    {
      var rows = response.Data?.ListData
        ?? throw new InvalidOperationException(
          "CategoryTare: response không có Data.ListData.");
      if (response.Data.TotalRecord != rows.Count)
      {
        throw new InvalidOperationException(
          "CategoryTare: TotalRecord không khớp với số phần tử ListData.");
      }

      var ids = new HashSet<Guid>();
      foreach (var row in rows)
      {
        if (!row.Id.HasValue || row.Id == Guid.Empty || !ids.Add(row.Id.Value))
        {
          throw new InvalidOperationException(
            "CategoryTare: Id thiếu, không hợp lệ hoặc trùng lặp.");
        }
        if (row.TypeTare != (int)iSoft.Database.EnumData.EnumTypeDataLocation.Truck &&
            row.TypeTare != (int)iSoft.Database.EnumData.EnumTypeDataLocation.Good)
        {
          throw new InvalidOperationException(
            $"CategoryTare {row.Id}: TypeTare phải bằng 1 hoặc 2.");
        }
      }

      // API trả cả bản ghi xóa khi IsDeleted=true. Dùng tập active làm snapshot
      // để ServerSnapshot áp dụng xóa mềm cho các Id đã bị server xóa.
      var activeRows = rows.Where(row => row.IsDelete != true).ToList();
      return SyncAsync<ListDatumCategoryTare, Tare>(activeRows, activeRows.Count,
        (s, l) =>
        {
          l.Name = s.Name;
          l.Code = s.SerialCode;
          l.Description = s.Description;
          l.Value = s.WeightTare;
          l.EnumTypeData = (iSoft.Database.EnumData.EnumTypeDataLocation)s.TypeTare!.Value;
        }, token,
        filterLocals: query => query.Where(tare =>
          tare.EnumTypeData != iSoft.Database.EnumData.EnumTypeDataLocation.All));
    }

    public static Task<MasterDataChangedEventArgs?> SyncStationsAsync(StationAPI response, CancellationToken token = default) =>
      SyncAsync<ListDatumStation, Station>(response.Data?.ListData, response.Data?.TotalRecord,
        (s, l) => { l.Name = s.Name; l.Description = s.Description; }, token);

    public static Task<MasterDataChangedEventArgs?> SyncWarehousesAsync(WarehouseAPI response, CancellationToken token = default) =>
      SyncAsync<ListDatumWarehouse, Warehouse>(response.Data?.ListData, response.Data?.TotalRecord,
        (s, l) => { l.Name = s.Name; l.Description = s.Description; }, token);

    public static Task<MasterDataChangedEventArgs?> SyncTypeGoodsAsync(TypeGoodsAPI response, CancellationToken token = default) =>
      SyncAsync<ListDatumTypeGoods, Product>(response.Data?.ListData, response.Data?.TotalRecord,
        (s, l) =>
        {
          l.Name = s.Name;
          l.Code = s.SerialCode;
          l.Description = s.Description;
          l.EnumTypeData = iSoft.Database.EnumData.EnumTypeDataLocation.Truck;
        }, token,
        initializeAdded: product =>
          product.EnumTypeData = iSoft.Database.EnumData.EnumTypeDataLocation.Truck,
        filterLocals: query => query.Where(product =>
          product.EnumTypeData == iSoft.Database.EnumData.EnumTypeDataLocation.Truck));

    public static Task<MasterDataChangedEventArgs?> SyncProductGroupsAsync(ProductGroupAPI response, CancellationToken token = default) =>
      SyncAsync<ListDatumProductGroup, ProductGroup>(response.Data?.ListData, response.Data?.TotalRecord,
        (s, l) => { l.Name = s.Name; l.Description = s.Description; }, token);

    public static Task<MasterDataChangedEventArgs?> SyncClientsAsync(ClientAPI response, CancellationToken token = default) =>
      SyncAsync<ListDatumClient, Client>(response.Data?.ListData, response.Data?.TotalRecord,
        (s, l) => { l.Name = s.Name; l.Description = s.Description; }, token);

    public static Task<MasterDataChangedEventArgs?> SyncDeliveriesAsync(DeliveryAPI response, CancellationToken token = default) =>
      SyncAsync<ListDatumDelivery, Delivery>(response.Data?.ListData, response.Data?.TotalRecord,
        (s, l) =>
        {
          l.Name = s.Name;
          l.OfficeAddress = s.OfficeAddress;
          l.PhoneForOfficeAddress = s.PhoneForOfficeAddress;
          l.AgentAddress = s.AgentAddress;
          l.PhoneForAgentAddress = s.PhoneForAgentAddress;
          l.Description = s.Description;
        }, token);

    public static Task<MasterDataChangedEventArgs?> SyncUsersAsync(UserAPI response, CancellationToken token = default) =>
      SyncAsync<ListDatumUser, User>(response.Data?.ListData, response.Data?.TotalRecord,
        (s, l) =>
        {
          l.DisplayName = s.DisplayName;
          l.FullName = string.IsNullOrWhiteSpace(s.DisplayName)
            ? s.Username ?? string.Empty
            : s.DisplayName;
          l.Username = s.Username ?? string.Empty;
          l.Password = s.Password;
          l.EmployeeCode = s.EmployeeCode;
          l.IdCardCode = s.IdCardCode;
          l.EnableFlag = s?.EnableFlag??false;
          l.Role = JsonConvert.SerializeObject(s.Permission ?? new List<string>());
        }, token,
        initializeAdded: user => user.CreatedAt = DateTime.UtcNow);

    public static Task<MasterDataChangedEventArgs?> SyncDocumentsAsync(
      DocumentAPI response,
      CancellationToken token = default) =>
      DocumentDataServerSyncService.SyncAsync(response, token);

    public static Task<MasterDataChangedEventArgs?> SyncProductsAsync(ProductAPI response, CancellationToken token = default)
    {
      var groups = new Dictionary<Guid, Guid>();

      static Guid? GetProductGroupId(ListDatumProduct product)
      {
        Guid? groupId = product.ItemProductGroup?.Id;
        if (string.IsNullOrWhiteSpace(product.ProductGroupId))
          return groupId;

        if (!Guid.TryParse(product.ProductGroupId, out var parsed) || parsed == Guid.Empty)
          throw new InvalidOperationException($"Product {product.Id}: ProductGroupId không hợp lệ.");
        if (groupId.HasValue && groupId != parsed)
          throw new InvalidOperationException($"Product {product.Id}: thông tin nhóm không nhất quán.");

        return parsed;
      }

      return SyncAsync<ListDatumProduct, Product>(response.Data?.ListData, response.Data?.TotalRecord,
        (s, l) =>
        {
          Guid? groupId = GetProductGroupId(s);
          Guid? localGroupId = null;
          if (groupId.HasValue)
            localGroupId = groups[groupId.Value];
          l.Name = s.Name;
          l.Code = s.SerialCode;
          l.Description = s.Description;
          l.ProductGroupId = localGroupId;
          l.EnumTypeData = iSoft.Database.EnumData.EnumTypeDataLocation.Good;
          if (s.WasteType.HasValue &&
              Enum.IsDefined(typeof(iSoft.Database.EnumData.EnumWasteType), s.WasteType.Value))
          {
            l.EnumWasteType = (iSoft.Database.EnumData.EnumWasteType)s.WasteType.Value;
          }
        }, token,
        async db =>
        {
          groups = await db.Set<ProductGroup>()
            .Where(x => x.IdSrc.HasValue && x.IdSrc != Guid.Empty && !x.DeletedFlag)
            .ToDictionaryAsync(x => x.IdSrc!.Value, x => x.Id, token).ConfigureAwait(false);
        },
        s =>
        {
          Guid? groupId = GetProductGroupId(s);
          return !groupId.HasValue || groups.ContainsKey(groupId.Value);
        },
        initializeAdded: product =>
          product.EnumTypeData = iSoft.Database.EnumData.EnumTypeDataLocation.Good,
        filterLocals: query => query.Where(product =>
          product.EnumTypeData == iSoft.Database.EnumData.EnumTypeDataLocation.Good));
    }

    private static async Task<MasterDataChangedEventArgs?> SyncAsync<TSource, TEntity>(
      List<TSource>? rows, int? total, Action<TSource, TEntity> map,
      CancellationToken token, Func<MySqlDbContext, Task>? prepare = null,
      Func<TSource, bool>? shouldSync = null,
      Action<TEntity>? initializeAdded = null,
      Func<IQueryable<TEntity>, IQueryable<TEntity>>? filterLocals = null)
      where TSource : IServerRecord
      where TEntity : BaseModel, new()
    {
      ServerSnapshot.Validate(rows, total);
      await SyncLock.WaitAsync(token).ConfigureAwait(false);
      try
      {
        await using var db = new MySqlDbContext();
        if (prepare != null)
          await prepare(db).ConfigureAwait(false);
        var sourceIds = rows!.Select(row => row.Id!.Value).ToList();
        var localQuery = db.Set<TEntity>().AsQueryable();
        if (filterLocals != null)
          localQuery = filterLocals(localQuery);
        var locals = await localQuery
          .Where(x => (x.IdSrc.HasValue && x.IdSrc != Guid.Empty) || sourceIds.Contains(x.Id))
          .ToListAsync(token).ConfigureAwait(false);
        var rowsToSync = shouldSync == null
          ? rows!
          : rows!.Where(shouldSync).ToList();
        var snapshotIds = shouldSync == null
          ? null
          : rows!.Select(row => row.Id!.Value);
        var added = ServerSnapshot.Apply(rowsToSync, locals, map, token, snapshotIds);
        if (initializeAdded != null)
        {
          foreach (var entity in added)
            initializeAdded(entity);
        }
        db.Set<TEntity>().AddRange(added);

        db.ChangeTracker.DetectChanges();

        // Không ghi DB khi server chỉ trả timestamp khác nhưng dữ liệu entity giữ nguyên.
        // Việc này cũng ngăn EntityChanged phát ở mọi chu kỳ vì sai lệch timestamp.
        foreach (var entry in db.ChangeTracker.Entries<TEntity>()
          .Where(entry => entry.State == EntityState.Modified))
        {
          var hasEntityDataChange = entry.Properties.Any(property =>
            property.IsModified &&
            property.Metadata.Name != nameof(BaseModel.CreatedAt) &&
            property.Metadata.Name != nameof(BaseModel.UpdatedAt));

          if (!hasEntityDataChange)
            entry.State = EntityState.Unchanged;
        }

        var changedEntries = db.ChangeTracker.Entries<TEntity>()
          .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified)
          .ToList();
        if (changedEntries.Count == 0)
          return null;

        var addedEntities = changedEntries
          .Where(entry => entry.State == EntityState.Added)
          .Select(entry => entry.Entity)
          .ToList();
        var deletedEntities = changedEntries
          .Where(entry => entry.State == EntityState.Modified &&
            entry.Property(nameof(BaseModel.DeletedFlag)).IsModified &&
            entry.Entity.DeletedFlag)
          .Select(entry => entry.Entity)
          .ToList();
        var deletedIds = deletedEntities.Select(entity => entity.Id).ToHashSet();
        var updatedEntities = changedEntries
          .Where(entry => entry.State == EntityState.Modified && !deletedIds.Contains(entry.Entity.Id))
          .Select(entry => entry.Entity)
          .ToList();

        // Một lần SaveChanges cho cả thêm, cập nhật, xóa mềm; lỗi thì không lưu lượt này.
        var affectedRows = await db.SaveChangesAsync(token).ConfigureAwait(false);
        if (affectedRows == 0)
          return null;

        return new MasterDataChangedEventArgs(
          typeof(TEntity),
          addedEntities.Select(entity => entity.Id).ToList(),
          updatedEntities.Select(entity => entity.Id).ToList(),
          deletedEntities.Select(entity => entity.Id).ToList(),
          affectedRows);
      }
      finally
      {
        SyncLock.Release();
      }
    }
  }
}
