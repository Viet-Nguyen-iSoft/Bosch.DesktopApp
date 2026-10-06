using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;

namespace iSoft.Database.Service
{
  public class TareTruckService
  {
    public async Task<List<TareTruck>> GetAllAsync(bool IsContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new TareTruckRepository(context);
      return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
    }

    public async Task<TareTruck> AddOrUpdateAsync(TareTruck tareTruck)
    {
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, tareTruck).ConfigureAwait(false);
    }
  }
}
