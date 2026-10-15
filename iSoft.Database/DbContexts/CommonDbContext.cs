using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iSoft.Database.Service;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.DbContexts
{
  public class CommonDbContext : DbContext
  {
    private static readonly HashSet<string> SensitivePropertyNames =
      new(StringComparer.OrdinalIgnoreCase)
      {
        "Password", "PW", "PasswordHistory"
      };

    private static readonly HashSet<string> SystemOnlyPropertyNames =
      new(StringComparer.OrdinalIgnoreCase)
      {
        "UpdatedAt", "UpdatedBy", "SyncFlag"
      };

    public virtual DbSet<Station>? Stations { get; set; }
    public virtual DbSet<Connection>? Connections { get; set; }
    public virtual DbSet<AppConfig>? AppConfigs { get; set; }
    public virtual DbSet<LogAction>? LogActions { get; set; }
    public virtual DbSet<AuditLog>? AuditLogs { get; set; }
    public virtual DbSet<ProductGroup>? ProductGroups { get; set; }
    public virtual DbSet<Product>? Products { get; set; }
    public virtual DbSet<CategoryTare>? CategoryTares { get; set; }
    public virtual DbSet<Warehouse>? Warehouses { get; set; }
    public virtual DbSet<TypeGoods>? TypeGoods { get; set; }
    public virtual DbSet<Client>? Clients { get; set; }
    public virtual DbSet<RecordTruck>? RecordTrucks { get; set; }
    public virtual DbSet<RecordWeight>? RecordWeights { get; set; }
    public virtual DbSet<LicensePlate>? LicensePlates { get; set; }
    public virtual DbSet<ApiJobs>? ApiJobs { get; set; }
    public virtual DbSet<Delivery>? Deliveries { get; set; }
    public virtual DbSet<User>? Users { get; set; }
    public virtual DbSet<Role>? Roles { get; set; }
    public virtual DbSet<TareTruck>? TareTrucks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ConfigureDateTimeProperties("datetime(6)");

      foreach (var entityType in modelBuilder.Model.GetEntityTypes()
        .Where(entityType => typeof(BaseModel).IsAssignableFrom(entityType.ClrType)))
      {
        modelBuilder.Entity(entityType.ClrType)
          .HasIndex(nameof(BaseModel.IdSrc));
      }

      modelBuilder.Entity<RecordTruck>()
        .Property(record => record.LicensePlate)
        .HasMaxLength(20);

      modelBuilder.Entity<RecordTruck>()
        .Property(record => record.NumberTare)
        .HasDefaultValue(1);

      modelBuilder.Entity<AppConfig>()
        .Property(config => config.NumberPrintA4)
        .HasDefaultValue(1);

      modelBuilder.Entity<AppConfig>()
        .Property(config => config.NumberPrintLabel)
        .HasDefaultValue(1);

      modelBuilder.Entity<LicensePlate>()
        .Property(licensePlate => licensePlate.Plate)
        .HasMaxLength(20);

      modelBuilder.Entity<LicensePlate>()
        .HasIndex(licensePlate => licensePlate.Plate)
        .IsUnique()
        .HasDatabaseName("UX_LicensePlates_Name");

      modelBuilder.Entity<RecordTruck>()
        .HasIndex(record => new
        {
          record.DeletedFlag,
          record.EnumTypeDataTruck,
          record.CreatedAt,
          record.Id
        })
        .HasDatabaseName("IX_RecordTrucks_FirstWeighing");

      modelBuilder.Entity<RecordTruck>()
        .HasIndex(record => new { record.SyncFlag, record.CreatedAt })
        .HasDatabaseName("IX_RecordTrucks_PendingSync");

      modelBuilder.Entity<RecordTruck>()
        .HasIndex(record => record.LicensePlate)
        .HasDatabaseName("IX_RecordTrucks_LicensePlate");

      modelBuilder.Entity<RecordWeight>()
        .HasIndex(record => new { record.RecordTruckId, record.DeletedFlag })
        .HasDatabaseName("IX_RecordWeights_Truck_Deleted");

      modelBuilder.Entity<RecordWeight>()
        .HasIndex(record => new { record.SyncFlag, record.CreatedAt })
        .HasDatabaseName("IX_RecordWeights_PendingSync");

      modelBuilder.Entity<Product>()
        .Property(product => product.EnumWasteType)
        .HasConversion<int>()
        .HasDefaultValue((EnumData.EnumWasteType)0);

      modelBuilder.Entity<TypeGoods>()
        .Property(typeGoods => typeGoods.EnumWasteType)
        .HasConversion<int>()
        .HasDefaultValue((EnumData.EnumWasteType)0);

      modelBuilder.Entity<TypeGoods>()
        .HasOne(typeGoods => typeGoods.ProductGroup)
        .WithMany()
        .HasForeignKey(typeGoods => typeGoods.ProductGroupId);

      modelBuilder.Entity<Connection>()
        .HasIndex(connection => new
        {
          connection.EnumDevice,
          connection.DeletedFlag,
          connection.SyncFlag
        })
        .HasDatabaseName("IX_Connections_PendingWeight");

      modelBuilder.Entity<Station>()
        .HasIndex(station => new { station.DeletedFlag, station.EnableFlag })
        .HasDatabaseName("IX_Stations_Active");

      modelBuilder.Entity<LogAction>()
        .HasIndex(log => new { log.CreatedAt, log.eAction })
        .HasDatabaseName("IX_LogActions_CreatedAt_Action");

      modelBuilder.Entity<AuditLog>(entity =>
      {
        entity.Property(log => log.Action).HasMaxLength(50);
        entity.Property(log => log.EntityType).HasMaxLength(150);
        entity.Property(log => log.EntityId).HasMaxLength(100);
        entity.Property(log => log.EntityName).HasMaxLength(500);
        entity.Property(log => log.Username).HasMaxLength(255);
        entity.Property(log => log.UserDisplayName).HasMaxLength(255);
        entity.Property(log => log.Source).HasMaxLength(255);
        entity.HasIndex(log => log.OccurredAt)
          .HasDatabaseName("IX_AuditLogs_OccurredAt");
        entity.HasIndex(log => new { log.EntityType, log.EntityId })
          .HasDatabaseName("IX_AuditLogs_Entity");
        entity.HasIndex(log => new { log.UserId, log.OccurredAt })
          .HasDatabaseName("IX_AuditLogs_User_OccurredAt");
      });

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
      TryAddAutomaticAuditEntries();
      try
      {
        return base.SaveChanges(acceptAllChangesOnSuccess);
      }
      catch (Exception ex)
      {
        LogDatabaseError(ex);
        throw;
      }
    }

    public override async Task<int> SaveChangesAsync(
      bool acceptAllChangesOnSuccess,
      CancellationToken cancellationToken = default)
    {
      TryAddAutomaticAuditEntries();
      try
      {
        return await base.SaveChangesAsync(
          acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);
      }
      catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
      {
        throw;
      }
      catch (Exception ex)
      {
        LogDatabaseError(ex);
        throw;
      }
    }

    private void TryAddAutomaticAuditEntries()
    {
      try
      {
        AddAutomaticAuditEntries();
      }
      catch (Exception ex)
      {
        LogDatabaseError(ex);
      }
    }

    private static void LogDatabaseError(Exception exception)
    {
      HelperManager.LogHelper.LogErrorToFileLog(
        exception, Path.Combine(AppContext.BaseDirectory, "Logs"));
    }

    private void AddAutomaticAuditEntries()
    {
      ChangeTracker.DetectChanges();
      var trackedEntries = ChangeTracker.Entries<BaseModel>()
        .Where(entry => entry.State is EntityState.Added or
          EntityState.Modified or EntityState.Deleted)
        .Where(entry => entry.Entity is not iSoft.Database.Models.ApiJobs &&
          entry.Entity is not iSoft.Database.Models.LogAction)
        .ToList();

      foreach (var entry in trackedEntries)
      {
        var properties = entry.Properties
          .Where(property => !SensitivePropertyNames.Contains(
            property.Metadata.Name))
          .ToList();
        var changedProperties = entry.State == EntityState.Modified
          ? properties.Where(property => property.IsModified).ToList()
          : properties;

        if (entry.State == EntityState.Modified &&
            changedProperties.All(property =>
              SystemOnlyPropertyNames.Contains(property.Metadata.Name)))
          continue;

        // Trạng thái đăng nhập được ghi thành sự kiện LOGIN_* riêng.
        if (entry.Entity is User && entry.State == EntityState.Modified &&
            changedProperties.All(property =>
              SystemOnlyPropertyNames.Contains(property.Metadata.Name) ||
              property.Metadata.Name is nameof(User.FailedLoginAttempts) or
                nameof(User.IsLoginLocked)))
          continue;

        var before = entry.State == EntityState.Added
          ? null
          : changedProperties.ToDictionary(
            property => property.Metadata.Name,
            property => property.OriginalValue);
        var after = entry.State == EntityState.Deleted
          ? null
          : changedProperties.ToDictionary(
            property => property.Metadata.Name,
            property => property.CurrentValue);

        var action = ResolveAction(entry);
        AuditLogs!.Add(new AuditLog
        {
          Id = Guid.NewGuid(),
          OccurredAt = DateTime.UtcNow,
          Action = action,
          EntityType = entry.Metadata.ClrType.Name,
          EntityId = entry.Entity.Id.ToString(),
          EntityName = GetEntityName(entry, properties),
          UserId = AuditActorContext.UserId,
          Username = AuditActorContext.Username,
          UserDisplayName = AuditActorContext.DisplayName,
          Source = AuditActorContext.Source ?? entry.Metadata.ClrType.Name,
          Reason = properties.FirstOrDefault(property =>
            property.Metadata.Name == "ReasonDelete")?.CurrentValue?.ToString(),
          BeforeData = before == null ? null : JsonSerializer.Serialize(before),
          AfterData = after == null ? null : JsonSerializer.Serialize(after),
          IsSuccess = true
        });
      }
    }

    private static string ResolveAction(
      Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseModel> entry)
    {
      if (entry.State == EntityState.Added)
        return AuditActions.Create;
      if (entry.State == EntityState.Deleted)
        return AuditActions.Delete;

      var deletedProperty = entry.Property(nameof(BaseModel.DeletedFlag));
      if (deletedProperty.IsModified)
      {
        var before = (bool)(deletedProperty.OriginalValue ?? false);
        var after = (bool)(deletedProperty.CurrentValue ?? false);
        if (!before && after)
          return AuditActions.Delete;
        if (before && !after)
          return AuditActions.Restore;
      }

      return AuditActions.Update;
    }

    private static string? GetEntityName(
      Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseModel> entry,
      IReadOnlyCollection<Microsoft.EntityFrameworkCore.ChangeTracking.PropertyEntry> properties)
    {
      foreach (var name in new[] { "Name", "Username", "Code", "LicensePlate" })
      {
        var property = properties.FirstOrDefault(item =>
          item.Metadata.Name == name);
        var value = property?.CurrentValue?.ToString();
        if (!string.IsNullOrWhiteSpace(value))
          return value;
      }

      return null;
    }
  }
}
