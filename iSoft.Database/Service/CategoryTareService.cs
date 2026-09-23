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
  public class CategoryTareService
  {
    public async Task<List<CategoryTare>> GetAllAsync(bool IsContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new CategoryTareRepository(context);
      return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
    }

    public async Task<CategoryTare> AddOrUpdateAsync(CategoryTare categoryTare)
    {
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, categoryTare).ConfigureAwait(false);
    }
  }
}
