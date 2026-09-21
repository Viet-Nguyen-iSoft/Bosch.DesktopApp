using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace iSoft.Database.Repositorys
{
  public class LicensePlateRepository : GenericRepository<LicensePlate, CommonDbContext>
  {
    public LicensePlateRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<LicensePlate>> GetAllAsync(bool IsContainDelete = false)
    {
      var query = Context.Set<LicensePlate>().AsQueryable();
      if (!IsContainDelete)
        query = query.Where(x => !x.DeletedFlag);
      return query.ToListAsync();
    }

    public async Task<(bool Exist, LicensePlate? LicensePlate)> EnsureExistsAsync(string? licensePlate)
    {
      var normalizedLicensePlate = Normalize(licensePlate);
      if (normalizedLicensePlate == null)
        return (true, null);

      var licensePlates = Context.Set<LicensePlate>();
      var existingLicensePlate = await licensePlates
        .FirstOrDefaultAsync(x => x.Plate == normalizedLicensePlate);

      if (existingLicensePlate != null)
      {
        if (existingLicensePlate.DeletedFlag)
        {
          existingLicensePlate.DeletedFlag = false;
          existingLicensePlate.EnableFlag = true;
          existingLicensePlate.SyncFlag = false;
          existingLicensePlate.UpdatedAt = DateTime.UtcNow;
        }

        return (true,existingLicensePlate);
      }

      var newLicensePlate = new LicensePlate
      {
        Plate = normalizedLicensePlate,
        CreatedAt = DateTime.UtcNow,
        EnableFlag = true,
        SyncFlag = false
      };

      await licensePlates.AddAsync(newLicensePlate);
      return (false, newLicensePlate);
    }

    public static string? Normalize(string? licensePlate)
    {
      if (string.IsNullOrWhiteSpace(licensePlate))
        return null;

      return Regex.Replace(licensePlate.Trim(), @"\s+", string.Empty)
        .ToUpperInvariant();
    }
  }
}
