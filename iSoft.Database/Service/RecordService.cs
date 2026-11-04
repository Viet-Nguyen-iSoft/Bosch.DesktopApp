using iSoft.Database.DbContexts;
using iSoft.Database.Repositorys;
using static iSoft.Database.EnumData;
using RecordModel = iSoft.Database.Models.Record;

namespace iSoft.Database.Service
{
  public sealed class RecordService
  {
    public async Task<RecordModel> AddOrUpdateAsync(RecordModel record)
    {
      await using var context = new MySqlDbContext();
      return await new RecordRepository(context).AddOrUpdateAsync(record)
        .ConfigureAwait(false);
    }

    public async Task<List<RecordModel>> GetTruckReportAsync(
      DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
      int statusFilterIndex = 0)
    {
      await using var context = new MySqlDbContext();
      return await new RecordRepository(context).GetTruckReportAsync(
        fromUtc, toUtcExclusive, searchKey, statusFilterIndex)
        .ConfigureAwait(false);
    }

    public async Task<(List<RecordModel> Records, int TotalRecords)>
      GetTruckReportPageAsync(
        DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
        int statusFilterIndex, int pageNumber, int pageSize)
    {
      await using var context = new MySqlDbContext();
      return await new RecordRepository(context).GetTruckReportPageAsync(
        fromUtc, toUtcExclusive, searchKey, statusFilterIndex,
        pageNumber, pageSize).ConfigureAwait(false);
    }

    public async Task<List<RecordModel>> GetGoodsReportAsync(
      DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
      int dataTypeIndex = 1, Guid? productGroupId = null,
      EnumWasteType? wasteType = null)
    {
      await using var context = new MySqlDbContext();
      return await new RecordRepository(context).GetGoodsReportAsync(
        fromUtc, toUtcExclusive, searchKey, dataTypeIndex,
        productGroupId, wasteType).ConfigureAwait(false);
    }

    public async Task<(List<RecordModel> Records, int TotalRecords)>
      GetGoodsReportPageAsync(
        DateTime fromUtc, DateTime toUtcExclusive, string? searchKey,
        int pageNumber, int pageSize, int dataTypeIndex = 1,
        Guid? productGroupId = null, EnumWasteType? wasteType = null)
    {
      await using var context = new MySqlDbContext();
      return await new RecordRepository(context).GetGoodsReportPageAsync(
        fromUtc, toUtcExclusive, searchKey, pageNumber, pageSize,
        dataTypeIndex, productGroupId, wasteType).ConfigureAwait(false);
    }
  }
}
