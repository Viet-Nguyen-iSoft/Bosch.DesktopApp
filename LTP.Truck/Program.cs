using HelperManager;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using LTP.Truck.Controls;

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
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      ApplicationConfiguration.Initialize();


      //Khởi tạo Db
      InitDb().GetAwaiter().GetResult();

      //Start Form
      AppCore.Ins.Init();
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
            await db.Database.BeginTransactionAsync();

            if (db?.AppConfigs?.Count() <= 0)
            {
              await db.AppConfigs.AddAsync(new AppConfig
              {
                IpServer = "100.101.160.94",
                PortServer = 7902,
                TimeoutConnectServer = 500,
                Key = "A",
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
  }
}
