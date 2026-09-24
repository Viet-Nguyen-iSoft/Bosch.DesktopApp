using ApiCICD;
using LTP.Truck.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LTP.Truck.Forms
{
  public partial class PopupApplyVersionNew : Form
  {
    public event EventHandler<string>? OnSendApply;
    public PopupApplyVersionNew()
    {
      InitializeComponent();
      this.TopMost = true;
      this.Load += PopupApplyVersionNew_Load;
      this.Shown += PopupApplyVersionNew_Shown;
    }

    private void PopupApplyVersionNew_Load(object? sender, EventArgs e)
    {
      lbVersionCurrent.Text = AppCore.Ins._appConfig.Version;
    }
    private GitHubReleaseDTO? GitHubReleaseDTO { get; set; }
    private async void PopupApplyVersionNew_Shown(object? sender, EventArgs e)
    {
      try
      {
        GitHubReleaseDTO = await ApiGetRelease.GetLatestReleaseAsync();
        if (GitHubReleaseDTO != null)
        {
          LoadInforVersion(GitHubReleaseDTO);
          CheckNewVersion();
        }
        else
        {
          LoadInforError("Lỗi: NULL");
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        LoadInforError(ex.Message);
      }
    }

    public void LoadInforVersion(GitHubReleaseDTO gitHubRelease)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadInforVersion(gitHubRelease);
        }));
        return;
      }

      lbVersion.Text = gitHubRelease.TagName;
      txtCommit.Text = gitHubRelease.CommitMessage;
    }

    public void LoadInforError(string msg)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadInforError(msg);
        }));
        return;
      }

      lbVersion.Text = msg;
      txtCommit.Text = string.Empty;
    }

    public void CheckNewVersion()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          CheckNewVersion();
        }));
        return;
      }


      picLoading.Visible = false;
      if (lbVersion.Text == lbVersionCurrent.Text)
      {
        btnDownload.Visible = false;
        txtCommit.Text = "Không có bản cập nhật";
      }
      else
      {
        btnDownload.Visible = true;
      }
    }

    public void LockByDownload(bool lockUI)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LockByDownload(lockUI);
        }));
        return;
      }

      this.Enabled = !lockUI;

      if (lockUI)
      {
        progressBar1.Visible = true;
        progressBar1.Style = ProgressBarStyle.Marquee;
        progressBar1.MarqueeAnimationSpeed = 10;
      }
      else
      {
        progressBar1.Visible = false;
      }
    }



    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private async void btnDownload_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        LockByDownload(true);
        string versionUpdate = GitHubReleaseDTO?.TagName.Replace(".", "_") ?? string.Empty;
        if (!string.IsNullOrEmpty(versionUpdate))
        {
          string folder = Path.Combine(Application.StartupPath, "Versions");

          if (!Directory.Exists(folder))
          {
            Directory.CreateDirectory(folder);
          }

          string pathFileZip = Path.Combine(folder, $"update_{versionUpdate}.zip");
          var ok = await ApiGetRelease.DownloadReleaseAsync(GitHubReleaseDTO, pathFileZip);
          if (ok)
          {
            btnApply.Visible = true;
            btnDownload.Visible = false;
          }
        }
        else
        {
          MessageBox.Show("Version lỗi");
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
      finally
      {
        LockByDownload(false);
      }
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      OnSendApply?.Invoke(this, GitHubReleaseDTO?.TagName ?? string.Empty);
      this.Close();
    }
  }
}
