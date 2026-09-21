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
        .Include(x => x.Client)
        .Include(x => x.TypeGoods)
        .Include(x => x.Warehouse)
        .Include(x => x.Station)
        .AsQueryable();
      if (!IsContainDelete)
        query = query.Where(x => !x.DeletedFlag);
      return query.ToListAsync();
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

    public async Task<RecordTruck> AddOrUpdateAsync(RecordTruck recordTruck)
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
      await licensePlateRepository.EnsureExistsAsync(recordTruck.LicensePlate);

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
      return existingRecord ?? recordTruck;
    }
  }
}
