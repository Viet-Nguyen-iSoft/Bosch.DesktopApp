using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;

namespace iSoft.Database.Service
{
  public class ApiJobsService
  {
    public async Task<List<ApiJobs>> GetAllAsync(bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new ApiJobsRepository(context);
      return await repository.GetAllAsync(isContainDelete).ConfigureAwait(false);
    }

    public async Task<List<ApiJobs>> GetCreatedAsync(
      CancellationToken cancellationToken = default)
    {
      await using var context = new MySqlDbContext();
      var repository = new ApiJobsRepository(context);
      return await repository.GetCreatedAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiJobs> AddOrUpdateAsync(ApiJobs apiJob)
    {
      await using var context = new MySqlDbContext();
      var repository = new ApiJobsRepository(context);
      return await repository.AddOrUpdateAsync(apiJob).ConfigureAwait(false);
    }
  }
}
