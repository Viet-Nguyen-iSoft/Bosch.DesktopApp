using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HelperManager
{
  public static class FileLockHelper
  {
    private const int ErrorSuccess = 0;
    private const int ErrorMoreData = 234;
    private const int MaxAppName = 255;
    private const int MaxServiceName = 63;

    public static bool IsFileLocked(string filePath)
    {
      if (!File.Exists(filePath))
        return false;

      try
      {
        using var stream = new FileStream(
          filePath,
          FileMode.Open,
          FileAccess.ReadWrite,
          FileShare.None);
        return false;
      }
      catch (IOException)
      {
        return true;
      }
      catch (UnauthorizedAccessException)
      {
        return true;
      }
    }

    public static async Task<bool> TryCloseLockingProcessesAsync(
      string filePath,
      int timeoutMilliseconds = 5000)
    {
      var lockingProcesses = GetLockingProcesses(filePath)
        .Where(process => process.Id != Environment.ProcessId)
        .GroupBy(process => process.Id)
        .Select(group => group.First())
        .ToList();

      if (lockingProcesses.Count == 0)
        return !IsFileLocked(filePath);

      foreach (var process in lockingProcesses)
      {
        using (process)
        {
          try
          {
            if (process.HasExited)
              continue;

            if (!process.CloseMainWindow())
              return false;

            bool exited = await Task.Run(() => process.WaitForExit(timeoutMilliseconds));
            if (!exited)
              return false;
          }
          catch (InvalidOperationException)
          {
            // Tiến trình đã đóng trong lúc kiểm tra.
          }
        }
      }

      for (int retry = 0; retry < 10; retry++)
      {
        if (!IsFileLocked(filePath))
          return true;

        await Task.Delay(200);
      }

      return false;
    }

    private static List<Process> GetLockingProcesses(string filePath)
    {
      uint sessionHandle;
      string sessionKey = Guid.NewGuid().ToString("N");
      int result = RmStartSession(out sessionHandle, 0, sessionKey);
      if (result != ErrorSuccess)
        throw new InvalidOperationException($"Không thể khởi tạo Restart Manager. Mã lỗi: {result}.");

      try
      {
        string[] resources = { Path.GetFullPath(filePath) };
        result = RmRegisterResources(
          sessionHandle,
          (uint)resources.Length,
          resources,
          0,
          null,
          0,
          null);

        if (result != ErrorSuccess)
          throw new InvalidOperationException($"Không thể kiểm tra tiến trình đang mở file. Mã lỗi: {result}.");

        uint processInfoNeeded = 0;
        uint processInfoCount = 0;
        uint rebootReasons = 0;
        result = RmGetList(
          sessionHandle,
          out processInfoNeeded,
          ref processInfoCount,
          null,
          ref rebootReasons);

        if (result == ErrorSuccess)
          return new List<Process>();

        if (result != ErrorMoreData)
          throw new InvalidOperationException($"Không thể lấy tiến trình đang mở file. Mã lỗi: {result}.");

        var processInfo = new RmProcessInfo[processInfoNeeded];
        processInfoCount = processInfoNeeded;
        result = RmGetList(
          sessionHandle,
          out processInfoNeeded,
          ref processInfoCount,
          processInfo,
          ref rebootReasons);

        if (result != ErrorSuccess)
          throw new InvalidOperationException($"Không thể lấy tiến trình đang mở file. Mã lỗi: {result}.");

        var processes = new List<Process>();
        for (int index = 0; index < processInfoCount; index++)
        {
          try
          {
            processes.Add(Process.GetProcessById(processInfo[index].Process.ProcessId));
          }
          catch (ArgumentException)
          {
            // Tiến trình đã kết thúc trước khi được lấy.
          }
        }

        return processes;
      }
      finally
      {
        RmEndSession(sessionHandle);
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RmUniqueProcess
    {
      public int ProcessId;
      public System.Runtime.InteropServices.ComTypes.FILETIME ProcessStartTime;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct RmProcessInfo
    {
      public RmUniqueProcess Process;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxAppName + 1)]
      public string AppName;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxServiceName + 1)]
      public string ServiceShortName;

      public uint ApplicationType;
      public uint AppStatus;
      public uint TerminalSessionId;

      [MarshalAs(UnmanagedType.Bool)]
      public bool Restartable;
    }

    [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
    private static extern int RmStartSession(
      out uint sessionHandle,
      int sessionFlags,
      string sessionKey);

    [DllImport("rstrtmgr.dll", CharSet = CharSet.Unicode)]
    private static extern int RmRegisterResources(
      uint sessionHandle,
      uint fileCount,
      string[] fileNames,
      uint applicationCount,
      RmUniqueProcess[]? applications,
      uint serviceCount,
      string[]? serviceNames);

    [DllImport("rstrtmgr.dll")]
    private static extern int RmGetList(
      uint sessionHandle,
      out uint processInfoNeeded,
      ref uint processInfoCount,
      [In, Out] RmProcessInfo[]? affectedApps,
      ref uint rebootReasons);

    [DllImport("rstrtmgr.dll")]
    private static extern int RmEndSession(uint sessionHandle);
  }
}
