using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Communication
{
  public class EnumCommunication
  {
    public enum eCommunicationType
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

    public enum eDevice
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

    public enum eDataType
    {
      Default = PlainText,
      PlainText = 0,
      Json,
      ByteArray,
      Number,
    }

    public enum EnumModeCommunication
    {
      [Description("None")]
      None,

      [Description("Continuous")]
      Continuous,

      [Description("SCOD")]
      SCOD,

      [Description("Digi")]
      Digi,

      [Description("SICS")]
      SICS,

      [Description("OHAUS")]
      OHAUS,
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

    public enum UnitOfWeight
    {
      None,
      Pounds = 0, // lp
      Kilograms = 1, // kg
      Grams = 2, // g
      Ounces // oz
    }

    public enum WeighingUnit
    {
      Miligram,
      Centigram,
      Decigram,
      Grams,
      Kilograms,
      MetricTon,
      Pounds,
      Ounces
    }


    public enum EnumValueWeightType
    {
      Tare,
      Net, 
      Gross,
      All,
    }


    public enum DecimalPointLocation
    {
      XXXXX00 = 0,
      XXXXX0 = 1,
      XXXXXX = 2,
      XXXXX_X = 3,
      XXXX_XX = 4,
      XXX_XXX = 5,
      XX_XXXX = 6,
      X_XXXXX = 7
    }

    public enum BuildCode
    {
      X1 = 1,
      X2 = 2,
      X5 = 3,
      NotUsed
    }

    public enum TypeOfWeight
    {
      Gross = 0,
      Net = 1
    }

    public enum Sign
    {
      Positive = 0,
      Negative = 1,
    }

    public enum OutOfRange
    {
      False = 0,
      True = 1,
    }


    public enum ZeroNotCapturedAfterPowerUp
    {
      No = 0,
      Yes = 1
    }

    public enum WeightDescription
    {
      SelectedByStatusByteB = 0,
      Grams = 1, // <= 

      NotUse2 = 2,

      Ounces = 3, // <= 

      NotUse4 = 4,
      NotUse5 = 5,
      NotUse6 = 6,
      NoUnits = 7,
    }


    public enum PrintRequest
    {
      False = 0,
      True = 1
    }

    public enum ExpandData
    {
      Normal = 0,
      x10 = 1
    }



  }
}
