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
            await EnsureUserSyncSchemaAsync(db);
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
            db.Database.RollbackTransaction();
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

    private static async Task EnsureUserSyncSchemaAsync(MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using var checkColumnCommand = db.Database.GetDbConnection().CreateCommand();
        checkColumnCommand.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'm_users'
            AND COLUMN_NAME = 'IdSrc';";
        var columnCount = Convert.ToInt32(await checkColumnCommand.ExecuteScalarAsync());
        if (columnCount == 0)
        {
          await using var addColumnCommand = db.Database.GetDbConnection().CreateCommand();
          addColumnCommand.CommandText =
            "ALTER TABLE `m_users` ADD COLUMN `IdSrc` char(36) NULL;";
          await addColumnCommand.ExecuteNonQueryAsync();
        }

        await using var checkIndexCommand = db.Database.GetDbConnection().CreateCommand();
        checkIndexCommand.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.STATISTICS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'm_users'
            AND INDEX_NAME = 'IX_m_users_IdSrc';";
        var indexCount = Convert.ToInt32(await checkIndexCommand.ExecuteScalarAsync());
        if (indexCount == 0)
        {
          await using var addIndexCommand = db.Database.GetDbConnection().CreateCommand();
          addIndexCommand.CommandText =
            "CREATE INDEX `IX_m_users_IdSrc` ON `m_users` (`IdSrc`);";
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
