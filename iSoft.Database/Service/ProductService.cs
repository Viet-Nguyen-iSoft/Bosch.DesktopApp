using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Database.EnumData;

namespace iSoft.Database.Service
{
  public class ProductService
  {
    public async Task<List<Product>> GetAllAsync(
      EnumTypeDataLocation location,
      bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new ProductRepository(context);
      return await repository.GetAllAsync(location, isContainDelete).ConfigureAwait(false);
    }

    public async Task<Product> AddOrUpdateAsync(
      Product product,
      EnumTypeDataLocation location)
    {
      if (location == EnumTypeDataLocation.All)
        throw new ArgumentOutOfRangeException(nameof(location));

      product.EnumTypeData = location;
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, product).ConfigureAwait(false);
    }
  }
}
