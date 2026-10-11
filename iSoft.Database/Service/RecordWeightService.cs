using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iSoft.Database.Service
{
  public class RecordWeightService
  {
    public async Task<List<RecordWeight>> GetAllAsync(bool IsContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordWeightRepository(context);
      return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
    }

    public async Task<RecordWeight> AddOrUpdateAsync(RecordWeight recordWeight)
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordWeightRepository(context);
      return await repository.AddOrUpdateAsync(recordWeight).ConfigureAwait(false);
    }

    public async Task<List<RecordWeight>> GetReportAsync(
      DateTime fromUtc,
      DateTime toUtcExclusive,
      string? searchKey,
      int dataTypeIndex = 1)
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordWeightRepository(context);
      return await repository.GetReportAsync(
        fromUtc,
        toUtcExclusive,
        searchKey,
        dataTypeIndex).ConfigureAwait(false);
    }

    public async Task<(List<RecordWeight> Records, int TotalRecords)> GetReportPageAsync(
      DateTime fromUtc,
      DateTime toUtcExclusive,
      string? searchKey,
      int pageNumber,
      int pageSize)
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordWeightRepository(context);
      return await repository.GetReportPageAsync(
        fromUtc,
        toUtcExclusive,
        searchKey,
        pageNumber,
        pageSize).ConfigureAwait(false);
    }

  }
}
