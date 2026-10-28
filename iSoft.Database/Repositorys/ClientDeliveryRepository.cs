using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using static iSoft.Database.EnumData;

namespace iSoft.Database.Repositorys
{
  public class ClientDeliveryRepository : GenericRepository<ClientDelivery, CommonDbContext>
  {
    public ClientDeliveryRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<ClientDelivery>> GetAllAsync(
      EnumTypeDataLocation enumTypeData,
      bool isContainDelete = false)
    {
      var query = Context.Set<ClientDelivery>().AsQueryable();
      if (enumTypeData != EnumTypeDataLocation.All)
        query = query.Where(item => item.EnumTypeData == enumTypeData);
      if (!isContainDelete)
        query = query.Where(item => !item.DeletedFlag);
      return query.OrderBy(item => item.Name).ToListAsync();
    }
  }
}
