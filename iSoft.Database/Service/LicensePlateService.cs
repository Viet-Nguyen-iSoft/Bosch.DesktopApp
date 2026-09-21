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
  }
}
