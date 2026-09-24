using Common;
using Common.Settings;
using HelperManager;
using iSoft.Communication.JsonPayload;
using iSoft.Communication.Mode;
using iSoft.Database.Models;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using Newtonsoft.Json;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.Threading.Tasks;
using TestConnectPrinter;
using static Common.EnumData;
using static HelperManager.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmSetting : Form
  {
    private bool _permitCheck { get; set; }
    public FrmSetting()
    {
      InitializeComponent();
      CustomUI();
      flowCommWeight.AutoScroll = true;
      flowCommWeight.FlowDirection = FlowDirection.LeftToRight;
      flowCommWeight.WrapContents = false;
      this.Load += FrmSetting_Load;
      btnSavePrint.Click += btnSavePrint_Click;
      btnSaveStation.Click += btnSaveStation_Click;
      btnAddCommWeight.Click += btnAddCommWeight_Click;
      txtPortServer.KeyPress += NonNegativeInteger_KeyPress;
      txtTimeoutServer.KeyPress += NonNegativeInteger_KeyPress;
      txtValueWeightPermit.KeyPress += NonNegativeDecimal_KeyPress;
      txtPortServer._TextChanged += NonNegativeInteger_TextChanged;
      txtTimeoutServer._TextChanged += NonNegativeInteger_TextChanged;
      txtValueWeightPermit._TextChanged += NonNegativeDecimal_TextChanged;

    }

    private static void NonNegativeInteger_KeyPress(object? sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && (e.KeyChar < '0' || e.KeyChar > '9'))
        e.Handled = true;
    }

    private static void NonNegativeInteger_TextChanged(object? sender, EventArgs e)
    {
      if (sender is not Common.Custom.RJTextBox textBox)
        return;

      string value = textBox.Texts;
      string sanitizedValue = new(value.Where(character =>
        character >= '0' && character <= '9').ToArray());

      // TextChanged còn bảo vệ trường hợp paste nội dung không hợp lệ.
      if (!string.Equals(value, sanitizedValue, StringComparison.Ordinal))
        textBox.Texts = sanitizedValue;
    }

    private static void NonNegativeDecimal_KeyPress(object? sender, KeyPressEventArgs e)
    {
      if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
        return;

      if ((e.KeyChar == '.' || e.KeyChar == ',') &&
          sender is Common.Custom.RJTextBox textBox &&
          !textBox.Texts.Contains('.') && !textBox.Texts.Contains(','))
        return;

      e.Handled = true;
    }

    private static void NonNegativeDecimal_TextChanged(object? sender, EventArgs e)
    {
      if (sender is not Common.Custom.RJTextBox textBox)
        return;

      string value = textBox.Texts;
      bool hasDecimalSeparator = false;
      string sanitizedValue = new(value.Where(character =>
      {
        if (char.IsDigit(character))
          return true;

        if ((character == '.' || character == ',') && !hasDecimalSeparator)
        {
          hasDecimalSeparator = true;
          return true;
        }

        return false;
      }).ToArray());

      // Chặn cả nội dung không hợp lệ được dán từ clipboard.
      if (!string.Equals(value, sanitizedValue, StringComparison.Ordinal))
        textBox.Texts = sanitizedValue;
    }

    #region Instance
    private static FrmSetting _Instance = null;
    public static FrmSetting Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmSetting();
        return _Instance;
      }
    }
    #endregion

    private void CustomUI()
    {
      ElipseControl elipseControl01 = new ElipseControl();
      elipseControl01.CornerRadius = 20;
      elipseControl01.TargetControl = this;

      ElipseControl elipseControl02 = new ElipseControl();
      elipseControl02.CornerRadius = 20;
      elipseControl02.TargetControl = tableLayoutPanel3;

      ElipseControl elipseControl03 = new ElipseControl();
      elipseControl03.CornerRadius = 20;
      elipseControl03.TargetControl = tableLayoutPanel4;

      ElipseControl elipseControl04 = new ElipseControl();
      elipseControl04.CornerRadius = 20;
      elipseControl04.TargetControl = tableLayoutPanel7;

      ElipseControl elipseControl05 = new ElipseControl();
      elipseControl05.CornerRadius = 20;
      elipseControl05.TargetControl = tableLayoutPanel11;

      ElipseControl elipseControl06 = new ElipseControl();
      elipseControl06.CornerRadius = 20;
      elipseControl06.TargetControl = tableLayoutPanel16;

      ElipseControl elipseControl07 = new ElipseControl();
      elipseControl07.CornerRadius = 20;
      elipseControl07.TargetControl = tableLayoutPanel17;
    }

    private async void FrmSetting_Load(object? sender, EventArgs e)
    {
      LoadInstalledPrinters();
      LoadShowInformationServer(AppCore.Ins._appConfig);
      LoadReportInformation(AppCore.Ins._appConfig);
      await LoadStationsAsync();
      await LoadWeightConnectionsAsync();

      _permitCheck = AppCore.Ins._appConfig?.PermitCheckWeight ?? false;
      SetStatusPermitCheckWeight(_permitCheck);
      txtValueWeightPermit.Texts = (AppCore.Ins._appConfig?.ValueCheckWeight ?? 0)
        .ToString(CultureInfo.CurrentCulture);

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
        tableLayoutPanel16.Visible = true;
      }
      else
      {
        tableLayoutPanel16.Visible = false;
      }
    }

    private async Task LoadStationsAsync()
    {
      try
      {
        var stations = (await AppCore.Ins._stationService.GetAllAsync())
          .OrderBy(station => station.Name)
          .ThenBy(station => station.Code)
          .ToList();

        LoadShowStation(stations);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private void LoadShowStation(List<Station> stations)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadShowStation(stations);
        }));
        return;
      }

      cbbStations.BeginUpdate();
      try
      {
        cbbStations.DataSource = null;
        cbbStations.DisplayMember = nameof(Station.Name);
        cbbStations.ValueMember = nameof(Station.Id);
        cbbStations.DataSource = stations;

        Guid? selectedStationId = AppCore.Ins._appConfig?.StationId ??
          AppCore.Ins._station?.Id;
        Station? selectedStation = selectedStationId.HasValue
          ? stations.FirstOrDefault(station => station.Id == selectedStationId.Value)
          : null;

        if (selectedStation != null)
          cbbStations.SelectedItem = selectedStation;
        else
          cbbStations.SelectedIndex = stations.Count > 0 ? 0 : -1;
      }
      finally
      {
        cbbStations.EndUpdate();
      }
    }

    private void LoadShowInformationServer(AppConfig? appConfig)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadShowInformationServer(appConfig);
        }));
        return;
      }

      txtIpServer.Texts = appConfig?.IpServer ?? string.Empty;
      txtPortServer.Texts = appConfig?.PortServer?.ToString() ?? string.Empty;
      txtTimeoutServer.Texts = appConfig?.TimeoutConnectServer?.ToString() ?? string.Empty;
    }

    private void LoadReportInformation(AppConfig? appConfig)
    {
      if (InvokeRequired)
      {
        Invoke(new Action(() => LoadReportInformation(appConfig)));
        return;
      }

      txtCompany.Texts = appConfig?.Company ?? string.Empty;
      txtAddress.Texts = appConfig?.Address ?? string.Empty;
      txtPhone.Texts = appConfig?.Phone ?? string.Empty;
    }

    private async void btnSaveStation_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      if (cbbStations.SelectedItem is not Station selectedStation)
      {
        using var popupWarning = new PopupConfirm(
          "Vui lòng chọn trạm !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupWarning.ShowDialog(this);
        return;
      }

      var appConfig = AppCore.Ins._appConfig;
      if (appConfig == null)
      {
        using var popupWarning = new PopupConfirm(
          "Không tìm thấy cấu hình ứng dụng !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupWarning.ShowDialog(this);
        return;
      }

      try
      {
        appConfig.StationId = selectedStation.Id;
        appConfig.UpdatedAt = DateTime.UtcNow;
        AppCore.Ins._appConfig = await AppCore.Ins._appConfigService
          .AddOrUpdateAsync(appConfig);
        AppCore.Ins.ChangeStation(selectedStation);

        using var popupSuccess = new PopupConfirm(
          "Đã lưu trạm.",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Information);
        popupSuccess.ShowDialog(this);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popupError = new PopupConfirm(
          "Không thể lưu trạm. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupError.ShowDialog(this);
      }
    }

    private void LoadInstalledPrinters()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadInstalledPrinters();
        }));
        return;
      }

      var printerNames = PrinterSettings.InstalledPrinters
        .Cast<string>()
        .OrderBy(name => name)
        .ToList();

      cbbPrint.BeginUpdate();
      try
      {
        cbbPrint.Items.Clear();
        cbbPrint.Items.AddRange(printerNames.Cast<object>().ToArray());

        var savedPrinter = AppCore.Ins._appConfig?.NamePrint;
        if (!string.IsNullOrWhiteSpace(savedPrinter))
          cbbPrint.SelectedItem = printerNames.FirstOrDefault(name =>
            string.Equals(name, savedPrinter, StringComparison.OrdinalIgnoreCase));

        if (cbbPrint.SelectedIndex < 0 && cbbPrint.Items.Count > 0)
          cbbPrint.SelectedIndex = 0;
      }
      finally
      {
        cbbPrint.EndUpdate();
      }
    }

    private async void btnSavePrint_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      if (cbbPrint.SelectedItem is not string printerName ||
        string.IsNullOrWhiteSpace(printerName))
      {
        PopupConfirm popupConfirm = new PopupConfirm("Vui lòng chọn máy in !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
        return;
      }

      var appConfig = AppCore.Ins._appConfig;
      if (appConfig == null)
      {
        PopupConfirm popupConfirm = new PopupConfirm("Không tìm thấy cấu hình ứng dụng !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
        return;
      }

      try
      {
        appConfig.NamePrint = printerName;
        appConfig.UpdatedAt = DateTime.UtcNow;
        AppCore.Ins._appConfig = await AppCore.Ins._appConfigService
          .AddOrUpdateAsync(appConfig);

        PopupConfirm popupConfirm = new PopupConfirm("Đã lưu máy in.", EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
        popupConfirm.ShowDialog();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        PopupConfirm popupConfirm = new PopupConfirm("Không thể lưu máy in. Vui lòng thử lại !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
      }
    }

    private async Task LoadWeightConnectionsAsync()
    {
      try
      {
        var connections = await AppCore.Ins._connectionService.GetAllAsync();
        var weightConnections = connections
          .Where(connection => connection.EnumDevice == EnumDevice.Weight)
          .OrderBy(connection => connection.Name)
          .ThenBy(connection => connection.Id)
          .ToList();

        flowCommWeight.SuspendLayout();
        try
        {
          flowCommWeight.Controls.Clear();

          foreach (var connection in weightConnections)
          {
            var item = new UcComm
            {
              Connection = connection,
              CommName = EnumHelper.GetDescription(connection.EnumCommunicationType),
              Information = GetConnectionInformation(connection),
              AutoConnect = GetConnectionAutoConnect(connection),
              Tag = connection,
              Margin = new Padding(3),
              Width = Math.Max(100, flowCommWeight.ClientSize.Width / 2 - 10),
              Height = 225
            };
            item.OnSendDataDetail += Item_OnSendDataDetail;
            item.OnSendDelete += Item_OnSendDelete;
            flowCommWeight.Controls.Add(item);
          }
        }
        finally
        {
          flowCommWeight.ResumeLayout();
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void Item_OnSendDelete(Connection? obj)
    {
      if (obj == null)
        return;

      try
      {
        obj.DeletedFlag = true;
        obj.UpdatedAt = DateTime.UtcNow;
        await AppCore.Ins._connectionService.AddOrUpdateAsync(obj);
        AppCore.Ins.DisconnectWeight();

        await LoadWeightConnectionsAsync();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private void Item_OnSendDataDetail(Connection? obj)
    {
      if (obj?.EnumCommunicationType == EnumCommunicationType.TcpClient)
      {
        PopupSettingTcpClient popupSettingTcpClient = new PopupSettingTcpClient(obj);
        popupSettingTcpClient.OnSendConfirm += Connection_OnSendConfirm;
        popupSettingTcpClient.ShowDialog();
      }
      else if (obj?.EnumCommunicationType == EnumCommunicationType.SerialPort)
      {
        PopupSettingSerial popupSettingSerial = new PopupSettingSerial(obj);
        popupSettingSerial.OnSendConfirm += Connection_OnSendConfirm;
        popupSettingSerial.ShowDialog();
      }
    }

    private static string GetConnectionInformation(Connection connection)
    {
      if (string.IsNullOrWhiteSpace(connection.JsonStrConfig))
      {
        return string.Empty;
      }

      try
      {
        if (connection.EnumCommunicationType == EnumCommunicationType.TcpClient)
        {
          var config = JsonConvert.DeserializeObject<JsonConfigTcpClient>(
            connection.JsonStrConfig);
          return config == null
            ? string.Empty
            : $"IP: {config.Host} - Port: {config.Port}";
        }

        if (connection.EnumCommunicationType == EnumCommunicationType.SerialPort)
        {
          var config = JsonConvert.DeserializeObject<JsonConfigTcpSerial>(
            connection.JsonStrConfig);
          return config == null
            ? string.Empty
            : $"COM: {config.COM}";
        }

        return string.Empty;
      }
      catch (JsonException ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        return "Cấu hình kết nối không hợp lệ";
      }
    }

    private static bool GetConnectionAutoConnect(Connection connection)
    {
      if (string.IsNullOrWhiteSpace(connection.JsonStrConfig))
        return false;

      try
      {
        return connection.EnumCommunicationType switch
        {
          EnumCommunicationType.TcpClient =>
            JsonConvert.DeserializeObject<JsonConfigTcpClient>(connection.JsonStrConfig)
              ?.AutoConnect ?? false,
          EnumCommunicationType.SerialPort =>
            JsonConvert.DeserializeObject<JsonConfigTcpSerial>(connection.JsonStrConfig)
              ?.AutoConnect ?? false,
          _ => false
        };
      }
      catch (JsonException ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        return false;
      }
    }


    private async void btnAddCommWeight_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      if (await HasWeightConnectionAsync())
      {
        ShowSingleWeightConnectionWarning();
        return;
      }

      PopupChooseComm popupChooseComm = new PopupChooseComm();
      popupChooseComm.OnSendConfirm += PopupChooseComm_OnSendConfirm;
      popupChooseComm.ShowDialog();
    }

    private void PopupChooseComm_OnSendConfirm(object? sender, Common.EnumData.EnumCommunication e)
    {
      if (e == Common.EnumData.EnumCommunication.TcpClient)
      {
        PopupSettingTcpClient tcpClient = new PopupSettingTcpClient();
        tcpClient.OnSendConfirm += Connection_OnSendConfirm;
        tcpClient.ShowDialog();
      }
      else if (e == Common.EnumData.EnumCommunication.RS232)
      {
        PopupSettingSerial serial = new PopupSettingSerial();
        serial.OnSendConfirm += Connection_OnSendConfirm;
        serial.ShowDialog();
      }
    }

    private async void Connection_OnSendConfirm(object? sender, Connection e)
    {
      if (e.Id == Guid.Empty && await HasWeightConnectionAsync())
      {
        ShowSingleWeightConnectionWarning();
        return;
      }

      e.StationId = AppCore.Ins._station?.Id;

      var savedConnection = await AppCore.Ins._connectionService.AddOrUpdateAsync(e);
      AppCore.Ins.ConnectWeight(savedConnection);
      await LoadWeightConnectionsAsync();
    }

    private async Task<bool> HasWeightConnectionAsync()
    {
      var connections = await AppCore.Ins._connectionService.GetAllAsync();
      return connections.Any(connection => connection.EnumDevice == EnumDevice.Weight);
    }

    private void ShowSingleWeightConnectionWarning()
    {
      using var popupMsg = new PopupConfirm(
        "Chỉ hỗ trợ 1 kết nối cân. Vui lòng xóa kết nối cũ rồi thêm mới hoặc cập nhật kết nối hiện tại.",
        EnumTypeMsg.MessageManualClose,
        EnumImageMsg.Warning);
      popupMsg.ShowDialog(this);
    }

    private async void btnConfirm_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        AppCore.Ins._appConfig.IpServer = txtIpServer.Texts.Trim();
        AppCore.Ins._appConfig.PortServer = int.Parse(txtPortServer.Texts.Trim());
        AppCore.Ins._appConfig.TimeoutConnectServer = int.Parse(txtTimeoutServer.Texts.Trim());
        AppCore.Ins._appConfig.UpdatedAt = DateTime.UtcNow;
        await AppCore.Ins._appConfigService.AddOrUpdateAsync(AppCore.Ins._appConfig);

        using var popupMsg = new PopupConfirm("Cập nhật thành công.",
            EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
        popupMsg.ShowDialog();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popupMsg = new PopupConfirm("Lưu thất bại. Kiểm tra lại !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsg.ShowDialog();
      }
    }


    private void picPermitCheckWeight_Click(object sender, EventArgs e)
    {
      _permitCheck = !_permitCheck;
      SetStatusPermitCheckWeight(_permitCheck);
    }

    private void SetStatusPermitCheckWeight(bool check)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetStatusPermitCheckWeight(check);
        }));
        return;
      }

      picPermitCheckWeight.Image = check ? Properties.Resources.icon_toggle_on : Properties.Resources.icon_toggle_off;
    }

    private async void btnSavePermitCheckWeight_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      var appConfig = AppCore.Ins._appConfig;
      if (appConfig == null)
      {
        using var popupWarning = new PopupConfirm(
          "Không tìm thấy cấu hình ứng dụng !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupWarning.ShowDialog(this);
        return;
      }

      string input = txtValueWeightPermit.Texts.Trim().Replace(',', '.');
      if (!double.TryParse(input, NumberStyles.AllowDecimalPoint,
          CultureInfo.InvariantCulture, out double permittedWeight) ||
          permittedWeight < 0)
      {
        using var popupWarning = new PopupConfirm(
          "Khối lượng sai số cho phép phải là số lớn hơn hoặc bằng 0 !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupWarning.ShowDialog(this);
        return;
      }

      try
      {
        appConfig.PermitCheckWeight = _permitCheck;
        appConfig.ValueCheckWeight = permittedWeight;
        appConfig.UpdatedAt = DateTime.UtcNow;
        AppCore.Ins._appConfig = await AppCore.Ins._appConfigService
          .AddOrUpdateAsync(appConfig);

        txtValueWeightPermit.Texts = permittedWeight
          .ToString(CultureInfo.CurrentCulture);

        using var popupSuccess = new PopupConfirm(
          "Đã lưu thông tin cài đặt thành công.",
          EnumTypeMsg.MessageAutoClose,
          EnumImageMsg.Information);
        popupSuccess.ShowDialog(this);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popupError = new PopupConfirm(
          "Không thể lưu thông tin cài đặt. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupError.ShowDialog(this);
      }

    }

    private async void btnInforReport_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      var appConfig = AppCore.Ins._appConfig;
      if (appConfig == null)
      {
        using var popupWarning = new PopupConfirm(
          "Không tìm thấy cấu hình ứng dụng !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupWarning.ShowDialog(this);
        return;
      }

      try
      {
        appConfig.Company = txtCompany.Texts.Trim();
        appConfig.Address = txtAddress.Texts.Trim();
        appConfig.Phone = txtPhone.Texts.Trim();
        appConfig.UpdatedAt = DateTime.UtcNow;

        AppCore.Ins._appConfig = await AppCore.Ins._appConfigService
          .AddOrUpdateAsync(appConfig);

        LoadReportInformation(AppCore.Ins._appConfig);

        using var popupSuccess = new PopupConfirm(
          "Đã lưu thông tin báo cáo thành công.",
          EnumTypeMsg.MessageAutoClose,
          EnumImageMsg.Information);
        popupSuccess.ShowDialog(this);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popupError = new PopupConfirm(
          "Không thể lưu thông tin báo cáo. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupError.ShowDialog(this);
      }

    }
  }
}
