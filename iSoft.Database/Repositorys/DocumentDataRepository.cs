using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Repositorys
{
  public class DocumentDataRepository : GenericRepository<DocumentData, CommonDbContext>
  {
    public DocumentDataRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<DocumentData>> GetAllAsync(bool isContainDelete = false)
    {
      var query = Context.Set<DocumentData>().AsQueryable();
      if (!isContainDelete)
        query = query.Where(document => !document.DeletedFlag);

      return query
        .OrderByDescending(document => document.Date)
        .ThenBy(document => document.Document)
        .ToListAsync();
    }

    public Task<DocumentData?> GetByDateAsync(DateTime date)
    {
      var dateFrom = date.Date;
      var dateTo = dateFrom.AddDays(1);
      return Context.Set<DocumentData>()
        .Where(document => !document.DeletedFlag)
        .Where(document => document.Date >= dateFrom && document.Date < dateTo)
        .FirstOrDefaultAsync();
    }
  }
}
