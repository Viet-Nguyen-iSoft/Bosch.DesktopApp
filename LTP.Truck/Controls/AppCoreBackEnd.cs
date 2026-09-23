using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Services;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static HelperManager.EnumData;
using static LTP.Truck.EnumData;
using static System.Windows.Forms.AxHost;

namespace LTP.Truck.Controls
{
  public partial class AppCore
  {
    #region Instance
    private static AppCore _ins = new AppCore();
    public static AppCore Ins
    {
      get
      {
        return _ins == null ? _ins = new AppCore() : _ins;
      }
    }
    #endregion

    #region Event
    //public delegate void SendDataWeight(MessageDataOutputWeight messageDataOutputWeight);
    //public event SendDataWeight? OnSendDataWeight;

    //public delegate void SendDataRfid(object? sender, MessageDataOutput e);
    //public event SendDataRfid? OnSendDataRfid;

    //public delegate void SendStatusDevice(MessageDataEvent messageDataEvent);
    //public event SendStatusDevice? OnSendStatusDevice;

    //public delegate void SendStatusConnectServer(EnumStatusConnectTcp enumStatusConnectTcp);
    //public event SendStatusConnectServer? OnSendStatusConnectServer;

    //public delegate void SendEndOfWeighingCycle();
    //public event SendEndOfWeighingCycle? OnSendEndOfWeighingCycle;

    //public delegate void SendEndOfWeighingDeliveryCycle();
    //public event SendEndOfWeighingDeliveryCycle? OnSendEndOfWeighingDeliveryCycle;

    //public event EventHandler<bool>? OnSendStatusConnectHID;
    //public event EventHandler<EnumStatusConnectTcp>? OnSendStatusConnectWeight;
    //public event EventHandler<List<Product>>? OnSendChangeTare;
    #endregion


    public readonly ClientService _clientService = new();
    public readonly TypeGoodsService _typeGoodsService = new();
    public readonly WarehouseService _warehouseService = new();
    public readonly RecordTruckService _recordTruckService = new();
    public readonly RecordWeightService _recordWeightService = new();
    public readonly CategoryTareService _categoryTareService = new();
    public readonly ProductGroupService _productGroupService = new();
    public readonly ProductService _productService = new();
    public readonly AppConfigService _appConfigService = new();
    public readonly StationService _stationService = new();
    public readonly ConnectionService _connectionService = new();
    public readonly UserService _userService = new();
    public readonly PermissionService _permissionService = new();
    public readonly LicensePlateService _licensePlateService = new();
    public readonly ApiJobsService _apiJobsService = new();
    private readonly ApiJobsBackgroundService _apiJobsBackgroundService = new();
    private readonly CancellationTokenSource _apiJobsCancellation = new();
    private Task? _apiJobsTask;

    public event Action<Station?>? OnChangeStation;

    public string _folderFileLog = Application.StartupPath + "Logs";
    public int _time = 7;
    public void Init()
    {
      try
      {
        LoadDataConfig().Wait();

        if (!Directory.Exists(_folderFileLog))
          Directory.CreateDirectory(_folderFileLog);

        _apiJobsTask ??= _apiJobsBackgroundService.RunAsync(
          _apiJobsCancellation.Token,
          ex => LogHelper.LogErrorToFileLog(ex, _folderFileLog));
        Application.ApplicationExit += (_, _) => _apiJobsCancellation.Cancel();

        StartShowUI();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Lỗi khởi động chương trình !");
        Environment.Exit(1);
      }
    }

    public AppConfig? _appConfig { get;set; }
    public Station? _station { get;set; }
    public User? _userCurrent { get;set; }
    public Connection? _connection { get;set; }


    public void ChangeStation(Station? station)
    {
      _station = station;
      OnChangeStation?.Invoke(station);
    }

