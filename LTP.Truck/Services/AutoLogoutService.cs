using HelperManager;
using LTP.Truck.Controls;

namespace LTP.Truck.Services
{
  /// <summary>
  /// Quản lý thời hạn của một phiên đăng nhập. Mỗi lần Start sẽ hủy phiên đếm
  /// trước đó và bắt đầu lại từ đầu.
  /// </summary>
  public sealed class AutoLogoutService : IDisposable
  {
    private readonly object _syncRoot = new();
    private CancellationTokenSource? _cancellation;
    private Task? _countdownTask;
    private SynchronizationContext? _synchronizationContext;

    public event EventHandler? Elapsed;

    public void Start(int? timeoutMinutes)
    {
      Stop();

      if (!timeoutMinutes.HasValue || timeoutMinutes.Value <= 0)
        return;

      var cancellation = new CancellationTokenSource();
      lock (_syncRoot)
      {
        _cancellation = cancellation;
        _synchronizationContext = SynchronizationContext.Current;
        _countdownTask = RunCountdownAsync(
          TimeSpan.FromMinutes(timeoutMinutes.Value),
          cancellation.Token);
      }
    }

    public void Stop()
    {
      CancellationTokenSource? cancellation;
      lock (_syncRoot)
      {
        cancellation = _cancellation;
        _cancellation = null;
        _countdownTask = null;
        _synchronizationContext = null;
      }

      if (cancellation == null)
        return;

      cancellation.Cancel();
      cancellation.Dispose();
    }

    private async Task RunCountdownAsync(TimeSpan timeout, CancellationToken cancellationToken)
    {
      try
      {
        await Task.Delay(timeout, cancellationToken).ConfigureAwait(false);
        if (cancellationToken.IsCancellationRequested)
          return;

        var synchronizationContext = _synchronizationContext;
        if (synchronizationContext != null)
        {
          synchronizationContext.Post(_ => RaiseElapsed(cancellationToken), null);
        }
        else
        {
          RaiseElapsed(cancellationToken);
        }
      }
      catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
      {
        // Phiên đã đăng xuất thủ công hoặc được bắt đầu lại.
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private void RaiseElapsed(CancellationToken cancellationToken)
    {
      if (cancellationToken.IsCancellationRequested)
        return;

      try
      {
        Elapsed?.Invoke(this, EventArgs.Empty);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    public void Dispose()
    {
      Stop();
      GC.SuppressFinalize(this);
    }
  }
}
