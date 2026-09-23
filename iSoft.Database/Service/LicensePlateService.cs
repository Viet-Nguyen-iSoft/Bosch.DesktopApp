using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;

namespace iSoft.Database.Service
{
  public class LicensePlateService
  {
    public async Task<List<LicensePlate>> GetAllAsync(bool IsContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new LicensePlateRepository(context);
      return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
    }

    public async Task<(bool Exist, LicensePlate? LicensePlate)> EnsureExistsAsync(
      string? licensePlate)
    {
      await using var context = new MySqlDbContext();
      var repository = new LicensePlateRepository(context);
      var result = await repository.EnsureExistsAsync(licensePlate).ConfigureAwait(false);
      await context.SaveChangesAsync().ConfigureAwait(false);
      return result;
    }
  }
}
