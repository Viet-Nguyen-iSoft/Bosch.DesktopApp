using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Service
{
  internal static class MasterDataUpsertHelper
  {
    internal static async Task<TEntity> AddOrUpdateAsync<TEntity>(
      MySqlDbContext context, TEntity entity)
      where TEntity : BaseModel
    {
      ArgumentNullException.ThrowIfNull(entity);

      var entities = context.Set<TEntity>();
      var existingEntity = entity.Id == Guid.Empty
        ? null
        : await entities.FindAsync(entity.Id).ConfigureAwait(false);

      if (existingEntity == null)
      {
        entity.CreatedAt ??= DateTime.UtcNow;
        await entities.AddAsync(entity).ConfigureAwait(false);
      }
      else
      {
        entity.UpdatedAt = DateTime.UtcNow;
        context.Entry(existingEntity).CurrentValues.SetValues(entity);
      }

      await context.SaveChangesAsync().ConfigureAwait(false);
      return existingEntity ?? entity;
    }
  }
}
