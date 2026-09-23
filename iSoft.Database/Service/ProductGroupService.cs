using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Service
{
  public class ProductGroupService
  {
    public async Task<List<ProductGroup>> GetAllAsync(bool IsContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new ProductGroupRepository(context);
      return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
    }

    public async Task<ProductGroup> AddOrUpdateAsync(ProductGroup productGroup)
    {
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, productGroup).ConfigureAwait(false);
    }
  }
}
