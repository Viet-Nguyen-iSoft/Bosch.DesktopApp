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
  public class ProductService
  {
    public async Task<List<Product>> GetAllAsync(bool IsContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new ProductRepository(context);
      return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
    }

    public async Task<Product> AddOrUpdateAsync(Product product)
    {
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, product).ConfigureAwait(false);
    }
  }
}
