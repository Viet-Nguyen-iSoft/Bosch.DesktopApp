using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Repositorys
{
  public class DeliveryRepository : GenericRepository<Delivery, CommonDbContext>
  {
    public DeliveryRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<Delivery>> GetAllAsync(bool IsContainDelete = false)
    {
      var query = Context.Set<Delivery>().AsQueryable();
      if (!IsContainDelete)
        query = query.Where(x => !x.DeletedFlag);
      return query.ToListAsync();
    }
  }
}
