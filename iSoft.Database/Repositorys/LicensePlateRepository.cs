using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Repositorys
{
  public class LicensePlateRepository : GenericRepository<LicensePlate, CommonDbContext>
  {
    public LicensePlateRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<LicensePlate>> GetAllAsync(bool IsContainDelete = false)
    {
      var query = Context.Set<LicensePlate>().AsQueryable();
      if (!IsContainDelete)
        query = query.Where(x => !x.DeletedFlag);
      return query.ToListAsync();
    }
  }
}
