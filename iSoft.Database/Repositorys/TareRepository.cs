using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using static iSoft.Database.EnumData;

namespace iSoft.Database.Repositorys
{
  public sealed class TareRepository : GenericRepository<Tare, CommonDbContext>
  {
    public TareRepository(DbContext context) : base(context)
    {
    }

    public Task<List<Tare>> GetAllAsync(
      EnumTypeDataLocation location,
      bool isContainDelete = false)
    {
      var query = Context.Set<Tare>().AsQueryable();
      if (location != EnumTypeDataLocation.All)
        query = query.Where(tare => tare.EnumTypeData == location);
      if (!isContainDelete)
        query = query.Where(tare => !tare.DeletedFlag);

      return query.OrderBy(tare => tare.Name).ToListAsync();
    }
  }
}
