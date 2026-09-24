using HelperManager;
using iSoft.Database.Models;
using LTP.Truck.Controls;
using LTP.Truck.UserControls;
using static HelperManager.EnumData;
using static LTP.Truck.EnumData;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace LTP.Truck.Forms
{
  public partial class FrmOperation : Form
  {
    private readonly Dictionary<Button, (Color BackColor, Color ForeColor, Color MouseOverColor, Color MouseDownColor)> _menuButtonColors = new();
    private readonly Dictionary<Button, string> _mainMenuTexts = new();
    private bool _masterDataExpanded;
    private bool _menuCollapsed;

    private const int ExpandedMenuWidth = 250;
    private const int CollapsedMenuWidth = 80;
    private const int ExpandedButtonWidth = 242;
    private const int CollapsedButtonWidth = 72;

    public System.Timers.Timer _timerClock = new System.Timers.Timer();
    public FrmOperation()
    {
      InitializeComponent();
      AppTheme.Apply(this);
      InitializeMenuSelection();
      SetMasterDataExpanded(false);
      SetMenuCollapsed(true);
      this.Load += FrmOperation_Load;
      this.Shown += FrmOperation_Shown;
      AppCore.Ins.OnChangeStation += AppCore_OnChangeStation;
      AppCore.Ins.OnSendStatusWeight += Ins_OnSendStatusWeight;
      AppCore.Ins.OnSendStatusServer += Ins_OnSendStatusServer;
    }

    private void AppCore_OnChangeStation(Station? station)
    {
      LoadStation(station);
    }

    private void InitializeMenuSelection()
    {
      Button[] menuButtons =
      {
        btnHomeTruck, btnHomeGoods, btnReportTruck, btnReportGoods, btnSetting, btnMasterData,
        btnClient, btnTypeGoods, btnWarehouse, btnTare, btnGroupProduct, btnProduct
      };

      foreach (Button button in menuButtons)
      {
        _menuButtonColors.Add(button, (button.BackColor, button.ForeColor,
          button.FlatAppearance.MouseOverBackColor, button.FlatAppearance.MouseDownBackColor));
        button.Click += MenuButton_Click;
      }

      foreach (Button button in GetMainMenuButtons())
        _mainMenuTexts.Add(button, button.Text);
    }

    private Button[] GetMainMenuButtons()
    {
      return new[]
      {
        btnHomeTruck, btnHomeGoods, btnReportTruck, btnReportGoods, btnSetting, btnMasterData
      };
    }

    private void MenuButton_Click(object? sender, EventArgs e)
    {
      if (sender is Button button)
        CheckMenuButton(button);
    }

    private void CheckMenuButton(Button selectedButton)
    {
      Color choose = AppTheme.Primary;
      bool selectedChild = IsMasterDataChild(selectedButton);
      Button selectedMainButton = selectedChild ? btnMasterData : selectedButton;

      Button? selectedChildButton = selectedChild ? selectedButton
        : selectedButton == btnMasterData ? btnClient : null;

      foreach (var entry in _menuButtonColors)
      {
        Button button = entry.Key;
        var originalColors = entry.Value;
        bool mainSelected = button == selectedMainButton;
        bool childSelected = button == selectedChildButton;
        button.BackColor = mainSelected ? choose : originalColors.BackColor;
        button.ForeColor = mainSelected ? Color.White
          : childSelected ? AppTheme.Primary : originalColors.ForeColor;
        button.FlatAppearance.MouseOverBackColor = mainSelected ? choose : originalColors.MouseOverColor;
        button.FlatAppearance.MouseDownBackColor = mainSelected ? choose : originalColors.MouseDownColor;
      }
    }

    private bool IsMasterDataChild(Button button)
    {
      return button == btnClient || button == btnTypeGoods || button == btnWarehouse
        || button == btnTare || button == btnGroupProduct || button == btnProduct;
    }

    private void SetMasterDataExpanded(bool expanded)
    {
      _masterDataExpanded = expanded;
      ApplyMasterDataVisibility();
    }

    private void ApplyMasterDataVisibility()
    {
      flowLayoutPanel1.SuspendLayout();
      try
      {
        foreach (Button button in _menuButtonColors.Keys)
        {
          if (IsMasterDataChild(button))
            button.Visible = !_menuCollapsed && _masterDataExpanded && IsMasterDataChildAvailable(button);
        }
      }
      finally
      {
        flowLayoutPanel1.ResumeLayout(true);
      }
    }

    private bool IsMasterDataChildAvailable(Button button)
    {
      bool isTruckStation = Environment.GetEnvironmentVariable("STATION") == "1";
      return isTruckStation
        ? button == btnClient || button == btnWarehouse || button == btnTypeGoods
        : button == btnGroupProduct || button == btnProduct || button == btnTare;
    }

    private void SetMenuCollapsed(bool collapsed)
    {
      _menuCollapsed = collapsed;
      int menuWidth = collapsed ? CollapsedMenuWidth : ExpandedMenuWidth;
      int buttonWidth = collapsed ? CollapsedButtonWidth : ExpandedButtonWidth;

      tableLayoutPanel1.SuspendLayout();
      flowLayoutPanel1.SuspendLayout();
      try
      {
        tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
        tableLayoutPanel1.ColumnStyles[0].Width = menuWidth;
        panelMenu.Width = menuWidth;
        tableLayoutPanel7.Visible = !collapsed;

        foreach (Button button in GetMainMenuButtons())
        {
          button.Width = buttonWidth;
          button.Text = collapsed ? string.Empty : _mainMenuTexts[button];
        }

        ApplyMasterDataVisibility();
      }
      finally
      {
        flowLayoutPanel1.ResumeLayout(true);
        tableLayoutPanel1.ResumeLayout(true);
      }
    }

    private void EnsureMenuExpanded()
    {
      if (_menuCollapsed)
        SetMenuCollapsed(false);
    }

    private void btnMenu_Click(object? sender, EventArgs e)
    {
      SetMenuCollapsed(!_menuCollapsed);
    }

    #region Instance
    private static FrmOperation _Instance = null;
    public static FrmOperation Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmOperation();
        return _Instance;
      }
    }
    #endregion

    private void FrmOperation_Load(object? sender, EventArgs e)
    {
      lbVersion.Text = $"Version {AppCore.Ins._appConfig?.Version ?? string.Empty}";
      LoadStation(AppCore.Ins._station);
      //LoadAccount(AppCore.Ins._userCurrent);

      this.btnHomeTruck.Click += btnHomeTruck_Click;
      this.btnHomeGoods.Click += btnHomeGoods_Click;

      this.btnClient.Click += BtnClient_Click;
      this.btnTypeGoods.Click += BtnTypeGoods_Click;
      this.btnWarehouse.Click += BtnWarehouse_Click;
      this.btnTare.Click += BtnTare_Click;
      this.btnGroupProduct.Click += BtnGroupProduct_Click;
      this.btnProduct.Click += BtnProduct_Click;

      LoadConfig();
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
        btnHomeGoods.Visible = false;
        btnReportGoods.Visible = false;
        btnHomeTruck.Visible = true;
        btnReportTruck.Visible = true;

        this.btnHomeTruck.PerformClick();
      }
      else
      {
        btnHomeGoods.Visible = true;
        btnReportGoods.Visible = true;
        btnHomeTruck.Visible = false;
        btnReportTruck.Visible = false;

        this.btnHomeGoods.PerformClick();
      }

      ApplyMasterDataVisibility();
    }

    public void LoadAccount(User? user)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => LoadAccount(user)));
        return;
      }

      ucLogin.Account = user?.Username ?? "Login";
    }

    private void FrmOperation_Shown(object? sender, EventArgs e)
    {
      InitClock();
    }

    private void LoadStation(Station? station)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadStation(station);
        }));
        return;
      }

      if (station != null)
      {
        var stationKey = Environment.GetEnvironmentVariable("STATION");
        if (stationKey == "1")
        {
          lbTitle.Text = $"HỆ THỐNG CÂN XE TẢI - {station.Name}";
        }
        else
        {
          lbTitle.Text = $"HỆ THỐNG CÂN PHẾ PHẨM - {station.Name}";
        }
      }
      else
      {
        lbTitle.Text = $"- - - - -";
      }
    }

    private void Ins_OnSendStatusWeight(object? sender, iSoft.Communication.Interface.CommunicationStatusChangedEventArgs e)
    {
      EnumStatusConnectTcp enumStatusConnectTcp = e.IsConnected ? EnumStatusConnectTcp.Connect : EnumStatusConnectTcp.Disconnect;
      SetStatusConnect(ucStatusConnectWeight, enumStatusConnectTcp, "Cân");
    }
    private void Ins_OnSendStatusServer(object? sender, EnumStatusConnectTcp e)
    {
      SetStatusConnect(ucStatusConnectServer, e, "Server");
    }

    private void SetStatusConnect(UcStatusConnect ucStatus, EnumStatusConnectTcp status, string name)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetStatusConnect(ucStatus, status, name);
        }));
        return;
      }

      switch (status)
      {
        case EnumStatusConnectTcp.Connect:
          ucStatus.Title = $"{name} - Kết nối";
          ucStatus.StatusColor = Color.DarkGreen;
          break;
        case EnumStatusConnectTcp.Disconnect:
          ucStatus.Title = $"{name} - Mất kết nối";
          ucStatus.StatusColor = Color.Red;
          break;
        case EnumStatusConnectTcp.Connecting:
          ucStatus.Title = $"{name} - Đang kết nối";
          ucStatus.StatusColor = Color.DarkOrange;
          break;
        default:
          ucStatus.Title = $"{name} - Không xác định";
          ucStatus.StatusColor = Color.Gray;
          break;
      }

    }

    private async void BtnProduct_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      EnsureMenuExpanded();
      await ChangePage(EnumScreen.MD_Product);
    }

    private async void BtnGroupProduct_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      EnsureMenuExpanded();
      await ChangePage(EnumScreen.MD_GroupProduct);
    }

    private async void BtnTare_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      EnsureMenuExpanded();
      await ChangePage(EnumScreen.MD_Tare);
    }

    private async void BtnWarehouse_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      EnsureMenuExpanded();
      await ChangePage(EnumScreen.MD_Warehouse);
    }

    private async void BtnTypeGoods_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      EnsureMenuExpanded();
      await ChangePage(EnumScreen.MD_TypeGoods);
    }
    private async void BtnClient_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      EnsureMenuExpanded();
      await ChangePage(EnumScreen.MD_Client);
    }
    private async void btnMasterData_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      if (_menuCollapsed)
      {
        SetMenuCollapsed(false);
        SetMasterDataExpanded(true);
      }
      else
      {
        SetMasterDataExpanded(!_masterDataExpanded);
      }
      await ChangePage(EnumScreen.MD_Client);
    }


    private async void btnHomeTruck_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await ChangePage(EnumScreen.HomeTruck);
    }
    private async void btnHomeGoods_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await ChangePage(EnumScreen.HomeGoods);
    }
    private async void btnSetting_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await ChangePage(EnumScreen.Setting);
    }


    #region ChangePage
    public async Task ChangePage(EnumScreen appModulSupport, bool actionBack = false)
    {
      try
      {
        switch (appModulSupport)
        {
          case EnumScreen.HomeTruck:
            OpenChildForm(appModulSupport, FrmHomeTruck.Instance);
            break;
          case EnumScreen.HomeGoods:
            OpenChildForm(appModulSupport, FrmHomeGoods.Instance);
            break;
          case EnumScreen.Setting:
            OpenChildForm(appModulSupport, FrmSetting.Instance);
            break;
          case EnumScreen.ReportTruck:
            OpenChildForm(appModulSupport, FrmReportTruck.Instance);
            break;
          case EnumScreen.ReportGoods:
            OpenChildForm(appModulSupport, FrmReportGoods.Instance);
            break;
          case EnumScreen.MD_Client:
            OpenChildForm(appModulSupport, FrmMasterData.Instance);
            await FrmMasterData.Instance.LoadData(EnumTypeMasterData.Client);
            break;
          case EnumScreen.MD_TypeGoods:
            OpenChildForm(appModulSupport, FrmMasterData.Instance);
            await FrmMasterData.Instance.LoadData(EnumTypeMasterData.TypeGoods);
            break;
          case EnumScreen.MD_Warehouse:
            OpenChildForm(appModulSupport, FrmMasterData.Instance);
            await FrmMasterData.Instance.LoadData(EnumTypeMasterData.Warehouse);
            break;
          case EnumScreen.MD_Tare:
            OpenChildForm(appModulSupport, FrmMasterData.Instance);
            await FrmMasterData.Instance.LoadData(EnumTypeMasterData.Tare);
            break;
          case EnumScreen.MD_GroupProduct:
            OpenChildForm(appModulSupport, FrmMasterData.Instance);
            await FrmMasterData.Instance.LoadData(EnumTypeMasterData.GroupProduct);
            break;
          case EnumScreen.MD_Product:
            OpenChildForm(appModulSupport, FrmMasterData.Instance);
            await FrmMasterData.Instance.LoadData(EnumTypeMasterData.Product);
            break;
        }

        ShowPagePath(appModulSupport);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private void ShowPagePath(EnumScreen screen)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => ShowPagePath(screen)));
        return;
      }

      lbTitlePage.Text = screen switch
      {
        EnumScreen.HomeTruck => "Trang chính > Cân xe tải",
        EnumScreen.HomeGoods => "Trang chính > Cân hàng",
        EnumScreen.ReportTruck => "Trang chính > Báo cáo xe tải",
        EnumScreen.ReportGoods => "Trang chính > Báo cáo cân hàng",
        EnumScreen.Setting => "Trang chính > Cài đặt",
        EnumScreen.MD_Client => "Trang chính > Dữ liệu gốc > Khách hàng",
        EnumScreen.MD_TypeGoods => "Trang chính > Dữ liệu gốc > Loại hàng",
        EnumScreen.MD_Warehouse => "Trang chính > Dữ liệu gốc > Kho hàng",
        EnumScreen.MD_Tare => "Trang chính > Dữ liệu gốc > Nhóm Tare",
        EnumScreen.MD_GroupProduct => "Trang chính > Dữ liệu gốc > Nhóm chất thải",
        EnumScreen.MD_Product => "Trang chính > Dữ liệu gốc > Chất thải",
        _ => "Trang chính"
      };
    }

    private Form CurrentForm;
    public void OpenChildForm(EnumScreen modulSupport, Form childForm)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          OpenChildForm(modulSupport, childForm);
        }));
        return;
      }

      bool Is_same_form = false;
      if (this.panelMain.Tag != null)
      {
        if (this.panelMain.Tag is Tuple<EnumScreen, Form>)
        {
          Tuple<EnumScreen, Form> TagAsForm = (Tuple<EnumScreen, Form>)(this.panelMain.Tag);
          if (TagAsForm.Item1 == modulSupport)
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
        this.panelMain.Tag = Tuple.Create(modulSupport, childForm);
        CurrentForm = childForm;
        childForm.TopLevel = false;
        childForm.FormBorderStyle = FormBorderStyle.None;
        childForm.Dock = DockStyle.Fill;
        childForm.BringToFront();
        AppTheme.Apply(childForm);
        this.panelMain.Controls.Add(childForm);
        childForm.Show();
      }
    }
    #endregion


    #region Clock
    public void InitClock()
    {
      _timerClock.Interval = 1000;
      _timerClock.Elapsed += _timerClock_Elapsed;
      _timerClock.Start();
    }

    private void _timerClock_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          _timerClock_Elapsed(sender, e);
        }));
        return;
      }

      try
      {
        _timerClock.Stop();
        DateTime dt = DateTime.Now;
        lbTime.Text = dt.ToString("dd/MM/yyyy HH:mm:ss");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
      finally
      {
        _timerClock.Start();
      }
    }
    #endregion

    private void btnLogout_Click(object sender, EventArgs e)
    {
      AppCore.Ins._userCurrent = null;
      LoadAccount(null);
      FrmMain.Instance.ChangePage(EnumScreen.Waiting);
    }

    private async void btnReportTruck_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await ChangePage(EnumScreen.ReportTruck);
    }

    private async void btnReportGoods_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await ChangePage(EnumScreen.ReportGoods);
    }
  }
}
