using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using static iSoft.Database.EnumData;
using RecordModel = iSoft.Database.Models.Record;

namespace iSoft.Database.Repositorys
{
  public sealed class RecordRepository : GenericRepository<RecordModel, CommonDbContext>
  {
    private const string SearchCollation = "utf8mb4_unicode_ci";

    public RecordRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<RecordModel>> GetTruckReportAsync(
      DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
      int statusFilterIndex = 0)
    {
      return BuildTruckReportQuery(fromUtc, toUtcExclusive, searchKey,
          statusFilterIndex)
        .OrderBy(record => record.CreatedAt)
        .ThenBy(record => record.Id)
        .ToListAsync();
    }

    public async Task<(List<RecordModel> Records, int TotalRecords)>
      GetTruckReportPageAsync(
        DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
        int statusFilterIndex, int pageNumber, int pageSize)
    {
      pageNumber = Math.Max(1, pageNumber);
      pageSize = Math.Max(1, pageSize);
      var query = BuildTruckReportQuery(fromUtc, toUtcExclusive, searchKey,
        statusFilterIndex);
      var totalRecords = await query.CountAsync();
      var totalPages = Math.Max(1,
        (int)Math.Ceiling(totalRecords / (double)pageSize));
      pageNumber = Math.Min(pageNumber, totalPages);
      var records = await query
        .OrderBy(record => record.CreatedAt)
        .ThenBy(record => record.Id)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
      return (records, totalRecords);
    }

    public Task<List<RecordModel>> GetGoodsReportAsync(
      DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
      int dataTypeIndex = 1, Guid? productGroupId = null,
      EnumWasteType? wasteType = null)
    {
      return BuildGoodsReportQuery(fromUtc, toUtcExclusive, searchKey,
          dataTypeIndex, productGroupId, wasteType)
        .OrderBy(record => record.CreatedAt)
        .ThenBy(record => record.Id)
        .ToListAsync();
    }

    public async Task<(List<RecordModel> Records, int TotalRecords)>
      GetGoodsReportPageAsync(
        DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
        int pageNumber, int pageSize, int dataTypeIndex = 1,
        Guid? productGroupId = null, EnumWasteType? wasteType = null)
    {
      pageNumber = Math.Max(1, pageNumber);
      pageSize = Math.Max(1, pageSize);
      var query = BuildGoodsReportQuery(fromUtc, toUtcExclusive, searchKey,
        dataTypeIndex, productGroupId, wasteType);
      var totalRecords = await query.CountAsync();
      var totalPages = Math.Max(1,
        (int)Math.Ceiling(totalRecords / (double)pageSize));
      pageNumber = Math.Min(pageNumber, totalPages);
      var records = await query
        .OrderByDescending(record => record.TypeData == EnumTypeDataLocation.Truck
          ? record.UpdatedAt : record.CreatedAt)
        .ThenBy(record => record.Id)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
      return (records, totalRecords);
    }

    private IQueryable<RecordModel> BaseReportQuery()
    {
      return Context.Set<RecordModel>()
        .AsNoTracking()
        .Include(record => record.Client)
        .Include(record => record.TypeGoods)
          .ThenInclude(product => product.ProductGroup)
        .Include(record => record.CategoryTare)
        .Include(record => record.Warehouse)
        .Include(record => record.User)
        .Include(record => record.Station);
    }

    private IQueryable<RecordModel> BuildTruckReportQuery(
      DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
      int statusFilterIndex)
    {
      var query = BaseReportQuery().Where(record =>
        record.TypeData == EnumTypeDataLocation.Truck &&
        record.UpdatedAt >= fromUtc && record.UpdatedAt < toUtcExclusive);
      query = statusFilterIndex switch
      {
        1 => query.Where(record => !record.DeletedFlag &&
          (record.EnumTypeDataTruck == EnumTypeDataTruck.WeightedTime01 ||
           record.EnumTypeDataTruck == EnumTypeDataTruck.DoneTime01 ||
           record.EnumTypeDataTruck == EnumTypeDataTruck.WeightedTime02)),
        2 => query.Where(record => !record.DeletedFlag &&
          record.EnumTypeDataTruck == EnumTypeDataTruck.DoneTime02),
        3 => query.Where(record => record.DeletedFlag),
        _ => query
      };
      return ApplySearch(query, searchKey);
    }

    private IQueryable<RecordModel> BuildGoodsReportQuery(
      DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
      int dataTypeIndex, Guid? productGroupId, EnumWasteType? wasteType)
    {
      var query = BaseReportQuery().Where(record =>
        (record.TypeData == EnumTypeDataLocation.Good &&
         record.CreatedAt >= fromUtc && record.CreatedAt < toUtcExclusive) ||
        (record.TypeData == EnumTypeDataLocation.Truck &&
         record.UpdatedAt >= fromUtc && record.UpdatedAt < toUtcExclusive));
      query = dataTypeIndex switch
      {
        2 => query.Where(record => record.DeletedFlag),
        0 => query,
        _ => query.Where(record => !record.DeletedFlag)
      };
      if (productGroupId.HasValue)
        query = query.Where(record => record.TypeGoods != null &&
          record.TypeGoods.ProductGroupId == productGroupId.Value);
      if (wasteType.HasValue)
        query = query.Where(record => record.TypeGoods != null &&
          record.TypeGoods.EnumWasteType == wasteType.Value);
      return ApplySearch(query, searchKey);
    }

    private static IQueryable<RecordModel> ApplySearch(
      IQueryable<RecordModel> query, string? searchKey)
    {
      if (string.IsNullOrWhiteSpace(searchKey))
        return query;
      return query.Where(record =>
        (record.NoLabelAuto != null && EF.Functions.Collate(
          record.NoLabelAuto, SearchCollation).Contains(searchKey)) ||
        (record.NoLabelManual != null && EF.Functions.Collate(
          record.NoLabelManual, SearchCollation).Contains(searchKey)) ||
        (record.LicensePlate != null && EF.Functions.Collate(
          record.LicensePlate, SearchCollation).Contains(searchKey)) ||
        (record.NameDriver != null && EF.Functions.Collate(
          record.NameDriver, SearchCollation).Contains(searchKey)) ||
        (record.IdCard != null && EF.Functions.Collate(
          record.IdCard, SearchCollation).Contains(searchKey)) ||
        (record.Note != null && EF.Functions.Collate(
          record.Note, SearchCollation).Contains(searchKey)) ||
        (record.Client != null && record.Client.Name != null &&
          EF.Functions.Collate(record.Client.Name, SearchCollation)
            .Contains(searchKey)) ||
        (record.TypeGoods != null && record.TypeGoods.Name != null &&
          EF.Functions.Collate(record.TypeGoods.Name, SearchCollation)
            .Contains(searchKey)) ||
        (record.TypeGoods != null && record.TypeGoods.ProductGroup != null &&
          record.TypeGoods.ProductGroup.Name != null && EF.Functions.Collate(
            record.TypeGoods.ProductGroup.Name, SearchCollation)
            .Contains(searchKey)) ||
        (record.Warehouse != null && record.Warehouse.Name != null &&
          EF.Functions.Collate(record.Warehouse.Name, SearchCollation)
            .Contains(searchKey)) ||
        (record.Station != null && record.Station.Name != null &&
          EF.Functions.Collate(record.Station.Name, SearchCollation)
            .Contains(searchKey)));
    }
  }
}
