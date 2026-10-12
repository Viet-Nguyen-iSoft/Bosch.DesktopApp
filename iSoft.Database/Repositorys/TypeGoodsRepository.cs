using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Repositorys
{
  public class TypeGoodsRepository : GenericRepository<TypeGoods, CommonDbContext>
  {
    public TypeGoodsRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<TypeGoods>> GetAllAsync(bool IsContainDelete = false)
    {
      var query = Context.Set<TypeGoods>()
        .Include(typeGoods => typeGoods.ProductGroup)
        .AsQueryable();
      if (!IsContainDelete)
        query = query.Where(x => !x.DeletedFlag);
      return query.ToListAsync();
    }
  }
}
