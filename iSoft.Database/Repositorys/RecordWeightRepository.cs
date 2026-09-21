using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSoft.Database.Repositorys
{
  public class RecordWeightRepository : GenericRepository<RecordWeight, CommonDbContext>
  {
    public RecordWeightRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<RecordWeight>> GetAllAsync(bool IsContainDelete = false)
    {
      var query = Context.Set<RecordWeight>()
        .Include(record => record.Product)
          .ThenInclude(product => product.ProductGroup)
        .Include(record => record.CategoryTare)
        .Include(record => record.RecordTruck)
        .AsQueryable();

      if (!IsContainDelete)
        query = query.Where(record => !record.DeletedFlag);

      return query.ToListAsync();
    }

    public async Task<double> SumNetByRecordTruckIdAsync(Guid recordTruckId)
    {
      return await Context.Set<RecordWeight>()
        .Where(record => !record.DeletedFlag && record.RecordTruckId == recordTruckId)
        .SumAsync(record => (double?)record.Net) ?? 0.0;
    }
    public async Task<double> SumNetByRecordTruckIdAsync(string plate)
    {
      return await Context.Set<RecordWeight>()
        .Where(record => !record.DeletedFlag && record.LicensePlate == plate)
        .SumAsync(record => (double?)record.Net) ?? 0.0;
    }

    public async Task<RecordWeight> AddOrUpdateAsync(RecordWeight recordWeight)
    {
      if (recordWeight == null)
        throw new ArgumentNullException(nameof(recordWeight));

      // Mọi thay đổi local cần được đưa vào hàng đợi đồng bộ lại.
      recordWeight.SyncFlag = false;

      await Context.Database.EnsureCreatedAsync();
      var records = Context.Set<RecordWeight>();
      var existingRecord = recordWeight.Id == Guid.Empty
        ? null
        : await records.FindAsync(recordWeight.Id);

      if (existingRecord == null)
        await records.AddAsync(recordWeight);
      else
        Context.Entry(existingRecord).CurrentValues.SetValues(recordWeight);

      await Context.SaveChangesAsync();
      return existingRecord ?? recordWeight;
    }
  }
}
