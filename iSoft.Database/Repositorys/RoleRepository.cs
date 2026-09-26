using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Repositorys
{
  public class RoleRepository : GenericRepository<Role, CommonDbContext>
  {
    public RoleRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<Role>> GetAllAsync(bool isContainDelete = false)
    {
      var query = Context.Set<Role>().AsQueryable();
      if (!isContainDelete)
        query = query.Where(role => !role.DeletedFlag);

      return query
        .OrderBy(role => role.Code)
        .ThenBy(role => role.Name)
        .ToListAsync();
    }
  }
}
