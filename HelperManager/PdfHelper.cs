using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace HelperManager
{
  public class PdfHelper
  {
    private static IBrowser? _browser;
    private static readonly SemaphoreSlim _lock = new(1, 1);

    /// <summary>
    /// Khởi tạo Chromium (chỉ chạy 1 lần)
    /// </summary>
    //public static async Task InitAsync()
    //{
    //  if (_browser != null)
    //    return;

    //  await _lock.WaitAsync();

    //  try
    //  {
    //    if (_browser != null)
    //      return;

    //    var fetcher = new BrowserFetcher();
    //    await fetcher.DownloadAsync();

    //    _browser = await Puppeteer.LaunchAsync(new LaunchOptions
    //    {
    //      Headless = true,
    //      Args = new[]
    //        {
    //                "--disable-gpu",
    //                "--disable-dev-shm-usage",
    //                "--no-first-run",
    //                "--no-default-browser-check"
    //            }
    //    });
    //  }
    //  finally
    //  {
    //    _lock.Release();
    //  }
    //}

    public static async Task InitAsync()
    {
      if (_browser != null)
        return;

      await _lock.WaitAsync();

      try
      {
        if (_browser != null)
          return;

        var fetcher = new BrowserFetcher();
        await fetcher.DownloadAsync();

        _browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
          Headless = true,
          HeadlessMode = HeadlessMode.Shell,
          Args = new[]
            {
                "--disable-gpu",
                "--disable-dev-shm-usage",
                "--no-first-run",
                "--no-default-browser-check"
          }
        });
      }
      finally
      {
        _lock.Release();
      }
    }

    /// <summary>
    /// Chuyển HTML thành PDF
    /// </summary>
    public static async Task HtmlToPdfAsync(string htmlFile, string pdfFile)
    {
      await InitAsync();

      if (_browser == null)
        throw new Exception("Browser chưa được khởi tạo.");

      await using var page = await _browser.NewPageAsync();

      await page.GoToAsync(
          new Uri(htmlFile).AbsoluteUri,
          WaitUntilNavigation.Load);

      await page.PdfAsync(pdfFile, new PdfOptions
      {
        Format = PaperFormat.A4,
        PrintBackground = true,
        MarginOptions = new MarginOptions
        {
          Top = "10mm",
          Bottom = "10mm",
          Left = "10mm",
          Right = "10mm"
        }
      });

      await page.CloseAsync();
    }

    public static async Task HtmlToPdfWithoutConsoleAsync(string htmlFile, string pdfFile)
    {
      // Chrome's DownloadAsync runs setup.exe even for cached installations.
      // Headless Shell avoids that setup process; its own console is hidden below.
      var fetcher = new BrowserFetcher(new BrowserFetcherOptions
      {
        Browser = SupportedBrowser.ChromeHeadlessShell,
      });
      var installedBrowser = await fetcher.DownloadAsync();
      using var launcher = new ChromeLauncher(installedBrowser.GetExecutablePath(), new LaunchOptions
      {
        ExecutablePath = installedBrowser.GetExecutablePath(),
        Headless = true,
        HeadlessMode = HeadlessMode.Shell,
        Args = new[] { "--disable-gpu", "--no-first-run", "--no-default-browser-check" },
      });
      // Set these before StartAsync: hiding an already-started process can still flash.
      launcher.Process.StartInfo.UseShellExecute = false;
      launcher.Process.StartInfo.CreateNoWindow = true;
      launcher.Process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
      try
      {
        await launcher.StartAsync();
        await using var browser = await Puppeteer.ConnectAsync(new ConnectOptions
        {
          BrowserWSEndpoint = launcher.EndPoint,
        });
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(new Uri(Path.GetFullPath(htmlFile)).AbsoluteUri, WaitUntilNavigation.Load);
        await page.PdfAsync(pdfFile, new PdfOptions
        {
          Format = PaperFormat.A4,
          PrintBackground = true,
          MarginOptions = new MarginOptions
          {
            Top = "10mm",
            Bottom = "10mm",
            Left = "10mm",
            Right = "10mm",
          },
        });
      }
      finally
      {
        await launcher.KillAsync();
      }
    }

    /// <summary>
    /// Đóng Chromium khi thoát chương trình
    /// </summary>
    //public static async Task DisposeAsync()
    //{
    //  if (_browser == null)
    //    return;

    //  try
    //  {
    //    var process = _browser.Process;

    //    await _browser.CloseAsync();
    //    await _browser.DisposeAsync();

    //    if (process != null && !process.HasExited)
    //    {
    //      process.Kill(true);
    //      process.WaitForExit();
    //    }
    //  }
    //  catch
    //  {
    //  }
    //  finally
    //  {
    //    _browser = null;
    //  }
    //}

    public static async Task DisposeAsync()
    {
      await _lock.WaitAsync();

      try
      {
        if (_browser == null)
          return;

        var browser = _browser;
        var process = browser.Process;

        try
        {
          await browser.CloseAsync();
        }
        catch
        {
          // CloseAsync thất bại thì vẫn tiếp tục Dispose và dừng process.
        }

        try
        {
          await browser.DisposeAsync();
        }
        catch
        {
          // Best-effort cleanup.
        }

        try
        {
          if (process != null && !process.HasExited)
          {
            process.Kill(entireProcessTree: true);
            await process.WaitForExitAsync();
          }
        }
        catch
        {
          // Process có thể đã tự thoát giữa lúc kiểm tra và Kill.
        }
        finally
        {
          process?.Dispose();
        }
      }
      finally
      {
        _browser = null;
        _lock.Release();
      }
    }
  }
}
