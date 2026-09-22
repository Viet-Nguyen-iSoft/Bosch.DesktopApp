using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Repositorys
{
  public class RecordTruckRepository : GenericRepository<RecordTruck, CommonDbContext>
  {
    public RecordTruckRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<RecordTruck>> GetAllAsync(bool IsContainDelete = false)
    {
      var query = Context.Set<RecordTruck>()
        .AsNoTracking()
        .Include(x => x.Client)
        .Include(x => x.TypeGoods)
        .Include(x => x.Warehouse)
        .Include(x => x.User)
        .Include(x => x.Station)
        .AsQueryable();
      if (!IsContainDelete)
        query = query.Where(x => !x.DeletedFlag);
      return query.ToListAsync();
    }

    public Task<List<RecordTruck>> GetReportAsync(
      DateTime fromUtc,
      DateTime toUtcExclusive,
      string? searchKey,
      int statusFilterIndex = 0,
      int typeFilterIndex = 0)
    {
      var query = Context.Set<RecordTruck>()
        .AsNoTracking()
        .Include(record => record.Client)
        .Include(record => record.TypeGoods)
        .Include(record => record.Warehouse)
        .Include(record => record.User)
        .Include(record => record.Station)
        .Where(record => record.UpdatedAt >= fromUtc && record.UpdatedAt < toUtcExclusive);

      query = typeFilterIndex switch
      {
        1 => query.Where(record => !record.DeletedFlag),
        2 => query.Where(record => record.DeletedFlag),
        _ => query
      };

      query = statusFilterIndex switch
      {
        1 => query.Where(record =>
          record.EnumTypeDataTruck == EnumData.EnumTypeDataTruck.WeightedTime01 ||
          record.EnumTypeDataTruck == EnumData.EnumTypeDataTruck.DoneTime01 ||
          record.EnumTypeDataTruck == EnumData.EnumTypeDataTruck.WeightedTime02),
        2 => query.Where(record =>
          record.EnumTypeDataTruck == EnumData.EnumTypeDataTruck.DoneTime02),
        _ => query
      };

      if (!string.IsNullOrWhiteSpace(searchKey))
      {
        query = query.Where(record =>
          (record.NoLabelAuto != null && record.NoLabelAuto.Contains(searchKey)) ||
          (record.NoLabelManual != null && record.NoLabelManual.Contains(searchKey)) ||
          (record.LicensePlate != null && record.LicensePlate.Contains(searchKey)) ||
          (record.NameDriver != null && record.NameDriver.Contains(searchKey)) ||
          (record.IdCard != null && record.IdCard.Contains(searchKey)) ||
          (record.Document != null && record.Document.Contains(searchKey)) ||
          (record.Client != null && record.Client.Name != null && record.Client.Name.Contains(searchKey)) ||
          (record.TypeGoods != null && record.TypeGoods.Name != null && record.TypeGoods.Name.Contains(searchKey)) ||
          (record.Warehouse != null && record.Warehouse.Name != null && record.Warehouse.Name.Contains(searchKey)) ||
          (record.Station != null && record.Station.Name != null && record.Station.Name.Contains(searchKey)));
      }

      return query
        .OrderByDescending(record => record.CreatedAt)
        .ThenByDescending(record => record.Id)
        .ToListAsync();
    }

    public Task<List<RecordTruck>> GetFirstWeighingRecordsAsync()
    {
      return Context.Set<RecordTruck>()
        .AsNoTracking()
        .Where(record => !record.DeletedFlag &&
          record.EnumTypeDataTruck == EnumData.EnumTypeDataTruck.DoneTime01)
        .Include(record => record.Client)
        .Include(record => record.TypeGoods)
        .Include(record => record.Warehouse)
        .OrderByDescending(record => record.CreatedAt)
        .ThenByDescending(record => record.Id)
        .ToListAsync();
    }

    public Task<RecordTruck?> GetDetailByIdAsync(Guid id, bool isContainDelete = false)
    {
      var query = Context.Set<RecordTruck>()
        .AsNoTracking()
        .Include(record => record.Client)
        .Include(record => record.TypeGoods)
        .Include(record => record.Warehouse)
        .Include(record => record.User)
        .Include(record => record.Station)
        .Include(record => record.RecordWeights!)
          .ThenInclude(recordWeight => recordWeight.Product)
            .ThenInclude(product => product.ProductGroup)
        .AsQueryable();

      if (!isContainDelete)
        query = query.Where(record => !record.DeletedFlag);

      return query.FirstOrDefaultAsync(record => record.Id == id);
    }

    public Task<RecordTruck?> GetPendingByLicensePlateAsync(string licensePlate)
    {
      var normalizedLicensePlate = LicensePlateRepository.Normalize(licensePlate);
      if (normalizedLicensePlate == null)
        return Task.FromResult<RecordTruck?>(null);

      return Context.Set<RecordTruck>()
        .AsNoTracking()
        .Include(record => record.Client)
        .Include(record => record.TypeGoods)
        .Include(record => record.Warehouse)
        .Include(record => record.User)
        .Include(record => record.Station)
        .Where(record => !record.DeletedFlag &&
          record.LicensePlate == normalizedLicensePlate &&
          record.NetTime01 > 0 &&
          record.NetTime02 <= 0 &&
          (record.EnumTypeDataTruck == EnumData.EnumTypeDataTruck.DoneTime01 ||
           record.EnumTypeDataTruck == EnumData.EnumTypeDataTruck.WeightedTime02))
        .OrderByDescending(record => record.UpdatedAt)
        .ThenByDescending(record => record.Id)
        .FirstOrDefaultAsync();
    }

    public async Task<(RecordTruck Record, bool Exist, LicensePlate? LicensePlate)> AddOrUpdateAsync(RecordTruck recordTruck)
    {
      if (recordTruck == null)
      {
        throw new ArgumentNullException(nameof(recordTruck));
      }

      // Mọi thay đổi local cần được đưa vào hàng đợi đồng bộ lại.
      recordTruck.SyncFlag = false;

      await Context.Database.EnsureCreatedAsync();
      recordTruck.LicensePlate = LicensePlateRepository.Normalize(recordTruck.LicensePlate);

      var licensePlateRepository = new LicensePlateRepository((CommonDbContext)Context);
      var rsLicensePlate = await licensePlateRepository.EnsureExistsAsync(recordTruck.LicensePlate);

      var records = Context.Set<RecordTruck>();
      var existingRecord = recordTruck.Id == Guid.Empty
        ? null
        : await records.FindAsync(recordTruck.Id);

      if (existingRecord == null)
      {
        await records.AddAsync(recordTruck);
      }
      else
      {
        Context.Entry(existingRecord).CurrentValues.SetValues(recordTruck);
      }

      await Context.SaveChangesAsync();
      return (existingRecord ?? recordTruck, rsLicensePlate.Exist, rsLicensePlate.LicensePlate);
    }
  }
}
