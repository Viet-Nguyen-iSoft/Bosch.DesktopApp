using ApiSyncData;
using HelperManager;
using iSoft.Database.Models;
using LTP.Truck.Custom;
using LTP.Truck.Forms;
using SixLabors.Fonts;
using System.Diagnostics;
using static LTP.Truck.EnumData;
using AppCore = LTP.Truck.Controls.AppCore;

namespace LTP.Truck
{
  public partial class FrmMain : Form
  {
    public event EventHandler? OnChangeProductGroup;
    public event EventHandler? OnChangeProduct;
    public event EventHandler? OnChangeTare;
    public FrmMain()
    {
      InitializeComponent();

      this.FormBorderStyle = FormBorderStyle.None;
      this.WindowState = FormWindowState.Maximized;

      this.Load += FrmMain_Load;
    }

    #region Instance
    private static FrmMain _Instance = null;
    public static FrmMain Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmMain();
        return _Instance;
      }
    }
    #endregion

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Enter)
      {
        if (this.ActiveControl is Button btn && btn.Focused)
        {
          return true;
        }
        if (this.ActiveControl is RJButton btnRJ && btnRJ.Focused)
        {
          return true;
        }
        if (this.ActiveControl is UserControl uc && uc.Focused)
        {
          return true;
        }
        if (this.ActiveControl is DataGridView dgv && dgv.Focused)
        {
          return true;
        }
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }


    #region ChangePage
    public void ChangePage(EnumScreen appModulSupport)
    {
      switch (appModulSupport)
      {
        case EnumScreen.Waiting:
          OpenChildForm(appModulSupport, FrmWaiting.Instance);
          break;
        case EnumScreen.Operation:
          OpenChildForm(appModulSupport, FrmOperation.Instance);
          break;
      }
    }

    private Form CurrentForm;
    public void OpenChildForm(EnumScreen enumScreen, Form childForm)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          OpenChildForm(enumScreen, childForm);
        }));
        return;
      }

      bool Is_same_form = false;
      if (this.panelMain.Tag != null)
      {
        if (this.panelMain.Tag is Tuple<EnumScreen, Form>)
        {
          Tuple<EnumScreen, Form> TagAsForm = (Tuple<EnumScreen, Form>)(this.panelMain.Tag);
          if (TagAsForm.Item1 == enumScreen)
          {
            Is_same_form = true;
          }
        }
      }
      if (Is_same_form == false)
      {
        if (CurrentForm != null)
        {
          CurrentForm.Visible = false;
        }
        this.panelMain.Controls.Clear();
        this.panelMain.Tag = Tuple.Create(enumScreen, childForm);
        CurrentForm = childForm;
        childForm.TopLevel = false;
        childForm.FormBorderStyle = FormBorderStyle.None;
        childForm.Dock = DockStyle.Fill;
        childForm.BringToFront();
        this.panelMain.Controls.Add(childForm);
        childForm.Show();
      }
      else
      {

      }
    }
    #endregion

    public void ShowMainForm()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ShowMainForm();
        }));
        return;
      }
      try
      {
        this.WindowState = FormWindowState.Maximized;
        this.ShowInTaskbar = true;
        this.ShowDialog();
        this.BringToFront();
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    // Khai báo trong class FrmMain
    private readonly CancellationTokenSource _syncCts = new();
    private readonly CancellationTokenSource _syncCts02 = new();
    private Task? _syncTask;
    private Task? _localDataSyncTask;

    private void FrmMain_Load(object? sender, EventArgs e)
    {
      try
      {
        if (AppCore.Ins._station!=null)
        {
          _syncTask ??= PeriodicRunner.RunEvery5SecondsAsync(AppCore.Ins._station.Id, _syncCts.Token);
          _localDataSyncTask ??= LocalDataSyncService.RunEvery5SecondsAsync(pathFolderSrc: Application.StartupPath, _syncCts02.Token);
          PeriodicRunner.EntityChanged += (sender, e) =>
          {
            if (e.EntityType == typeof(ProductGroup))
            {
              OnChangeProductGroup?.Invoke(this, e);
            }
            else if (e.EntityType == typeof(Product))
            {
              OnChangeProduct?.Invoke(this, e);
            }
            else if (e.EntityType == typeof(CategoryTare))
            {
              OnChangeTare?.Invoke(this, e);
            }
          };
        }
        
        AppCore.Ins.CheckConnectServer();
        AppCore.Ins.ConnectWeight();
        //CheckOpenMulApp();
        ChangePage(EnumScreen.Waiting);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private void CheckOpenMulApp()
    {
      string procName = Process.GetCurrentProcess().ProcessName;
      Process[] processes = Process.GetProcessesByName(procName);
      if (processes.Length > 1)
      {
        Program.CloseApp();
      }
    }

    public void MiniTab()
    {
      this.WindowState = FormWindowState.Minimized;
    }

  }
}
