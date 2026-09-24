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
    private const string SearchCollation = "utf8mb4_unicode_ci";
    public RecordWeightRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<RecordWeight>> GetAllAsync(bool IsContainDelete = false)
    {
      var query = Context.Set<RecordWeight>()
        .AsNoTracking()
        .Include(record => record.Product)
          .ThenInclude(product => product.ProductGroup)
        .Include(record => record.CategoryTare)
        .Include(record => record.RecordTruck)
        .Include(record => record.User)
        .AsQueryable();

      if (!IsContainDelete)
        query = query.Where(record => !record.DeletedFlag);

      return query.ToListAsync();
    }

    public Task<List<RecordWeight>> GetReportAsync(
      DateTime fromUtc,
      DateTime toUtcExclusive,
      string? searchKey)
    {
      var query = Context.Set<RecordWeight>()
        .AsNoTracking()
        .Include(record => record.Product)
          .ThenInclude(product => product.ProductGroup)
        .Include(record => record.CategoryTare)
        .Include(record => record.RecordTruck)
        .Include(record => record.User)
        .Where(record =>
          !record.DeletedFlag &&
          record.CreatedAt >= fromUtc &&
          record.CreatedAt < toUtcExclusive);

      if (!string.IsNullOrWhiteSpace(searchKey))
      {
        query = query.Where(record =>
          (record.Product != null && record.Product.Code != null && EF.Functions.Collate(record.Product.Code, SearchCollation).Contains(searchKey)) ||
          (record.Product != null && record.Product.Name != null && EF.Functions.Collate(record.Product.Name, SearchCollation).Contains(searchKey)) ||
          (record.Product != null && record.Product.ProductGroup != null &&
            record.Product.ProductGroup.Code != null && EF.Functions.Collate(record.Product.ProductGroup.Code, SearchCollation).Contains(searchKey)) ||
          (record.Product != null && record.Product.ProductGroup != null &&
            record.Product.ProductGroup.Name != null && EF.Functions.Collate(record.Product.ProductGroup.Name, SearchCollation).Contains(searchKey)) ||
          (record.CategoryTare != null && record.CategoryTare.Code != null && EF.Functions.Collate(record.CategoryTare.Code, SearchCollation).Contains(searchKey)) ||
          (record.CategoryTare != null && record.CategoryTare.Name != null && EF.Functions.Collate(record.CategoryTare.Name, SearchCollation).Contains(searchKey)) ||
          (record.LicensePlate != null && EF.Functions.Collate(record.LicensePlate, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.NoLabelAuto != null &&
            EF.Functions.Collate(record.RecordTruck.NoLabelAuto, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.NoLabelManual != null &&
            EF.Functions.Collate(record.RecordTruck.NoLabelManual, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.LicensePlate != null &&
            EF.Functions.Collate(record.RecordTruck.LicensePlate, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.NameDriver != null &&
            EF.Functions.Collate(record.RecordTruck.NameDriver, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.IdCard != null &&
            EF.Functions.Collate(record.RecordTruck.IdCard, SearchCollation).Contains(searchKey)));
      }

      return query
        .OrderByDescending(record => record.CreatedAt)
        .ThenByDescending(record => record.Id)
        .ToListAsync();
    }

    public async Task<(List<RecordWeight> Records, int TotalRecords)> GetReportPageAsync(
      DateTime fromUtc,
      DateTime toUtcExclusive,
      string? searchKey,
      int pageNumber,
      int pageSize)
    {
      pageNumber = Math.Max(1, pageNumber);
      pageSize = Math.Max(1, pageSize);

      var query = BuildReportQuery(fromUtc, toUtcExclusive, searchKey);
      var totalRecords = await query.CountAsync();
      var totalPages = Math.Max(1, (int)Math.Ceiling(totalRecords / (double)pageSize));
      pageNumber = Math.Min(pageNumber, totalPages);
      var records = await query
        .OrderByDescending(record => record.CreatedAt)
        .ThenByDescending(record => record.Id)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      return (records, totalRecords);
    }

    private IQueryable<RecordWeight> BuildReportQuery(
      DateTime fromUtc,
      DateTime toUtcExclusive,
      string? searchKey)
    {
      var query = Context.Set<RecordWeight>()
        .AsNoTracking()
        .Include(record => record.Product)
          .ThenInclude(product => product.ProductGroup)
        .Include(record => record.CategoryTare)
        .Include(record => record.RecordTruck)
        .Include(record => record.User)
        .Where(record =>
          !record.DeletedFlag &&
          record.CreatedAt >= fromUtc &&
          record.CreatedAt < toUtcExclusive);

      if (!string.IsNullOrWhiteSpace(searchKey))
      {
        query = query.Where(record =>
          (record.Product != null && record.Product.Code != null && EF.Functions.Collate(record.Product.Code, SearchCollation).Contains(searchKey)) ||
          (record.Product != null && record.Product.Name != null && EF.Functions.Collate(record.Product.Name, SearchCollation).Contains(searchKey)) ||
          (record.Product != null && record.Product.ProductGroup != null &&
            record.Product.ProductGroup.Code != null && EF.Functions.Collate(record.Product.ProductGroup.Code, SearchCollation).Contains(searchKey)) ||
          (record.Product != null && record.Product.ProductGroup != null &&
            record.Product.ProductGroup.Name != null && EF.Functions.Collate(record.Product.ProductGroup.Name, SearchCollation).Contains(searchKey)) ||
          (record.CategoryTare != null && record.CategoryTare.Code != null && EF.Functions.Collate(record.CategoryTare.Code, SearchCollation).Contains(searchKey)) ||
          (record.CategoryTare != null && record.CategoryTare.Name != null && EF.Functions.Collate(record.CategoryTare.Name, SearchCollation).Contains(searchKey)) ||
          (record.LicensePlate != null && EF.Functions.Collate(record.LicensePlate, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.NoLabelAuto != null &&
            EF.Functions.Collate(record.RecordTruck.NoLabelAuto, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.NoLabelManual != null &&
            EF.Functions.Collate(record.RecordTruck.NoLabelManual, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.LicensePlate != null &&
            EF.Functions.Collate(record.RecordTruck.LicensePlate, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.NameDriver != null &&
            EF.Functions.Collate(record.RecordTruck.NameDriver, SearchCollation).Contains(searchKey)) ||
          (record.RecordTruck != null && record.RecordTruck.IdCard != null &&
            EF.Functions.Collate(record.RecordTruck.IdCard, SearchCollation).Contains(searchKey)));
      }

      return query;
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
