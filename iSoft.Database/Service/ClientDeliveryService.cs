using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using static iSoft.Database.EnumData;

namespace iSoft.Database.Service
{
  public class ClientDeliveryService
  {
    public async Task<List<ClientDelivery>> GetAllAsync(
      EnumTypeDataLocation enumTypeData,
      bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new ClientDeliveryRepository(context);
      return await repository.GetAllAsync(enumTypeData, isContainDelete)
        .ConfigureAwait(false);
    }

    public async Task<ClientDelivery> AddOrUpdateAsync(
      ClientDelivery item,
      EnumTypeDataLocation enumTypeData)
    {
      ArgumentNullException.ThrowIfNull(item);
      if (enumTypeData == EnumTypeDataLocation.All)
        throw new ArgumentOutOfRangeException(nameof(enumTypeData));

      item.EnumTypeData = enumTypeData;
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, item)
        .ConfigureAwait(false);
    }
  }
}
