using HelperManager;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using LTP.Truck.Controls;
using Microsoft.EntityFrameworkCore;

namespace LTP.Truck
{
  internal static class Program
  {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
      Application.ThreadException += (_, args) =>
        LogHelper.LogErrorToFileLog(args.Exception, AppCore.Ins._folderFileLog);
      AppDomain.CurrentDomain.UnhandledException += (_, args) =>
      {
        if (args.ExceptionObject is Exception ex)
          LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      };
      TaskScheduler.UnobservedTaskException += (_, args) =>
      {
        LogHelper.LogErrorToFileLog(args.Exception, AppCore.Ins._folderFileLog);
        args.SetObserved();
      };

      try
      {
        ApplicationConfiguration.Initialize();
        InitDb().GetAwaiter().GetResult();
        AppCore.Ins.Init();
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        throw;
      }
    }

    public static void StartApp()
    {
      Application.Restart();
    }

    public static void CloseApp()
    {
      Application.Exit();
    }



    static async Task<bool> InitDb()
    {
      try
      {
        using (var db = new MySqlDbContext())
        {
          try
          {
            await db.Database.EnsureCreatedAsync();
            await UpdateDatabaseSchemaAsync(db);
            await db.Database.BeginTransactionAsync();

            if (db?.AppConfigs?.Count() <= 0)
            {
              await db.AppConfigs.AddAsync(new AppConfig
              {
                IpServer = "100.101.160.94",
                PortServer = 7902,
                TimeoutConnectServer = 500,
                Key = "A",
                Company = "Công ty TNHH BOSCH Việt Nam",
                OfficeAddress = "Đường số 8, KCN Long Thành, An Phước, T. Đồng Nai",
                PhoneForOfficeAddress = "0251.628.0340",
                PermitCheckWeight = false,
                DeletedFlag = false,
                EnableFlag = true,
                SyncFlag = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
              });
            }

            await db!.SaveChangesAsync();
            await db.Database.CommitTransactionAsync();
          }
          catch (Exception ex)
          {
            HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
            if (db.Database.CurrentTransaction != null)
              await db.Database.RollbackTransactionAsync();
          }
          return true;
        }
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        return false;
      }
    }

    private static async Task UpdateDatabaseSchemaAsync(MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        static async Task<int> GetTableCountAsync(
          MySqlDbContext context,
          string tableName)
        {
          await using var command = context.Database.GetDbConnection().CreateCommand();
          command.CommandText = @"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = @tableName;";
          var parameter = command.CreateParameter();
          parameter.ParameterName = "@tableName";
          parameter.Value = tableName;
          command.Parameters.Add(parameter);
          return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        var legacyUsersTableCount = await GetTableCountAsync(db, "m_users");
        var usersTableCount = await GetTableCountAsync(db, "Users");
        if (legacyUsersTableCount > 0 && usersTableCount > 0)
        {
          throw new InvalidOperationException(
            "Both 'm_users' and 'Users' exist. Automatic rename was stopped to avoid data loss.");
        }

        // Remove the obsolete many-to-many table before removing permissions,
        // so its foreign keys cannot block the schema cleanup.
        await using (var dropPermissionUsersCommand =
          db.Database.GetDbConnection().CreateCommand())
        {
          dropPermissionUsersCommand.CommandText =
            "DROP TABLE IF EXISTS `ref_permission_user`;";
          await dropPermissionUsersCommand.ExecuteNonQueryAsync();
        }

        await using (var dropPermissionsCommand =
          db.Database.GetDbConnection().CreateCommand())
        {
          dropPermissionsCommand.CommandText =
            "DROP TABLE IF EXISTS `m_permissions`;";
          await dropPermissionsCommand.ExecuteNonQueryAsync();
        }

        if (legacyUsersTableCount > 0)
        {
          await using var renameUsersCommand =
            db.Database.GetDbConnection().CreateCommand();
          renameUsersCommand.CommandText =
            "RENAME TABLE `m_users` TO `Users`;";
          await renameUsersCommand.ExecuteNonQueryAsync();
        }

        await using var checkColumnCommand = db.Database.GetDbConnection().CreateCommand();
        checkColumnCommand.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'Users'
            AND COLUMN_NAME = 'IdSrc';";
        var columnCount = Convert.ToInt32(await checkColumnCommand.ExecuteScalarAsync());
        if (columnCount == 0)
        {
          await using var addColumnCommand = db.Database.GetDbConnection().CreateCommand();
          addColumnCommand.CommandText =
            "ALTER TABLE `Users` ADD COLUMN `IdSrc` char(36) NULL;";
          await addColumnCommand.ExecuteNonQueryAsync();
        }

        await using var checkIndexCommand = db.Database.GetDbConnection().CreateCommand();
        checkIndexCommand.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.STATISTICS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'Users'
            AND COLUMN_NAME = 'IdSrc';";
        var indexCount = Convert.ToInt32(await checkIndexCommand.ExecuteScalarAsync());
        if (indexCount == 0)
        {
          await using var addIndexCommand = db.Database.GetDbConnection().CreateCommand();
          addIndexCommand.CommandText =
            "CREATE INDEX `IX_Users_IdSrc` ON `Users` (`IdSrc`);";
          await addIndexCommand.ExecuteNonQueryAsync();
        }
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }
  }
}
