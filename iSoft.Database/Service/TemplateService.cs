using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;

namespace iSoft.Database.Service
{
  public class TemplateService
  {
    public async Task<List<Template>> GetAllAsync(
      bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new TemplateRepository(context);
      return await repository.GetAllAsync(isContainDelete)
        .ConfigureAwait(false);
    }
  }
}
