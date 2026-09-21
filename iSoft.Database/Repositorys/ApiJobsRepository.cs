using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Repositorys
{
  public class ApiJobsRepository : GenericRepository<ApiJobs, CommonDbContext>
  {
    public ApiJobsRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<ApiJobs>> GetAllAsync(bool isContainDelete = false)
    {
      var query = Context.Set<ApiJobs>().AsQueryable();
      if (!isContainDelete)
        query = query.Where(apiJob => !apiJob.DeletedFlag);

      return query.ToListAsync();
    }

    public async Task<ApiJobs> AddOrUpdateAsync(ApiJobs apiJob)
    {
      ArgumentNullException.ThrowIfNull(apiJob);

      await Context.Database.EnsureCreatedAsync();
      var apiJobs = Context.Set<ApiJobs>();
      var existingApiJob = apiJob.Id == Guid.Empty
        ? null
        : await apiJobs.FindAsync(apiJob.Id);

      if (existingApiJob == null)
      {
        apiJob.CreatedAt ??= DateTime.UtcNow;
        await apiJobs.AddAsync(apiJob);
      }
      else
      {
        apiJob.UpdatedAt = DateTime.UtcNow;
        Context.Entry(existingApiJob).CurrentValues.SetValues(apiJob);
      }

      await Context.SaveChangesAsync();
      return existingApiJob ?? apiJob;
    }
  }
}
