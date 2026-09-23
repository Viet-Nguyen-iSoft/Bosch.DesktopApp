using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTP.Truck
{
  public class EnumData
  {
    public enum EnumScreen
    {
      Waiting,
      Operation,
      Setting,
      MasterData,

      HomeTruck,
      HomeGoods,
      ReportTruck,
      ReportGoods,

      MD_Client,
      MD_TypeGoods,
      MD_Warehouse,
      MD_Tare,
      MD_GroupProduct,
      MD_Product,

      
      
      Employee,

      OpChooseModeFunction,
      OpTypePO,
      OpShowListPO,
      OpChooseExportImport,
      OpTypeMR,
      OpDetailMRs,
      OpRMs_BTP_Defect,

      OpTypeForPoOther,

      OperationPrint,

      Menu,
      LogInSussess,


      
      SettingDevice,
      SettingServer,
      SettingPrinter,
      SettingMachine,


      Department,
      ProductionOrder,
      Material,
      Product,


      LoadingPrintting,

      CheckUpdateVer,

      AreaInternalOrExternal,

      ScanRfid,
      ScanReceiving,
      ScanDelivery,
      //ScanQC,
      DeliveryPlan,
      ListItemDelivery,
      ReviewDelivery
    }

    public enum EnumTypeMasterData
    {
      Client,
      TypeGoods,
      Warehouse,
      Tare,
      GroupProduct,
      Product,
    }
    public enum EnumStation
    {
      None = 0,
      Truck,
      Goods,
    }

    public enum EnumTypePopup
    {
      None = 0,
      Add,
      Update,
    }
  }
}
