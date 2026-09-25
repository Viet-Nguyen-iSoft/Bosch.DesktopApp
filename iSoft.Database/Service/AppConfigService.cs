using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Service
{
  public class AppConfigService
  {
    private static readonly SemaphoreSlim LabelSequenceLock = new(1, 1);

    public async Task<List<AppConfig>> GetAllAsync(bool IsContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new AppConfigRepository(context);
      return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
    }
    public async Task<AppConfig?> GetAppConfigAsync(bool IsContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new AppConfigRepository(context);
      return await repository.GetFirstOrDefaultAsync(IsContainDelete).ConfigureAwait(false);
    }

    public async Task<AppConfig> AddOrUpdateAsync(AppConfig appConfig)
    {
      await using var context = new MySqlDbContext();
      var repository = new AppConfigRepository(context);
      return await repository.AddOrUpdateAsync(appConfig).ConfigureAwait(false);
    }

    public async Task<string> CreateNextLabelAsync(
      AppConfig appConfig,
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(appConfig);

      await LabelSequenceLock.WaitAsync(cancellationToken).ConfigureAwait(false);
      try
      {
        var today = DateTime.Today;
        var sequenceDateUtc = DateTime.SpecifyKind(today, DateTimeKind.Utc);
        if (!appConfig.LabelSequenceDate.HasValue ||
            appConfig.LabelSequenceDate.Value.Date != today)
        {
          appConfig.LabelSequenceDate = sequenceDateUtc;
          appConfig.LabelSequenceNumber = 1;
        }
        else
        {
          if (appConfig.LabelSequenceNumber >= 9999)
            throw new InvalidOperationException("Số thứ tự tem trong ngày đã đạt giới hạn 9999.");

          appConfig.LabelSequenceNumber++;
        }

        // MySqlDbContext dùng DateTimeKind=Utc; luôn chuẩn hóa cả giá trị còn
        // trong cache từ một lần lưu lỗi trước đó.
        appConfig.LabelSequenceDate = sequenceDateUtc;
        appConfig.UpdatedAt = DateTime.UtcNow;
        await AddOrUpdateAsync(appConfig).ConfigureAwait(false);

        return HelperManager.KeyHelper.CreateLabel(today, appConfig.LabelSequenceNumber);
      }
      finally
      {
        LabelSequenceLock.Release();
      }
    }
  }
}
