using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HelperManager
{
  public static class LogHelper
  {
    private static readonly object _lock = new object();
    public static void LogErrorToFileLog(Exception exception, string folderFileLog)
    {
      try
      {
        lock (_lock)
        {
          Directory.CreateDirectory(folderFileLog);
          string fileName = $"Filelog_{DateTime.Now:yyyyMMdd}.txt";
          string nameFileLog = Path.Combine(folderFileLog, fileName);

          string contentLog =
              $"{DateTime.Now:yyyy/MM/dd HH:mm:ss}\r\n" +
              $"Content: {exception}\r\n";

          File.AppendAllText(nameFileLog, contentLog);
        }
      }
      catch
      {
        // Không để lỗi ghi file log che mất exception nghiệp vụ ban đầu.
        System.Diagnostics.Debug.WriteLine(exception);
      }
    }
  }


}
