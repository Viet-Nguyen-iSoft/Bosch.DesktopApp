using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

      Dictionary<string, object?>? beforeData = null;
      var action = AuditActions.Create;

      if (existingEntity == null)
      {
        entity.CreatedAt ??= DateTime.UtcNow;
        await entities.AddAsync(entity).ConfigureAwait(false);
      }
      else
      {
        var entry = context.Entry(existingEntity);
        beforeData = entry.Properties.ToDictionary(
          property => property.Metadata.Name,
          property => property.OriginalValue);
        var wasDeleted = existingEntity.DeletedFlag;

        entity.UpdatedAt = DateTime.UtcNow;
        entry.CurrentValues.SetValues(entity);

        action = !wasDeleted && entity.DeletedFlag
          ? AuditActions.Delete
          : wasDeleted && !entity.DeletedFlag
            ? AuditActions.Restore
            : AuditActions.Update;
      }

      var savedEntity = existingEntity ?? entity;
      var savedEntry = context.Entry(savedEntity);
      var afterData = savedEntry.Properties.ToDictionary(
        property => property.Metadata.Name,
        property => property.CurrentValue);
      var entityName = GetEntityName(savedEntry);

      await context.AuditLogs!.AddAsync(new AuditLog
      {
        Id = Guid.NewGuid(),
        OccurredAt = DateTime.UtcNow,
        Action = action,
        EntityType = typeof(TEntity).Name,
        EntityId = savedEntity.Id.ToString(),
        EntityName = entityName,
        UserId = AuditActorContext.UserId,
        Username = AuditActorContext.Username,
        UserDisplayName = AuditActorContext.DisplayName,
        Source = "MasterData",
        BeforeData = beforeData == null
          ? null
          : JsonSerializer.Serialize(beforeData),
        AfterData = JsonSerializer.Serialize(afterData),
        IsSuccess = true
      }).ConfigureAwait(false);

      await context.SaveChangesAsync().ConfigureAwait(false);
      return savedEntity;
    }

    private static string? GetEntityName<TEntity>(
      Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entry)
      where TEntity : BaseModel
    {
      foreach (var propertyName in new[] { "Name", "Code" })
      {
        var property = entry.Properties.FirstOrDefault(item =>
          item.Metadata.Name == propertyName);
        var value = property?.CurrentValue?.ToString();
        if (!string.IsNullOrWhiteSpace(value))
          return value;
      }

      return null;
    }
  }
}
