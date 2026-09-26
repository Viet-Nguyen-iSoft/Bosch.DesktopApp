using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;

namespace iSoft.Database.Service
{
  public class RoleService
  {
    public async Task<List<Role>> GetAllAsync(bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new RoleRepository(context);
      return await repository.GetAllAsync(isContainDelete).ConfigureAwait(false);
    }

    public async Task<Role> AddOrUpdateAsync(Role role)
    {
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, role)
        .ConfigureAwait(false);
    }
  }
}
