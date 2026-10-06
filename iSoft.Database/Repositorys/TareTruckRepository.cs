using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Repositorys
{
  public class TareTruckRepository : GenericRepository<TareTruck, CommonDbContext>
  {
    public TareTruckRepository(DbContext context) : base(context)
    {
    }

    public Task<List<TareTruck>> GetAllAsync(bool IsContainDelete = false)
    {
      var query = Context.Set<TareTruck>().AsQueryable();
      if (!IsContainDelete)
        query = query.Where(x => !x.DeletedFlag);
      return query.ToListAsync();
    }
  }
}
