using ApiSyncData.Req;
using Common;
using HelperManager;
using LTP.Truck.Controls;
using System.Diagnostics;
using System.Windows.Forms;
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmWaiting : Form
  {
    public FrmWaiting()
    {
      InitializeComponent();
      LoadConfig();
      this.Load += FrmWaiting_Load;
    }

    #region Instance
    private static FrmWaiting _Instance = null;
    public static FrmWaiting Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmWaiting();
        return _Instance;
      }
    }
    #endregion

    #region MenuClose App
    private void btnMenu_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.InvokeRequired)
        {
          this.Invoke(new Action(() =>
          {
            var frm = FrmOptionCloseApp.Instance;

            frm.StartPosition = FormStartPosition.Manual;
            var screen = Screen.PrimaryScreen.WorkingArea;
            int x = (screen.Width - frm.Width) / 2;
            int y = (screen.Height - frm.Height) / 2 - 0;
            if (y < 0) y = 0;

            frm.OnSendRestart -= Frm_OnSendRestart;
            frm.OnSendRestart += Frm_OnSendRestart;

            frm.OnSendClose -= Frm_OnSendCloseApp;
            frm.OnSendClose += Frm_OnSendCloseApp;

            frm.OnSendMini -= Frm_OnSendMini;
            frm.OnSendMini += Frm_OnSendMini;

            frm.OnSendCheckUpdateVersion -= Frm_OnSendCheckUpdateVersion;
            frm.OnSendCheckUpdateVersion += Frm_OnSendCheckUpdateVersion;

            frm.Location = new Point(x, y);
            frm.ShowDialog();
            frm.BringToFront();
          }));
        }
        else
        {
          var frm = FrmOptionCloseApp.Instance;

          frm.StartPosition = FormStartPosition.Manual;
          var screen = Screen.PrimaryScreen.WorkingArea;
          int x = (screen.Width - frm.Width) / 2;
          int y = (screen.Height - frm.Height) / 2 - 0;
          if (y < 0) y = 0;

          frm.OnSendRestart -= Frm_OnSendRestart;
          frm.OnSendRestart += Frm_OnSendRestart;

          frm.OnSendClose -= Frm_OnSendCloseApp;
          frm.OnSendClose += Frm_OnSendCloseApp;

          frm.OnSendMini -= Frm_OnSendMini;
          frm.OnSendMini += Frm_OnSendMini;

          frm.OnSendCheckUpdateVersion -= Frm_OnSendCheckUpdateVersion;
          frm.OnSendCheckUpdateVersion += Frm_OnSendCheckUpdateVersion;

          frm.Location = new Point(x, y);
          frm.ShowDialog();
          frm.BringToFront();
        }
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }
    private void Frm_OnSendRestart(object? sender, EventArgs e)
    {
      Program.StartApp();
    }
    private void Frm_OnSendCloseApp(object? sender, EventArgs e)
    {
      try
      {
        Program.CloseApp();
      }
      catch (Exception)
      {
        Environment.Exit(0);
      }
    }
    private void Frm_OnSendMini(object? sender, EventArgs e)
    {
      FrmMain.Instance.MiniTab();
    }
    private void Frm_OnSendCheckUpdateVersion(object? sender, EventArgs e)
    {
      PopupApplyVersionNew popupApplyVersionNew = new PopupApplyVersionNew();
      popupApplyVersionNew.OnSendApply += PopupApplyVersionNew_OnSendApply;
      popupApplyVersionNew.ShowDialog();
    }

    private async void PopupApplyVersionNew_OnSendApply(object? sender, string e)
    {
      AppCore.Ins._appConfig.Version = e;
      AppCore.Ins._appConfig.UpdatedAt = DateTime.UtcNow;
      await AppCore.Ins._appConfigService.AddOrUpdateAsync(AppCore.Ins._appConfig);

      string app = Path.Combine(Application.StartupPath, "Versions\\ApplyVersion\\ApplyNewVersion.exe");
      Process.Start(app);

      Application.Exit();
    }
    #endregion

    private void FrmWaiting_Load(object? sender, EventArgs e)
    {
      ucPanelLogin1.Account = "Bosch";
      ucPanelLogin1.Password = "Hsf@2026";
      ucPanelLogin1.Account = "admin";
      ucPanelLogin1.Password = "admin";
      ucPanelLogin1.OnSendLogin += UcPanelLogin1_OnSendLogin;
    }

    private void LoadConfig()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadConfig();
        }));
        return;
      }

      var station = Environment.GetEnvironmentVariable("STATION");
      if (station == "1")
      {
        lbTitle.Text = "HỆ THỐNG CÂN XE TẢI";
      }
      else
      {
        lbTitle.Text = "HỆ THỐNG CÂN PHẾ PHẨM";
      }
    }

    private async void UcPanelLogin1_OnSendLogin(object? sender, EventArgs e)
    {
      FrmMain.Instance.ChangePage(EnumScreen.Operation);
      //AppCore.Ins._userCurrent = await AppCore.Ins._userService.CheckLogin(ucPanelLogin1.Account, ucPanelLogin1.Password);
      //if (AppCore.Ins._userCurrent != null)
      //{
      //  FrmOperation.Instance.LoadAccount(AppCore.Ins._userCurrent);
      //  FrmMain.Instance.ChangePage(EnumScreen.Operation);
      //}
      //else
      //{
      //  using var popupMsg = new PopupConfirm("Tài khoản hoặc mật khẩu sai. Vui lòng thử lại !",
      //    EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
      //  popupMsg.ShowDialog(this);
      //}

      //if (ucPanelLogin1.Account == "admin" && ucPanelLogin1.Password == "admin")
      //{
      //  FrmMain.Instance.ChangePage(EnumScreen.Operation);
      //  AppCore.Ins._employeeCurrent = AppCore.Ins._employees?.Where(x => x.Account == "admin").FirstOrDefault();
      //}
      //else
      //{
      //  using var popupMsg = new PopupConfirm("Tài khoản hoặc mật khẩu sai. Vui lòng thử lại !",
      //    EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
      //  popupMsg.ShowDialog(this);
      //}
    }

    private async void pictureBox1_Click(object sender, EventArgs e)
    {
      try
      {
        //var warehouse = new WarehouseUpsertRequest
        //{
        //  Name = "Kho Shopee",
        //};
        //var response = await AppCore.Ins.Warehouse(warehouse);



        //CategoryTareUpsertRequest categoryTareUpsertRequest = new CategoryTareUpsertRequest();
        //categoryTareUpsertRequest.Name = "Rổ";
        //categoryTareUpsertRequest.SerialCode = "B2032";
        //categoryTareUpsertRequest.WeightTare = (decimal)15.600;
        //categoryTareUpsertRequest.Description = "Rổ nhựa";
        //var response = await AppCore.Ins.CategoryTare(categoryTareUpsertRequest);

        //TypeGoodsUpsertRequest typeGoodsUpsertRequest = new TypeGoodsUpsertRequest();
        //typeGoodsUpsertRequest.SerialCode = "HH003";
        //typeGoodsUpsertRequest.Name = "Hóa chất";
        //typeGoodsUpsertRequest.Description = "Nguy hại";
        //var response = await AppCore.Ins.TypeGoods(typeGoodsUpsertRequest);

        //ClientUpsertRequest client = new ClientUpsertRequest();
        //client.Name = "Unilever";
        //client.Description = "UCC";
        //var response = await AppCore.Ins.Client(client);

        LicensePlateUpsertRequest client = new LicensePlateUpsertRequest();
        client.LicensePlateCode = "58C - 26548";
        client.Description = "UCC";
        var response = await AppCore.Ins.LicensePlate(client);
      }
      catch (Exception ex)
      {
        throw;
      }
    }



  }
}
