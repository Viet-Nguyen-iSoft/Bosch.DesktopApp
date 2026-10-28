using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using static iSoft.Database.EnumData;

namespace iSoft.Database.Service
{
  public sealed class TareService
  {
    public async Task<List<Tare>> GetAllAsync(
      EnumTypeDataLocation location,
      bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new TareRepository(context);
      return await repository.GetAllAsync(location, isContainDelete).ConfigureAwait(false);
    }

    public async Task<Tare> AddOrUpdateAsync(
      Tare tare,
      EnumTypeDataLocation location)
    {
      ArgumentNullException.ThrowIfNull(tare);
      if (location == EnumTypeDataLocation.All)
        throw new ArgumentOutOfRangeException(nameof(location));

      tare.EnumTypeData = location;
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, tare)
        .ConfigureAwait(false);
    }
  }
}
