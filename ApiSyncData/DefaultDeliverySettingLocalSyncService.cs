using ApiSyncData.Req;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSyncData
{
  internal static class DefaultDeliverySettingLocalSyncService
  {
    private static readonly SemaphoreSlim SyncLock = new(1, 1);

    internal static async Task<MasterDataChangedEventArgs?> SyncAsync(
      ApiService api,
      Guid clientId,
      CancellationToken token = default)
    {
      ArgumentNullException.ThrowIfNull(api);
      if (clientId == Guid.Empty)
        throw new ArgumentException("ClientId không hợp lệ.", nameof(clientId));

      await SyncLock.WaitAsync(token).ConfigureAwait(false);
      try
      {
        AppConfigSnapshot snapshot;
        await using (var db = new MySqlDbContext())
        {
          var appConfig = await db.Set<AppConfig>()
            .AsNoTracking()
            .FirstOrDefaultAsync(item => !item.DeletedFlag, token)
            .ConfigureAwait(false)
            ?? throw new InvalidOperationException(
              "Không thể đồng bộ DefaultDeliverySetting vì local chưa có AppConfig.");

          if (appConfig.SyncFlag)
            return null;

          snapshot = AppConfigSnapshot.From(appConfig);
        }

        var request = new DefaultDeliverySettingUpdateRequest
        {
          Client = new DefaultDeliverySettingUpdateClient
          {
            Id = clientId,
            Name = snapshot.Company,
            OfficeAddress = snapshot.OfficeAddress,
            PhoneForOfficeAddress = snapshot.PhoneForOfficeAddress,
            AgentAddress = snapshot.AgentAddress,
            PhoneForAgentAddress = snapshot.PhoneForAgentAddress,
          }
        };
        await api.UpdateDefaultDeliverySettingAsync(request, cancellationToken: token)
          .ConfigureAwait(false);

        await using var updateDb = new MySqlDbContext();
        var current = await updateDb.Set<AppConfig>()
          .FirstOrDefaultAsync(item => item.Id == snapshot.Id, token)
          .ConfigureAwait(false);
        if (current == null || current.SyncFlag || !snapshot.HasSameReportData(current))
          return null;

        current.SyncFlag = true;
        int affectedRows = await updateDb.SaveChangesAsync(token).ConfigureAwait(false);
        if (affectedRows == 0)
          return null;

        return new MasterDataChangedEventArgs(
          typeof(AppConfig),
          Array.Empty<Guid>(),
          new[] { current.Id },
          Array.Empty<Guid>(),
          affectedRows);
      }
      finally
      {
        SyncLock.Release();
      }
    }

    private sealed record AppConfigSnapshot(
      Guid Id,
      string? Company,
      string? OfficeAddress,
      string? PhoneForOfficeAddress,
      string? AgentAddress,
      string? PhoneForAgentAddress)
    {
      internal static AppConfigSnapshot From(AppConfig value) => new(
        value.Id,
        value.Company,
        value.OfficeAddress,
        value.PhoneForOfficeAddress,
        value.AgentAddress,
        value.PhoneForAgentAddress);

      internal bool HasSameReportData(AppConfig value) =>
        string.Equals(Company, value.Company, StringComparison.Ordinal) &&
        string.Equals(OfficeAddress, value.OfficeAddress, StringComparison.Ordinal) &&
        string.Equals(PhoneForOfficeAddress, value.PhoneForOfficeAddress,
          StringComparison.Ordinal) &&
        string.Equals(AgentAddress, value.AgentAddress, StringComparison.Ordinal) &&
        string.Equals(PhoneForAgentAddress, value.PhoneForAgentAddress,
          StringComparison.Ordinal);
    }
  }
}
