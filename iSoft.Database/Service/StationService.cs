using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;

namespace iSoft.Database.Service
{
  public class StationService
  {
    public async Task<List<Station>> GetAllAsync(bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new StationRepository(context);
      return await repository.GetAllAsync(isContainDelete).ConfigureAwait(false);
    }
    public async Task<Station?> GetFirstDataStation(bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new StationRepository(context);
      return await repository.GetFirstDataStationAsync().ConfigureAwait(false);
    }
    public async Task<Station?> GetByCodeAsync(Guid? id)
    {
      await using var context = new MySqlDbContext();
      var repository = new StationRepository(context);
      return await repository.GetByCodeAsync(id).ConfigureAwait(false);
    }

    public async Task<Station> AddOrUpdateAsync(Station station)
    {
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, station).ConfigureAwait(false);
    }
  }
}
