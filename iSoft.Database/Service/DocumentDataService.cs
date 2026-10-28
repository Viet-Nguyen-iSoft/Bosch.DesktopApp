using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;

namespace iSoft.Database.Service
{
  public class DocumentDataService
  {
    public async Task<List<DocumentData>> GetAllAsync(bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new DocumentDataRepository(context);
      return await repository.GetAllAsync(isContainDelete).ConfigureAwait(false);
    }

    public async Task<DocumentData> AddOrUpdateAsync(DocumentData documentData)
    {
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, documentData)
        .ConfigureAwait(false);
    }

    public async Task<DocumentData?> GetByDateAsync(DateTime date)
    {
      await using var context = new MySqlDbContext();
      var repository = new DocumentDataRepository(context);
      return await repository.GetByDateAsync(date).ConfigureAwait(false);
    }

    public async Task<bool> ExistsByDateAsync(DateTime date, Guid? excludedId = null)
    {
      await using var context = new MySqlDbContext();
      var repository = new DocumentDataRepository(context);
      return await repository.ExistsByDateAsync(date, excludedId).ConfigureAwait(false);
    }
  }
}
