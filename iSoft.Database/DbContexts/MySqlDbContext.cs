using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.DbContexts
{
  public class MySqlDbContext : CommonDbContext
  {
    private static readonly Lazy<DatabaseConfiguration> Configuration = new(
      CreateConfiguration,
      LazyThreadSafetyMode.ExecutionAndPublication);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (optionsBuilder.IsConfigured)
        return;

      var configuration = Configuration.Value;
      optionsBuilder.UseMySql(
        configuration.ConnectionString,
        configuration.ServerVersion);
    }

    private static DatabaseConfiguration CreateConfiguration()
    {
      string envPath = Path.Combine(AppContext.BaseDirectory, ".env");
      if (!File.Exists(envPath))
        throw new FileNotFoundException("Không tìm thấy file cấu hình database.", envPath);

      DotNetEnv.Env.Load(envPath);
      string? host = Environment.GetEnvironmentVariable("DB_CONFIG_ADDRESS");
      string? port = Environment.GetEnvironmentVariable("DB_CONFIG_PORT");
      string? user = Environment.GetEnvironmentVariable("DB_CONFIG_USERNAME");
      string? password = Environment.GetEnvironmentVariable("DB_CONFIG_PASSWORD");
      string? databaseName = Environment.GetEnvironmentVariable("DB_CONFIG_DATABASE_NAME");

      if (string.IsNullOrWhiteSpace(host) ||
          string.IsNullOrWhiteSpace(port) ||
          string.IsNullOrWhiteSpace(user) ||
          string.IsNullOrWhiteSpace(databaseName))
      {
        throw new InvalidOperationException(
          "Cấu hình database trong file .env chưa đầy đủ.");
      }

      var connectionString =
        $"Server={host};" +
        $"Port={port};" +
        $"Database={databaseName};" +
        $"User ID={user};" +
        $"Password={password};" +
        "DateTimeKind=Utc;";

      return new DatabaseConfiguration(
        connectionString,
        ServerVersion.AutoDetect(connectionString));
    }

    private sealed record DatabaseConfiguration(
      string ConnectionString,
      ServerVersion ServerVersion);
  }
}
