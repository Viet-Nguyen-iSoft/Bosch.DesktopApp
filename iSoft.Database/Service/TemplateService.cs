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

    public async Task UpdatePathLocalAsync(Guid id, string pathLocal)
    {
      ArgumentException.ThrowIfNullOrWhiteSpace(pathLocal);

      await using var context = new MySqlDbContext();
      var template = await context.Templates!.FindAsync(id)
        .ConfigureAwait(false)
        ?? throw new InvalidOperationException(
          "Không tìm thấy template để cập nhật đường dẫn local.");

      template.PathLocal = pathLocal;
      await context.SaveChangesAsync().ConfigureAwait(false);
    }
  }
}