    public async Task LoadDataConfig()
    {
      try
      {
        _appConfig = await _appConfigService.GetAppConfigAsync();
        _station = await _stationService.GetByCodeAsync(_appConfig?.StationId);
        _connection = await _connectionService.GetFirstDataConnection();
 
        //var employees = _employees.FirstOrDefault();
        //foreach (var item in _employees)
        //{
        //  var pass = HelperManager.EncoderHelper.Decrypt(item.Passwords);
        //}

      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public void PrinterLabel(string printer, DTOPrintLabel dTOPrintLabel)
    {
      try
      {
        int offsetY = 5;
        int offsetQR = 20;
        int startX = 2;
        int startY = 8;
        int rowIndex = 0;
        Brush brush = Brushes.Black;

        PrintDocument pd = new PrintDocument();
        pd.PrinterSettings.PrinterName = printer;

        pd.PrintPage += (sender, e) =>
        {
          e.Graphics.PageUnit = GraphicsUnit.Millimeter;

          // khung 80x50 mm
          e.Graphics.DrawRectangle(Pens.Black, 0, 0, 80, 50);


          string rawJson = Newtonsoft.Json.JsonConvert.SerializeObject(new
          {
            ProductGroup = dTOPrintLabel.ProductGroup,
            Product = dTOPrintLabel.Product,
            TypeTare = dTOPrintLabel.TypeTare,
            Net = dTOPrintLabel.Net,
            Tare = dTOPrintLabel.Tare,
            Datetime = dTOPrintLabel.Datetime,
            Operator = dTOPrintLabel.Operator,
          });
          string qrData = TextHelper.RemoveDiacritics(rawJson);
          using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
          {
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(
                qrData,
                QRCodeGenerator.ECCLevel.Q
            );

            using (QRCode qrCode = new QRCode(qrCodeData))
            {
              using (Bitmap qrBitmap = qrCode.GetGraphic(
                  pixelsPerModule: 2,
                  darkColor: Color.Black,
                  lightColor: Color.Transparent,
                  drawQuietZones: false
              ))
              {
                e.Graphics.DrawImage(
                    qrBitmap,
                    new Rectangle(startX, startY + 4 * offsetY, 19, 19)
                );
              }
            }
          }


          // Font family and styles to match the mockup
          Font fontTitlePallet = new Font("Arial", 10, FontStyle.Bold);
          Font fontTilte = new Font("Arial", 10, FontStyle.Regular);
          Font fontValue = new Font("Arial", 10, FontStyle.Bold);

          // Draw PALLET LẺ
          e.Graphics.DrawString("PHIẾU CÂN HÀNG", fontTitlePallet, brush, new PointF(startX, 2));

          e.Graphics.DrawString("Nhóm sản phẩm:", fontTilte, brush, new PointF(startX, startY));
          e.Graphics.DrawString(dTOPrintLabel.ProductGroup, fontValue, brush, new PointF(startX + 28, startY));

          var product = TextHelper.WrapText(dTOPrintLabel.Product ?? string.Empty, 23, 28);
          int i = 1;
          e.Graphics.DrawString("Tên sản phẩm:", fontTilte, brush, new PointF(startX, startY + offsetY * i));
          e.Graphics.DrawString(product[0], fontValue, brush, new PointF(startX + 28, startY + offsetY * i));
          i++;
          if (product?.Count()>=2)
          {
            e.Graphics.DrawString(product[1], fontValue, brush, new PointF(startX, startY + offsetY * i));
            i++;
          }

          e.Graphics.DrawString("Loại Tare :", fontTilte, brush, new PointF(startX, startY + offsetY * i));
          e.Graphics.DrawString(dTOPrintLabel.TypeTare, fontValue, brush, new PointF(startX + 20, startY + offsetY * i));

          i++;

          e.Graphics.DrawString("Net (Kg) :", fontTilte, brush, new PointF(startX + offsetQR, startY + offsetY * i));
          e.Graphics.DrawString(WeightFormatHelper.Format(dTOPrintLabel.Net, 3), fontValue, brush, new PointF(startX + offsetQR + 20, startY + offsetY * i));

          i++;

          e.Graphics.DrawString("Tare (Kg) :", fontTilte, brush, new PointF(startX + offsetQR, startY + offsetY * i));
          e.Graphics.DrawString(WeightFormatHelper.Format(dTOPrintLabel.Tare, 3), fontValue, brush, new PointF(startX + offsetQR + 20, startY + offsetY * i));

          i++;

          e.Graphics.DrawString("Thời gian:", fontTilte, brush, new PointF(startX + offsetQR, startY + offsetY * i));
          e.Graphics.DrawString(dTOPrintLabel.Datetime, fontValue, brush, new PointF(startX + offsetQR + 20, startY + offsetY * i));

          i++;

          e.Graphics.DrawString("OP:", fontTilte, brush, new PointF(startX + offsetQR, startY + offsetY * i));
          e.Graphics.DrawString("Admin", fontValue, brush, new PointF(startX + offsetQR + 20, startY + offsetY * i));
        };

        pd.Print();
      }
      catch (Exception ex)
      {

      }
    }

  }
}
