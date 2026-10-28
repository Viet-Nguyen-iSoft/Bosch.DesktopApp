using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Database.EnumData;

namespace iSoft.Database.Repositorys
{
  public class ProductRepository : GenericRepository<Product, CommonDbContext>
  {
    public ProductRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<Product>> GetAllAsync(
      EnumTypeDataLocation location,
      bool isContainDelete = false)
    {
      var query = Context.Set<Product>()
        .Include(product => product.ProductGroup)
        .AsQueryable();
      if (location != EnumTypeDataLocation.All)
        query = query.Where(product => product.EnumTypeData == location);
      if (!isContainDelete)
        query = query.Where(product => !product.DeletedFlag);
      return query.ToListAsync();
    }
  }
}
