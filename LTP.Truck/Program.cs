using HelperManager;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using LTP.Truck.Controls;
using LTP.Truck.SecurityPassword;
using LTP.Truck.Services;
using Microsoft.EntityFrameworkCore;
using static iSoft.Database.EnumData;

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
        LogUnhandledException(args.Exception, "UI Thread");
      AppDomain.CurrentDomain.UnhandledException += (_, args) =>
      {
        if (args.ExceptionObject is Exception ex)
          LogUnhandledException(ex, "AppDomain");
      };
      TaskScheduler.UnobservedTaskException += (_, args) =>
      {
        LogUnhandledException(args.Exception, "TaskScheduler");
        args.SetObserved();
      };

      try
      {
        //var a = PasswordEncryptor.Encrypt("Admin@1234");
        //var b = SecurityHelper.EncodePassword("VietNguyen", "WHj/9niZdWnILIFjR3VMAg==");
        //var c = SecurityHelper.EncodePassword("VietNguyen", "Hsf@2026");
        //var d = SecurityHelper.EncodePassword("VietNguyen", "Hsf@2026");
        ApplicationConfiguration.Initialize();
        if (!InitDb().GetAwaiter().GetResult())
        {
          throw new InvalidOperationException(
            "Khởi tạo hoặc nâng cấp cơ sở dữ liệu thất bại. Xem file log để biết chi tiết.");
        }
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

    private static void LogUnhandledException(Exception exception, string source)
    {
      LogHelper.LogErrorToFileLog(exception, AppCore.Ins._folderFileLog);
      _ = AuditTrail.WriteAsync(
        AuditActions.Error,
        "Application",
        entityName: "Lỗi ứng dụng chưa được xử lý",
        source: source,
        reason: exception.Message,
        isSuccess: false,
        exception: exception);
    }



    private static Dictionary<string, string> PermissionsTruck = new Dictionary<string, string>
    {
      ["0043"] = "Cho phép vận hành",
      ["0044"] = "Cho phép cập nhật dữ liệu masterdata",
      ["0045"] = "Cho phép cân khi số liệu sai lệch quá ngưỡng cài đặt",
      ["0046"] = "Cho phép xem cài đặt",
      ["0047"] = "Cho phép chỉnh sửa cài đặt",
      ["0048"] = "Cho phép xem dữ liệu masterdata",
      ["0049"] = "Cho phép xem lịch sử cân",

      ["0100"] = "Cho phép xem danh sách tài khoản",
      ["0101"] = "Cho phép thêm tài khoản",
      ["0102"] = "Cho phép chỉnh sửa tài khoản",
      ["0103"] = "Cho phép chỉnh xóa tài khoản",
      ["0104"] = "Cho phép phân quyền tài khoản",

      ["0200"] = "Cho phép thêm dữ liệu Manual cân lần 1",
      ["0201"] = "Cho phép xóa dữ liệu cân",
      ["0202"] = "Cho phép thu hồi dữ liệu đã xóa",
      ["0203"] = "Cho phép xem Audit Log",
      ["0203"] = "Cho phép truy cập trang Audit",
    };
    private static Dictionary<string, string> PermissionsGoods = new Dictionary<string, string>
    {
      ["0050"] = "Cho phép vận hành",
      ["0051"] = "Cho phép cập nhật dữ liệu masterdata",
      ["0052"] = "Cho phép xem cài đặt",
      ["0053"] = "Cho phép chỉnh sửa cài đặt",
      ["0054"] = "Cho phép xem dữ liệu masterdata",
      ["0055"] = "Cho phép xem lịch sử cân",

      ["0110"] = "Cho phép xem danh sách tài khoản",
      ["0111"] = "Cho phép thêm tài khoản",
      ["0112"] = "Cho phép chỉnh sửa tài khoản",
      ["0113"] = "Cho phép chỉnh xóa tài khoản",
      ["0114"] = "Cho phép phân quyền tài khoản",

      ["0300"] = "Cho phép xóa dữ liệu cân",
      ["0301"] = "Cho phép thu hồi dữ liệu đã xóa",
      ["0303"] = "Cho phép xem Audit Log",
      ["0303"] = "Cho phép truy cập trang Audit",
    };

    static async Task<bool> InitDb()
    {
      try
      {
        using (var db = new MySqlDbContext())
        {
          try
          {
            await db.Database.EnsureCreatedAsync();
            // Các service hiện tại chỉ đọc bảng hợp nhất. Tạo và chuyển dữ liệu
            // bảng này trước để một migration độc lập khác không thể chặn nó.
            await EnsureClientDeliveryTableAndMigrateAsync(db);
            await EnsureUserPasswordPolicyColumnsAsync(db);
            await EnsureAppConfigPrintColumnsAsync(db);
            await EnsureRecordWeightReasonDeleteColumnAsync(db);
            await EnsureTypeGoodsWasteColumnsAsync(db);
            await EnsureProductTagMappingReportColumnAsync(db);
            await EnsureProductTableAndMigrateTypeGoodsAsync(db);
            await EnsureAuditLogsTableAsync(db);
            await EnsureDocumentDataTableAsync(db);
            await EnsureTareTableAndMigrateAsync(db);
            await EnsureTemplateTableAsync(db);

            //TEST UPDATE DB
            await EnsureRecordTruckNumberTareColumnAsync(db);
            //await UpdateDatabaseSchemaAsync(db);

            var needsAppConfig = !await db.AppConfigs!.AnyAsync();
            var needsUsers = !await db.Users!.AnyAsync();
            var needStation = !await db.Stations!.AnyAsync();

            // Bỏ transaction/SaveChanges trong các lần mở app thông thường.
            //if (!needsAppConfig && !needsRoles)
            //  return true;

            await db.Database.BeginTransactionAsync();

            if (needsAppConfig)
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
                Version = "1.0.0",
                DeletedFlag = false,
                EnableFlag = true,
                SyncFlag = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
              });
            }

            if (needStation)
            {
              List<Station> stations = new List<Station>();
              stations.Add(new Station
              {
                Name = "Cân xe tải 80 tấn - Trạm 01",
                Code = "ST01",
                Description = "",
                WeightDeviation = 0,
                DeletedFlag = false,
                EnableFlag = true,
                SyncFlag = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

              });
              stations.Add(new Station
              {
                Name = "Cân xe tải 80 tấn - Trạm 02",
                Code = "ST02",
                Description = "",
                WeightDeviation = 0,
                DeletedFlag = false,
                EnableFlag = true,
                SyncFlag = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

              });
              stations.Add(new Station
              {
                Name = "Cân chất thải 3 tấn - Trạm 03",
                Code = "ST03",
                Description = "",
                WeightDeviation = 0,
                DeletedFlag = false,
                EnableFlag = true,
                SyncFlag = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

              });

              await db.Stations.AddRangeAsync(stations);
            }


            var existingRoles = await db.Roles!.ToListAsync();
            await SynchronizePermissionsAsync(
              db, existingRoles, PermissionsTruck, type: 1);
            await SynchronizePermissionsAsync(
              db, existingRoles, PermissionsGoods, type: 2);

            if (needsUsers)
            {
              var roleCodes = PermissionsTruck.Keys
                .Concat(PermissionsGoods.Keys)
                .ToList();

              await db.Users!.AddAsync(new User
              {
                FullName = "i-Soft",
                EmployeeCode = "IS000",
                IdCardCode = "",
                Username = "ISOFT",
                Password = "gbMPGkbKY/Fw2ySUZogOUw==",
                PW = "i-Soft@123",
                DisplayName = "i-Soft",
                Role = JsonHelper.ToJson(roleCodes),
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
            return false;
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

    private static async Task SynchronizePermissionsAsync(
      MySqlDbContext db,
      List<Role> existingRoles,
      IReadOnlyDictionary<string, string> permissions,
      int type)
    {
      foreach (var permission in permissions)
      {
        var existingRole = existingRoles.FirstOrDefault(role =>
          string.Equals(role.Code, permission.Key,
            StringComparison.OrdinalIgnoreCase));

        if (existingRole == null)
        {
          var newRole = new Role
          {
            Type = type,
            Code = permission.Key,
            Name = permission.Value,
            Description = string.Empty,
            DeletedFlag = false,
            EnableFlag = true,
            SyncFlag = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
          };

          await db.Roles!.AddAsync(newRole);
          existingRoles.Add(newRole);
          continue;
        }

        if (!string.Equals(existingRole.Name, permission.Value,
              StringComparison.Ordinal))
        {
          existingRole.Name = permission.Value;
          existingRole.UpdatedAt = DateTime.UtcNow;
        }
      }
    }

    private static async Task EnsureRecordTruckNumberTareColumnAsync(
      MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using var checkColumnCommand =
          db.Database.GetDbConnection().CreateCommand();
        checkColumnCommand.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'RecordTrucks'
            AND COLUMN_NAME = 'NumberTare';";

        var columnCount = Convert.ToInt32(
          await checkColumnCommand.ExecuteScalarAsync());
        if (columnCount > 0)
          return;

        await using var addColumnCommand =
          db.Database.GetDbConnection().CreateCommand();
        addColumnCommand.CommandText = @"
          ALTER TABLE `RecordTrucks`
          ADD COLUMN `NumberTare` int NULL DEFAULT 1;";
        await addColumnCommand.ExecuteNonQueryAsync();
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureAuditLogsTableAsync(MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using var command = db.Database.GetDbConnection().CreateCommand();
        command.CommandText = @"
          CREATE TABLE IF NOT EXISTS `AuditLogs` (
            `Id` char(36) NOT NULL,
            `OccurredAt` datetime(6) NOT NULL,
            `Action` varchar(50) NOT NULL,
            `EntityType` varchar(150) NOT NULL,
            `EntityId` varchar(100) NULL,
            `EntityName` varchar(500) NULL,
            `UserId` char(36) NULL,
            `Username` varchar(255) NULL,
            `UserDisplayName` varchar(255) NULL,
            `Source` varchar(255) NULL,
            `Reason` longtext NULL,
            `BeforeData` longtext NULL,
            `AfterData` longtext NULL,
            `IsSuccess` tinyint(1) NOT NULL DEFAULT 1,
            `ErrorMessage` longtext NULL,
            CONSTRAINT `PK_AuditLogs` PRIMARY KEY (`Id`),
            INDEX `IX_AuditLogs_OccurredAt` (`OccurredAt`),
            INDEX `IX_AuditLogs_Entity` (`EntityType`, `EntityId`),
            INDEX `IX_AuditLogs_User_OccurredAt` (`UserId`, `OccurredAt`)
          ) CHARACTER SET=utf8mb4;";
        await command.ExecuteNonQueryAsync();
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureDocumentDataTableAsync(MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using var command = db.Database.GetDbConnection().CreateCommand();
        command.CommandText = @"
          CREATE TABLE IF NOT EXISTS `DocumentDatas` (
            `Id` char(36) NOT NULL,
            `Date` datetime(6) NULL,
            `Document` longtext NULL,
            `Description` longtext NULL,
            `CreatedAt` datetime(6) NULL,
            `CreatedBy` char(36) NULL,
            `UpdatedAt` datetime(6) NULL,
            `UpdatedBy` char(36) NULL,
            `DeletedFlag` tinyint(1) NOT NULL,
            `EnableFlag` tinyint(1) NOT NULL,
            `SyncFlag` tinyint(1) NOT NULL,
            CONSTRAINT `PK_DocumentDatas` PRIMARY KEY (`Id`)
          ) CHARACTER SET=utf8mb4;";
        await command.ExecuteNonQueryAsync();
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureTemplateTableAsync(MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using var command = db.Database.GetDbConnection().CreateCommand();
        command.CommandText = @"
          CREATE TABLE IF NOT EXISTS `Templates` (
            `Id` char(36) NOT NULL,
            `Name` longtext NULL,
            `SerialCode` longtext NULL,
            `TemplateFile` longtext NULL,
            `PathLocal` longtext NULL,
            `Order` int NOT NULL,
            `IsBBGN` tinyint(1) NOT NULL,
            `CreatedAt` datetime(6) NULL,
            `CreatedBy` char(36) NULL,
            `UpdatedAt` datetime(6) NULL,
            `UpdatedBy` char(36) NULL,
            `DeletedFlag` tinyint(1) NOT NULL,
            `EnableFlag` tinyint(1) NOT NULL,
            `SyncFlag` tinyint(1) NOT NULL,
            CONSTRAINT `PK_Templates` PRIMARY KEY (`Id`),
            INDEX `IX_Templates_IsBBGN_DeletedFlag_Order`
              (`IsBBGN`, `DeletedFlag`, `Order`)
          ) CHARACTER SET=utf8mb4;";
        await command.ExecuteNonQueryAsync();

        await using var checkColumnCommand =
          db.Database.GetDbConnection().CreateCommand();
        checkColumnCommand.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'Templates'
            AND COLUMN_NAME = 'PathLocal';";
        var columnCount = Convert.ToInt32(
          await checkColumnCommand.ExecuteScalarAsync());
        if (columnCount == 0)
        {
          await using var addColumnCommand =
            db.Database.GetDbConnection().CreateCommand();
          addColumnCommand.CommandText = @"
            ALTER TABLE `Templates`
            ADD COLUMN `PathLocal` longtext NULL AFTER `TemplateFile`;";
          await addColumnCommand.ExecuteNonQueryAsync();
        }
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureTareTableAndMigrateAsync(MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using (var createCommand = db.Database.GetDbConnection().CreateCommand())
        {
          createCommand.CommandText = @"
            CREATE TABLE IF NOT EXISTS `Tares` (
              `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
              `Code` longtext NULL,
              `Name` longtext NULL,
              `Description` longtext NULL,
              `Value` double NULL DEFAULT 0,
              `EnumTypeData` int NOT NULL,
              `CreatedAt` datetime(6) NULL,
              `CreatedBy` char(36) NULL,
              `UpdatedAt` datetime(6) NULL,
              `UpdatedBy` char(36) NULL,
              `DeletedFlag` tinyint(1) NOT NULL,
              `EnableFlag` tinyint(1) NOT NULL,
              `SyncFlag` tinyint(1) NOT NULL,
              CONSTRAINT `PK_Tares` PRIMARY KEY (`Id`),
              INDEX `IX_Tares_EnumTypeData_DeletedFlag` (`EnumTypeData`, `DeletedFlag`)
            ) CHARACTER SET=utf8mb4;";
          await createCommand.ExecuteNonQueryAsync();
        }

        await CopyLegacyTaresAsync(db, "CategoryTares", (int)EnumTypeDataLocation.Good);
        await CopyLegacyTaresAsync(db, "TareTrucks", (int)EnumTypeDataLocation.Truck);

        var foreignKeys = new List<(string Name, string ReferencedTable)>();
        await using (var findForeignKeys = db.Database.GetDbConnection().CreateCommand())
        {
          findForeignKeys.CommandText = @"
            SELECT CONSTRAINT_NAME, REFERENCED_TABLE_NAME
            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = 'RecordWeights'
              AND COLUMN_NAME = 'CategoryTareId'
              AND REFERENCED_TABLE_NAME IS NOT NULL;";
          await using var reader = await findForeignKeys.ExecuteReaderAsync();
          while (await reader.ReadAsync())
            foreignKeys.Add((reader.GetString(0), reader.GetString(1)));
        }

        if (foreignKeys.Count == 1 && string.Equals(
              foreignKeys[0].ReferencedTable, "Tares",
              StringComparison.OrdinalIgnoreCase))
          return;

        foreach (var foreignKey in foreignKeys)
        {
          string escapedName = foreignKey.Name.Replace("`", "``", StringComparison.Ordinal);
          await using var dropCommand = db.Database.GetDbConnection().CreateCommand();
          dropCommand.CommandText =
            $"ALTER TABLE `RecordWeights` DROP FOREIGN KEY `{escapedName}`;";
          await dropCommand.ExecuteNonQueryAsync();
        }

        // Pomelo lưu Guid dạng char(36)/ascii. Nếu bảng được tạo bởi bản cũ
        // với utf8mb4 thì MySQL từ chối tạo FK vì hai cột không tương thích.
        await using (var normalizeId = db.Database.GetDbConnection().CreateCommand())
        {
          normalizeId.CommandText = @"
            ALTER TABLE `Tares`
            MODIFY COLUMN `Id`
              char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL;";
          await normalizeId.ExecuteNonQueryAsync();
        }

        await using var addForeignKey = db.Database.GetDbConnection().CreateCommand();
        addForeignKey.CommandText = @"
          ALTER TABLE `RecordWeights`
          ADD CONSTRAINT `FK_RecordWeights_Tares_CategoryTareId`
          FOREIGN KEY (`CategoryTareId`) REFERENCES `Tares` (`Id`);";
        await addForeignKey.ExecuteNonQueryAsync();
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task CopyLegacyTaresAsync(
      MySqlDbContext db,
      string tableName,
      int location)
    {
      await using var existsCommand = db.Database.GetDbConnection().CreateCommand();
      existsCommand.CommandText = @"
        SELECT COUNT(*)
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @tableName;";
      var parameter = existsCommand.CreateParameter();
      parameter.ParameterName = "@tableName";
      parameter.Value = tableName;
      existsCommand.Parameters.Add(parameter);
      if (Convert.ToInt32(await existsCommand.ExecuteScalarAsync()) == 0)
        return;

      string escapedTableName = tableName.Replace("`", "``", StringComparison.Ordinal);
      await using var copyCommand = db.Database.GetDbConnection().CreateCommand();
      copyCommand.CommandText = $@"
        INSERT IGNORE INTO `Tares`
          (`Id`, `Code`, `Name`, `Description`, `Value`, `EnumTypeData`,
           `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`,
           `DeletedFlag`, `EnableFlag`, `SyncFlag`)
        SELECT `Id`, `Code`, `Name`, `Description`, `Value`, {location},
               `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`,
               `DeletedFlag`, `EnableFlag`, `SyncFlag`
        FROM `{escapedTableName}`;";
      await copyCommand.ExecuteNonQueryAsync();
    }

    private static async Task EnsureClientDeliveryTableAndMigrateAsync(
      MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using (var createCommand = db.Database.GetDbConnection().CreateCommand())
        {
          createCommand.CommandText = @"
            CREATE TABLE IF NOT EXISTS `ClientDeliveries` (
              `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
              `Name` longtext NULL,
              `OfficeAddress` longtext NULL,
              `PhoneForOfficeAddress` longtext NULL,
              `AgentAddress` longtext NULL,
              `PhoneForAgentAddress` longtext NULL,
              `Description` longtext NULL,
              `IsDefaultDelivery` tinyint(1) NOT NULL DEFAULT 0,
              `EnumTypeData` int NOT NULL,
              `CreatedAt` datetime(6) NULL,
              `CreatedBy` char(36) NULL,
              `UpdatedAt` datetime(6) NULL,
              `UpdatedBy` char(36) NULL,
              `DeletedFlag` tinyint(1) NOT NULL,
              `EnableFlag` tinyint(1) NOT NULL,
              `SyncFlag` tinyint(1) NOT NULL,
              CONSTRAINT `PK_ClientDeliveries` PRIMARY KEY (`Id`),
              INDEX `IX_ClientDeliveries_EnumTypeData_DeletedFlag`
                (`EnumTypeData`, `DeletedFlag`)
            ) CHARACTER SET=utf8mb4;";
          await createCommand.ExecuteNonQueryAsync();
        }

        await EnsureClientDeliveryColumnsAsync(db);

        await CopyLegacyClientDeliveriesAsync(
          db, "Clients", EnumTypeDataLocation.Truck);
        await CopyLegacyClientDeliveriesAsync(
          db, "Deliveries", EnumTypeDataLocation.Good);

        await NormalizeClientDeliveryIdAsync(db);

        await EnsureClientDeliveryForeignKeyAsync(
          db, "RecordTrucks", "ClientId",
          "FK_RecordTrucks_ClientDeliveries_ClientId");
        await EnsureClientDeliveryForeignKeyAsync(
          db, "RecordWeights", "DeliveryId",
          "FK_RecordWeights_ClientDeliveries_DeliveryId");
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task NormalizeClientDeliveryIdAsync(MySqlDbContext db)
    {
      await using var referencedCommand = db.Database.GetDbConnection().CreateCommand();
      referencedCommand.CommandText = @"
        SELECT COUNT(*)
        FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
        WHERE TABLE_SCHEMA = DATABASE()
          AND REFERENCED_TABLE_NAME = 'ClientDeliveries'
          AND REFERENCED_COLUMN_NAME = 'Id';";
      int referenceCount = Convert.ToInt32(
        await referencedCommand.ExecuteScalarAsync());
      if (referenceCount > 0)
        return;

      await using var normalizeCommand = db.Database.GetDbConnection().CreateCommand();
      normalizeCommand.CommandText = @"
        ALTER TABLE `ClientDeliveries`
        MODIFY COLUMN `Id`
          char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL;";
      await normalizeCommand.ExecuteNonQueryAsync();
    }

    private static async Task EnsureClientDeliveryColumnsAsync(MySqlDbContext db)
    {
      await using var existsCommand = db.Database.GetDbConnection().CreateCommand();
      existsCommand.CommandText = @"
        SELECT COUNT(*)
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_SCHEMA = DATABASE()
          AND TABLE_NAME = 'ClientDeliveries'
          AND COLUMN_NAME = 'IsDefaultDelivery';";
      if (Convert.ToInt32(await existsCommand.ExecuteScalarAsync()) > 0)
        return;

      await using var addCommand = db.Database.GetDbConnection().CreateCommand();
      addCommand.CommandText = @"
        ALTER TABLE `ClientDeliveries`
        ADD COLUMN `IsDefaultDelivery` tinyint(1) NOT NULL DEFAULT 0
        AFTER `Description`;";
      await addCommand.ExecuteNonQueryAsync();
    }

    private static async Task CopyLegacyClientDeliveriesAsync(
      MySqlDbContext db,
      string tableName,
      EnumTypeDataLocation enumTypeData)
    {
      await using var existsCommand = db.Database.GetDbConnection().CreateCommand();
      existsCommand.CommandText = @"
        SELECT COUNT(*)
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @tableName;";
      var tableParameter = existsCommand.CreateParameter();
      tableParameter.ParameterName = "@tableName";
      tableParameter.Value = tableName;
      existsCommand.Parameters.Add(tableParameter);
      if (Convert.ToInt32(await existsCommand.ExecuteScalarAsync()) == 0)
        return;

      string escapedTableName = tableName.Replace("`", "``", StringComparison.Ordinal);
      string addressColumns = enumTypeData == EnumTypeDataLocation.Good
        ? "`OfficeAddress`, `PhoneForOfficeAddress`, `AgentAddress`, `PhoneForAgentAddress`"
        : "NULL, NULL, NULL, NULL";

      await using (var copyCommand = db.Database.GetDbConnection().CreateCommand())
      {
        copyCommand.CommandText = $@"
          INSERT IGNORE INTO `ClientDeliveries`
            (`Id`, `Name`, `OfficeAddress`, `PhoneForOfficeAddress`,
             `AgentAddress`, `PhoneForAgentAddress`, `Description`, `EnumTypeData`,
             `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`,
             `DeletedFlag`, `EnableFlag`, `SyncFlag`)
          SELECT `Id`, `Name`, {addressColumns}, `Description`, {(int)enumTypeData},
                 `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`,
                 `DeletedFlag`, `EnableFlag`, `SyncFlag`
          FROM `{escapedTableName}`;";
        await copyCommand.ExecuteNonQueryAsync();
      }

      await using var validateCommand = db.Database.GetDbConnection().CreateCommand();
      validateCommand.CommandText = $@"
        SELECT COUNT(*)
        FROM `{escapedTableName}` legacy
        LEFT JOIN `ClientDeliveries` merged
          ON merged.`Id` = legacy.`Id`
         AND merged.`EnumTypeData` = {(int)enumTypeData}
        WHERE merged.`Id` IS NULL;";
      int missingCount = Convert.ToInt32(await validateCommand.ExecuteScalarAsync());
      if (missingCount > 0)
      {
        throw new InvalidOperationException(
          $"Không thể chuyển đầy đủ {missingCount} bản ghi từ {tableName} " +
          "sang ClientDeliveries. Có thể Id bị trùng giữa Clients và Deliveries.");
      }
    }

    private static async Task EnsureClientDeliveryForeignKeyAsync(
      MySqlDbContext db,
      string tableName,
      string columnName,
      string constraintName)
    {
      var foreignKeys = new List<(string Name, string ReferencedTable)>();
      await using (var findCommand = db.Database.GetDbConnection().CreateCommand())
      {
        findCommand.CommandText = @"
          SELECT CONSTRAINT_NAME, REFERENCED_TABLE_NAME
          FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = @tableName
            AND COLUMN_NAME = @columnName
            AND REFERENCED_TABLE_NAME IS NOT NULL;";
        var tableParameter = findCommand.CreateParameter();
        tableParameter.ParameterName = "@tableName";
        tableParameter.Value = tableName;
        findCommand.Parameters.Add(tableParameter);
        var columnParameter = findCommand.CreateParameter();
        columnParameter.ParameterName = "@columnName";
        columnParameter.Value = columnName;
        findCommand.Parameters.Add(columnParameter);

        await using var reader = await findCommand.ExecuteReaderAsync();
        while (await reader.ReadAsync())
          foreignKeys.Add((reader.GetString(0), reader.GetString(1)));
      }

      if (foreignKeys.Count == 1 && string.Equals(
            foreignKeys[0].ReferencedTable, "ClientDeliveries",
            StringComparison.OrdinalIgnoreCase))
        return;

      foreach (var foreignKey in foreignKeys)
      {
        string escapedForeignKey = foreignKey.Name.Replace(
          "`", "``", StringComparison.Ordinal);
        string escapedTable = tableName.Replace("`", "``", StringComparison.Ordinal);
        await using var dropCommand = db.Database.GetDbConnection().CreateCommand();
        dropCommand.CommandText =
          $"ALTER TABLE `{escapedTable}` DROP FOREIGN KEY `{escapedForeignKey}`;";
        await dropCommand.ExecuteNonQueryAsync();
      }

      string safeTableName = tableName.Replace("`", "``", StringComparison.Ordinal);
      string safeColumnName = columnName.Replace("`", "``", StringComparison.Ordinal);
      string safeConstraintName = constraintName.Replace("`", "``", StringComparison.Ordinal);
      await using var addCommand = db.Database.GetDbConnection().CreateCommand();
      addCommand.CommandText = $@"
        ALTER TABLE `{safeTableName}`
        ADD CONSTRAINT `{safeConstraintName}`
        FOREIGN KEY (`{safeColumnName}`) REFERENCES `ClientDeliveries` (`Id`);";
      await addCommand.ExecuteNonQueryAsync();
    }

    private static async Task EnsureTypeGoodsWasteColumnsAsync(
      MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using (var tableCommand = db.Database.GetDbConnection().CreateCommand())
        {
          tableCommand.CommandText = @"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'TypeGoods';";
          if (Convert.ToInt32(await tableCommand.ExecuteScalarAsync()) == 0)
            return;
        }

        var columns = new Dictionary<string, string>
        {
          ["EnumWasteType"] = "int NOT NULL DEFAULT 0",
          ["ProductGroupId"] = "char(36) NULL",
          ["TagMappingReport"] = "longtext NULL"
        };

        foreach (var column in columns)
        {
          await using var checkCommand =
            db.Database.GetDbConnection().CreateCommand();
          checkCommand.CommandText = @"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = 'TypeGoods'
              AND COLUMN_NAME = @columnName;";

          var parameter = checkCommand.CreateParameter();
          parameter.ParameterName = "@columnName";
          parameter.Value = column.Key;
          checkCommand.Parameters.Add(parameter);

          if (Convert.ToInt32(await checkCommand.ExecuteScalarAsync()) > 0)
            continue;

          await using var addCommand =
            db.Database.GetDbConnection().CreateCommand();
          addCommand.CommandText =
            $"ALTER TABLE `TypeGoods` ADD COLUMN `{column.Key}` {column.Value};";
          await addCommand.ExecuteNonQueryAsync();
        }
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureProductTableAndMigrateTypeGoodsAsync(
      MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        async Task<bool> ColumnExistsAsync(string tableName, string columnName)
        {
          await using var command = db.Database.GetDbConnection().CreateCommand();
          command.CommandText = @"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = @tableName
              AND COLUMN_NAME = @columnName;";
          var tableParameter = command.CreateParameter();
          tableParameter.ParameterName = "@tableName";
          tableParameter.Value = tableName;
          command.Parameters.Add(tableParameter);
          var columnParameter = command.CreateParameter();
          columnParameter.ParameterName = "@columnName";
          columnParameter.Value = columnName;
          command.Parameters.Add(columnParameter);
          return Convert.ToInt32(await command.ExecuteScalarAsync()) > 0;
        }

        if (!await ColumnExistsAsync("Products", "EnumTypeData"))
        {
          await using var addTypeColumn = db.Database.GetDbConnection().CreateCommand();
          addTypeColumn.CommandText = @"
            ALTER TABLE `Products`
            ADD COLUMN `EnumTypeData` int NOT NULL DEFAULT 2;";
          await addTypeColumn.ExecuteNonQueryAsync();
        }

        await using (var normalizeProducts = db.Database.GetDbConnection().CreateCommand())
        {
          normalizeProducts.CommandText = @"
            UPDATE `Products`
            SET `EnumTypeData` = 2
            WHERE `EnumTypeData` = 0;";
          await normalizeProducts.ExecuteNonQueryAsync();
        }

        await using var legacyTableExists = db.Database.GetDbConnection().CreateCommand();
        legacyTableExists.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.TABLES
          WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'TypeGoods';";
        bool hasLegacyTypeGoods =
          Convert.ToInt32(await legacyTableExists.ExecuteScalarAsync()) > 0;

        if (hasLegacyTypeGoods)
        {
          await using var copyTypeGoods = db.Database.GetDbConnection().CreateCommand();
          copyTypeGoods.CommandText = @"
            INSERT IGNORE INTO `Products`
              (`Id`, `Code`, `Name`, `Description`, `EnumWasteType`,
               `TagMappingReport`, `EnumTypeData`, `ProductGroupId`,
               `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`,
               `DeletedFlag`, `EnableFlag`, `SyncFlag`)
            SELECT `Id`, `Code`, `Name`, `Description`, `EnumWasteType`,
                   `TagMappingReport`, 1, `ProductGroupId`,
                   `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`,
                   `DeletedFlag`, `EnableFlag`, `SyncFlag`
            FROM `TypeGoods`;";
          await copyTypeGoods.ExecuteNonQueryAsync();
        }

        var foreignKeys = new List<(string Name, string ReferencedTable)>();
        await using (var findForeignKeys = db.Database.GetDbConnection().CreateCommand())
        {
          findForeignKeys.CommandText = @"
            SELECT CONSTRAINT_NAME, REFERENCED_TABLE_NAME
            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = 'RecordTrucks'
              AND COLUMN_NAME = 'TypeGoodsId'
              AND REFERENCED_TABLE_NAME IS NOT NULL;";
          await using var reader = await findForeignKeys.ExecuteReaderAsync();
          while (await reader.ReadAsync())
            foreignKeys.Add((reader.GetString(0), reader.GetString(1)));
        }

        if (foreignKeys.Count != 1 || !string.Equals(
              foreignKeys[0].ReferencedTable, "Products",
              StringComparison.OrdinalIgnoreCase))
        {
          foreach (var foreignKey in foreignKeys)
          {
            string escapedName = foreignKey.Name.Replace("`", "``", StringComparison.Ordinal);
            await using var dropCommand = db.Database.GetDbConnection().CreateCommand();
            dropCommand.CommandText =
              $"ALTER TABLE `RecordTrucks` DROP FOREIGN KEY `{escapedName}`;";
            await dropCommand.ExecuteNonQueryAsync();
          }

          await using var addForeignKey = db.Database.GetDbConnection().CreateCommand();
          addForeignKey.CommandText = @"
            ALTER TABLE `RecordTrucks`
            ADD CONSTRAINT `FK_RecordTrucks_Products_TypeGoodsId`
            FOREIGN KEY (`TypeGoodsId`) REFERENCES `Products` (`Id`);";
          await addForeignKey.ExecuteNonQueryAsync();
        }

        await using var indexExists = db.Database.GetDbConnection().CreateCommand();
        indexExists.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.STATISTICS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'Products'
            AND INDEX_NAME = 'IX_Products_EnumTypeData_DeletedFlag';";
        if (Convert.ToInt32(await indexExists.ExecuteScalarAsync()) == 0)
        {
          await using var createIndex = db.Database.GetDbConnection().CreateCommand();
          createIndex.CommandText = @"
            CREATE INDEX `IX_Products_EnumTypeData_DeletedFlag`
            ON `Products` (`EnumTypeData`, `DeletedFlag`);";
          await createIndex.ExecuteNonQueryAsync();
        }
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureRecordWeightReasonDeleteColumnAsync(
      MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using var checkColumnCommand =
          db.Database.GetDbConnection().CreateCommand();
        checkColumnCommand.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'RecordWeights'
            AND COLUMN_NAME = 'ReasonDelete';";

        var columnCount = Convert.ToInt32(
          await checkColumnCommand.ExecuteScalarAsync());
        if (columnCount > 0)
          return;

        await using var addColumnCommand =
          db.Database.GetDbConnection().CreateCommand();
        addColumnCommand.CommandText = @"
          ALTER TABLE `RecordWeights`
          ADD COLUMN `ReasonDelete` longtext NULL;";
        await addColumnCommand.ExecuteNonQueryAsync();
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureProductTagMappingReportColumnAsync(
      MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        await using var checkColumnCommand =
          db.Database.GetDbConnection().CreateCommand();
        checkColumnCommand.CommandText = @"
          SELECT COUNT(*)
          FROM INFORMATION_SCHEMA.COLUMNS
          WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_NAME = 'Products'
            AND COLUMN_NAME = 'TagMappingReport';";

        var columnCount = Convert.ToInt32(
          await checkColumnCommand.ExecuteScalarAsync());
        if (columnCount > 0)
          return;

        await using var addColumnCommand =
          db.Database.GetDbConnection().CreateCommand();
        addColumnCommand.CommandText = @"
          ALTER TABLE `Products`
          ADD COLUMN `TagMappingReport` longtext NULL;";
        await addColumnCommand.ExecuteNonQueryAsync();
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureAppConfigPrintColumnsAsync(MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        static async Task<bool> ColumnExistsAsync(
          MySqlDbContext context,
          string columnName)
        {
          await using var command =
            context.Database.GetDbConnection().CreateCommand();
          command.CommandText = @"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = 'AppConfigs'
              AND COLUMN_NAME = @columnName;";

          var parameter = command.CreateParameter();
          parameter.ParameterName = "@columnName";
          parameter.Value = columnName;
          command.Parameters.Add(parameter);

          return Convert.ToInt32(await command.ExecuteScalarAsync()) > 0;
        }

        if (!await ColumnExistsAsync(db, "NumberPrintA4"))
        {
          await using var addNumberPrintA4Command =
            db.Database.GetDbConnection().CreateCommand();
          addNumberPrintA4Command.CommandText = @"
            ALTER TABLE `AppConfigs`
            ADD COLUMN `NumberPrintA4` int NULL DEFAULT 1;";
          await addNumberPrintA4Command.ExecuteNonQueryAsync();
        }

        if (!await ColumnExistsAsync(db, "NumberPrintLabel"))
        {
          await using var addNumberPrintLabelCommand =
            db.Database.GetDbConnection().CreateCommand();
          addNumberPrintLabelCommand.CommandText = @"
            ALTER TABLE `AppConfigs`
            ADD COLUMN `NumberPrintLabel` int NULL DEFAULT 1;";
          await addNumberPrintLabelCommand.ExecuteNonQueryAsync();
        }
        if (!await ColumnExistsAsync(db, "NumberDayPermitDeleteData"))
        {
          await using var command = db.Database.GetDbConnection().CreateCommand();
          command.CommandText = @"
            ALTER TABLE `AppConfigs`
            ADD COLUMN `NumberDayPermitDeleteData` int NULL DEFAULT 1;";
          await command.ExecuteNonQueryAsync();
        }
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

    private static async Task EnsureUserPasswordPolicyColumnsAsync(
      MySqlDbContext db)
    {
      await db.Database.OpenConnectionAsync();
      try
      {
        var columns = new Dictionary<string, string>
        {
          ["PasswordChangedAt"] = "datetime(6) NULL",
          ["MustChangePassword"] = "tinyint(1) NOT NULL DEFAULT 0",
          ["FailedLoginAttempts"] = "int NOT NULL DEFAULT 0",
          ["IsLoginLocked"] = "tinyint(1) NOT NULL DEFAULT 0",
          ["PasswordHistory"] = "longtext NULL",
          ["SsoLoginId"] = "varchar(255) NULL"
        };

        foreach (var column in columns)
        {
          await using var checkCommand =
            db.Database.GetDbConnection().CreateCommand();
          checkCommand.CommandText = @"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = DATABASE()
              AND TABLE_NAME = 'Users'
              AND COLUMN_NAME = @columnName;";
          var parameter = checkCommand.CreateParameter();
          parameter.ParameterName = "@columnName";
          parameter.Value = column.Key;
          checkCommand.Parameters.Add(parameter);

          if (Convert.ToInt32(await checkCommand.ExecuteScalarAsync()) > 0)
            continue;

          await using var addCommand =
            db.Database.GetDbConnection().CreateCommand();
          addCommand.CommandText =
            $"ALTER TABLE `Users` ADD COLUMN `{column.Key}` {column.Value};";
          await addCommand.ExecuteNonQueryAsync();
        }
      }
      finally
      {
        await db.Database.CloseConnectionAsync();
      }
    }

  }
}
