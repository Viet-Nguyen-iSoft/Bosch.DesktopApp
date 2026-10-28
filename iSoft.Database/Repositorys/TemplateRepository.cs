using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Repositorys
{
  public class TemplateRepository : GenericRepository<Template, CommonDbContext>
  {
    public TemplateRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<Template>> GetAllAsync(bool isContainDelete = false)
    {
      var query = Context.Set<Template>().AsQueryable();
      if (!isContainDelete)
        query = query.Where(template => !template.DeletedFlag);

      return query
        .OrderBy(template => template.Order)
        .ThenBy(template => template.Name)
        .ToListAsync();
    }
  }
}
