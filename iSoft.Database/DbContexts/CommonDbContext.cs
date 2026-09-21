using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
    public virtual DbSet<Station>? Stations { get; set; }
    public virtual DbSet<Connection>? Connections { get; set; }
    public virtual DbSet<AppConfig>? AppConfigs { get; set; }
    public virtual DbSet<LogAction>? LogActions { get; set; }
    public virtual DbSet<ProductGroup>? ProductGroups { get; set; }
    public virtual DbSet<Product>? Products { get; set; }
    public virtual DbSet<CategoryTare>? CategoryTares { get; set; }
    public virtual DbSet<Warehouse>? Warehouses { get; set; }
    public virtual DbSet<TypeGoods>? TypeGoods { get; set; }
    public virtual DbSet<Client>? Clients { get; set; }
    public virtual DbSet<RecordTruck>? RecordTrucks { get; set; }
    public virtual DbSet<RecordWeight>? RecordWeights { get; set; }
    public virtual DbSet<LicensePlate>? LicensePlates { get; set; }
    public virtual DbSet<User>? Users { get; set; }
    public virtual DbSet<Permission>? Permissions { get; set; }


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

      modelBuilder.Entity<User>()
        .HasMany(user => user.Permissions)
        .WithMany(permission => permission.Users)
        .UsingEntity<Dictionary<string, object>>(
          "ref_permission_user",
          join => join
            .HasOne<Permission>()
            .WithMany()
            .HasForeignKey("PermissionId")
            .OnDelete(DeleteBehavior.ClientSetNull),
          join => join
            .HasOne<User>()
            .WithMany()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.ClientSetNull),
          join =>
          {
            join.HasKey("UserId", "PermissionId");
            join.Property<Guid>("UserId").HasColumnType("char(36)");
            join.Property<Guid>("PermissionId").HasColumnType("char(36)");
          });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     
    }
  }
}
