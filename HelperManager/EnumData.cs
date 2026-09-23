using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperManager
{
  public class EnumData
  {
    public enum EnumCheckData
    {
      None,
      Check,
      UnCheck,
    }

    public enum EnumStatusConnectTcp
    {
      None,
      Connect,
      Disconnect,
      Connecting,
    }

    public enum EnumGroup
    {
      None,
      Warehouse,
      QC,
      Other, //Các phòng còn lại
      SoChe,
      CheBien,
      Packing,
    }

    public enum EnumTypePO
    {
      None,
      POandOther,
      TestMachine,
      RequestOther
    }

    public enum EnumInternalExternalStatus
    {
      None = 0,
      Internal = 1,
      External = 2
    }

    public enum EnumExportImport
    {
      [Description("N/A")]
      None = 0,

      [Description("Nhập hàng")]
      Import = 1,

      [Description("Xuất hàng")]
      Export = 2,

      [Description("Giao nhận")]
      ImportExport = 3,
    }

    public enum EnumProductionOrderType
    {
      None = 0,
      LenhSanXuat = 1,
      LenhThuNghiem = 2,
      LenhDuPhong = 3,
      LenhLayMau = 4,

      //TestMachine,
      //RequestOther,
    }

    public enum EnumProductionOrderCategory
    {
      None = 0,
      MachineTesting = 1,
      NewProductTesting,
      SampleProduction,
    }

    public enum EnumProcessing
    {
      None,
      Processing,
      Unprocessed,
    }

    public enum ActiveWeighingStatus
    {
      Stable = 0,
      Motion = 1,
      Default = 2,
      Distable = 3,
      Overload = 4,
      Underload = 5,
      CommandUnderstoodButNotExecutableAtPresent = 6,

    }

    public enum EnumCommunicationType
    {
      None,

      [Description("USB")]
      USBHID,

      [Description("Serial")]
      SerialPort,

      [Description("Tcp Listener")]
      TcpListener,

      [Description("Tcp Client")]
      TcpClient,
    }

    public enum EnumDevice
    {
      [Description("None")]
      None,
      [Description("Weight")]
      Weight,

      [Description("Rfid")]
      Rfid,

      [Description("Server")]
      Server,


      [Description("Weight Tcp")]
      WeightTcp,

      [Description("Hid Tcp")]
      HidTcp,
    }

    public enum eValueWeightType
    {
      Tare,
      Net,
      Gross,
      All,
    }

    public enum UnitOfWeight
    {
      None,
      Pounds = 0, // lp
      Kilograms = 1, // kg
      Grams = 2, // g
      Ounces // oz
    }

    public enum EnumGroupData
    {
      None,
      Level1,
      Level2,
      Detail
    }

    public enum EnumTypeAPI
    {
      [Description("None")]
      None,
      Plate,

    }

    public enum EnumStatusAPI
    {
      [Description("Created")]
      Created,
      [Description("Success")]
      Success,
      [Description("Fail")]
      Fail,
      
    }
  }
}
