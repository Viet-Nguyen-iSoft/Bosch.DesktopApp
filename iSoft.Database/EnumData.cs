using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database
{
  public class EnumData
  {
    public enum EnumMaterialType
    {
      [Description("N/A")]
      None = 0,

      [Description("Vật tư")]
      Material = 1,

      [Description("Nguyên liệu")]
      RawMaterial = 2,

      [Description("Thành phẩm")]
      FinshGoods = 3,

      [Description("Bán thành phẩm")]
      SemiFinishedGoods = 4,

      [Description("Kho gia vị")]
      Spice = 5,

      [Description("Kho hóa chất")]
      Chemical = 6,

      [Description("Phế phẩm")]
      MRsDefect = 7,

      [Description("TS_CCDC")]
      TS_CCDC = 8,
    }
   

    public enum eTypeData
    {
      All,
      OnlyNotDelete,
      OnlyDelete,
    }

    public enum EnumTypeDataTruck
    {
      None,
      WeightedTime01, //KHông dùng
      DoneTime01,
      WeightedTime02, //KHông dùng
      DoneTime02,
      Delete,
    }

    public enum EnumWasteType
    {
      [Description("---")]
      None,
      [Description("Nguy hại")]
      Hazardous = 1,
      [Description("Không thể tái chế")]
      NonRecyclable = 2,
      [Description("Có thể tái chế")]
      Recyclable = 3,
    }
  }
}
