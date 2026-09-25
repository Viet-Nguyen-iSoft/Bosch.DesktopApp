using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Service
{
  public class RecordTruckService
  {
    public async Task<List<RecordTruck>> GetAllAsync(bool IsContainDelete = false)
    {
      try
      {
        await using var context = new MySqlDbContext();
        var repository = new RecordTruckRepository(context);
        return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
      }
      catch (Exception)
      {
        throw;
      }
    }
    public async Task<List<RecordTruck>> GetFirstWeighingRecordsAsync()
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordTruckRepository(context);
      return await repository.GetFirstWeighingRecordsAsync().ConfigureAwait(false);
    }

    public async Task<RecordTruck?> GetDetailByIdAsync(Guid id, bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordTruckRepository(context);
      return await repository.GetDetailByIdAsync(id, isContainDelete).ConfigureAwait(false);
    }

    public async Task<List<RecordTruck>> GetReportAsync(
      DateTime fromUtc,
      DateTime toUtcExclusive,
      string? searchKey,
      int statusFilterIndex = 0)
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordTruckRepository(context);
      return await repository.GetReportAsync(
        fromUtc,
        toUtcExclusive,
        searchKey,
        statusFilterIndex).ConfigureAwait(false);
    }

    public async Task<(List<RecordTruck> Records, int TotalRecords)> GetReportPageAsync(
      DateTime fromUtc,
      DateTime toUtcExclusive,
      string? searchKey,
      int statusFilterIndex,
      int pageNumber,
      int pageSize)
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordTruckRepository(context);
      return await repository.GetReportPageAsync(
        fromUtc,
        toUtcExclusive,
        searchKey,
        statusFilterIndex,
        pageNumber,
        pageSize).ConfigureAwait(false);
    }

    public async Task<RecordTruck?> GetPendingByLicensePlateAsync(string licensePlate)
    {
      await using var context = new MySqlDbContext();
      var repository = new RecordTruckRepository(context);
      return await repository.GetPendingByLicensePlateAsync(licensePlate).ConfigureAwait(false);
    }

    public async Task<(RecordTruck Record, bool Exist, LicensePlate? LicensePlate)> AddOrUpdateAsync(RecordTruck recordTruck)
    {
      try
      {
        await using var context = new MySqlDbContext();
        var repository = new RecordTruckRepository(context);
        return await repository.AddOrUpdateAsync(recordTruck);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
