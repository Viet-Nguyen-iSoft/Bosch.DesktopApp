using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections.Generic;
using static HelperManager.EnumData;
using static iSoft.Database.EnumData;
using RecordModel = iSoft.Database.Models.Record;


namespace iSoft.Database
{
  public static class DTOHelper
  {
    public static int utc = int.Parse(Environment.GetEnvironmentVariable("UTC"));

    public static RecordTruckDTO ConvertRecordTruckDTO(RecordModel record)
    {
      ArgumentNullException.ThrowIfNull(record);
      string status = "Đã xóa";
      if (!record.DeletedFlag)
      {
        status = record.EnumTypeDataTruck switch
        {
          EnumTypeDataTruck.None => "Chưa cân",
          EnumTypeDataTruck.WeightedTime01 => "Đang cân lần 1",
          EnumTypeDataTruck.DoneTime01 => "Chưa hoàn thành",
          EnumTypeDataTruck.WeightedTime02 => "Đã cân lần 2",
          EnumTypeDataTruck.DoneTime02 => "Hoàn thành",
          EnumTypeDataTruck.Delete => "Đã xóa",
          _ => record.EnumTypeDataTruck.ToString()
        };
      }

      return new RecordTruckDTO
      {
        Record = record,
        No = 1,
        NoLabelAuto = record.NoLabelAuto,
        NoLabelManual = record.NoLabelManual,
        NetTime01 = WeightFormatHelper.Format(record.NetTime01) + " Kg",
        NetTime02 = WeightFormatHelper.Format(record.NetTime02) + " Kg",
        Time01 = record.WeighInAt.HasValue
          ? record.WeighInAt.Value.AddHours(utc).ToString("dd/MM/yyyy HH:mm:ss")
          : "---",
        Time02 = record.WeighOutAt.HasValue
          ? record.WeighOutAt.Value.AddHours(utc).ToString("dd/MM/yyyy HH:mm:ss")
          : "---",
        Status = status,
        EnumTypeDataTruck = record.EnumTypeDataTruck,
        Client = record.Client?.Name,
        TypeGoods = record.TypeGoods?.Name,
        Warehouse = record.Warehouse?.Name,
        NameDriver = record.NameDriver,
        IdCard = record.IdCard,
        LicensePlate = record.LicensePlate,
        Document = record.Note,
        Station = record.Station?.Name,
        Operator = record.User?.FullName,
      };
    }

    public static List<RecordTruckDTO> ConvertRecordTruckDTO(
      List<RecordModel>? records)
    {
      if (records == null || records.Count == 0)
        return new List<RecordTruckDTO>();
      var ordered = records
        .OrderByDescending(record => record.CreatedAt)
        .ThenByDescending(record => record.Id)
        .ToList();
      return ordered.Select((record, index) =>
      {
        var dto = ConvertRecordTruckDTO(record);
        dto.No = ordered.Count - index;
        return dto;
      }).ToList();
    }

    public static List<RecordWeightDTO> ConvertCombinedHistoryDTO(
      IEnumerable<RecordModel>? records)
    {
      var ordered = (records ?? Enumerable.Empty<RecordModel>())
        .Select(record =>
        {
          bool isTruck = record.TypeData == EnumTypeDataLocation.Truck;
          var time = isTruck
            ? record.UpdatedAt ?? record.CreatedAt ?? DateTime.MinValue
            : record.CreatedAt ?? DateTime.MinValue;
          var gross = isTruck
            ? Math.Max(record.NetTime01, record.NetTime02)
            : record.NetTime01 + record.Tare;
          return new RecordWeightDTO
          {
            Record = record,
            // Adapter tạm cho các template PDF hiện hữu; dữ liệu nguồn vẫn là Records.
            RecordTruck = isTruck ? ToLegacyRecordTruck(record) : null,
            RecordWeight = isTruck ? null : ToLegacyRecordWeight(record),
            SortAt = time,
            Datetime = time == DateTime.MinValue
              ? string.Empty
              : time.AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss"),
            Source = isTruck ? "Cân xe tải" : "Cân hàng",
            Delivery = record.Client?.Name,
            ProductGroup = record.TypeGoods?.ProductGroup?.Name,
            Type = EnumHelper.GetDescription(record.TypeGoods?.EnumWasteType),
            Product = record.TypeGoods?.Name,
            CategoryTare = record.CategoryTare?.Name,
            Gross = WeightFormatHelper.Format(gross, 2),
            Net = WeightFormatHelper.Format(record.NetTime01, 2),
            Tare = WeightFormatHelper.Format(record.Tare, 2),
            LicensePlate = record.LicensePlate,
            NameDriver = record.NameDriver,
          };
        })
        .OrderByDescending(item => item.SortAt)
        .ThenByDescending(item => item.Record?.Id ?? Guid.Empty)
        .ToList();
      for (int index = 0; index < ordered.Count; index++)
        ordered[index].No = ordered.Count - index;
      return ordered;
    }

    private static RecordTruck ToLegacyRecordTruck(RecordModel record)
    {
      return new RecordTruck
      {
        Id = record.Id,
        NoLabelAuto = record.NoLabelAuto,
        NoLabelManual = record.NoLabelManual,
        NetTimeTemp = record.NetTimeTemp,
        TareTimeTemp = record.TareTimeTemp,
        NetTime01 = record.NetTime01,
        TareTime01 = record.TareTime01,
        NetTime02 = record.NetTime02,
        TareTime02 = record.TareTime02,
        Tare = record.Tare,
        NameTareForTruck = record.CategoryTare?.Name,
        NumberTare = record.NumberTare,
        EnumTypeDataTruck = record.EnumTypeDataTruck,
        NameDriver = record.NameDriver,
        IdCard = record.IdCard,
        LicensePlate = record.LicensePlate,
        Note = record.Note,
        ReasonDelete = record.ReasonDelete,
        WeighInAt = record.WeighInAt,
        WeighOutAt = record.WeighOutAt,
        Type = record.Type,
        ClientId = record.ClientId,
        Client = record.Client,
        ProductionId = record.ProductionId,
        TypeGoods = record.TypeGoods,
        WarehouseId = record.WarehouseId,
        Warehouse = record.Warehouse,
        UserId = record.UserId,
        User = record.User,
        StationId = record.StationId,
        Station = record.Station,
        CreatedAt = record.CreatedAt,
        CreatedBy = record.CreatedBy,
        UpdatedAt = record.UpdatedAt,
        UpdatedBy = record.UpdatedBy,
        DeletedFlag = record.DeletedFlag,
        EnableFlag = record.EnableFlag,
        SyncFlag = record.SyncFlag,
      };
    }

    private static RecordWeight ToLegacyRecordWeight(RecordModel record)
    {
      return new RecordWeight
      {
        Id = record.Id,
        Net = record.NetTime01,
        Tare = record.Tare,
        LicensePlate = record.LicensePlate,
        NameDriver = record.NameDriver,
        IdCard = record.IdCard,
        Note = record.Note,
        ReasonDelete = record.ReasonDelete,
        StationId = record.StationId,
        Station = record.Station,
        UserId = record.UserId,
        User = record.User,
        ProductId = record.ProductionId,
        Product = record.TypeGoods!,
        CategoryTareId = record.CategoryTareId,
        CategoryTare = record.CategoryTare!,
        DeliveryId = record.ClientId,
        Delivery = record.Client!,
        CreatedAt = record.CreatedAt,
        CreatedBy = record.CreatedBy,
        UpdatedAt = record.UpdatedAt,
        UpdatedBy = record.UpdatedBy,
        DeletedFlag = record.DeletedFlag,
        EnableFlag = record.EnableFlag,
        SyncFlag = record.SyncFlag,
      };
    }
    public static RecordTruckDTO ConvertRecordTruckDTO(RecordTruck recordTruck)
    {
      ArgumentNullException.ThrowIfNull(recordTruck);
      string status = "Đã xóa";
      if (recordTruck.DeletedFlag == false)
      {
        status = recordTruck.EnumTypeDataTruck switch
        {
          EnumTypeDataTruck.None => "Chưa cân",
          EnumTypeDataTruck.WeightedTime01 => "Đang cân lần 1",
          EnumTypeDataTruck.DoneTime01 => "Chưa hoàn thành",
          EnumTypeDataTruck.WeightedTime02 => "Đã cân lần 2",
          EnumTypeDataTruck.DoneTime02 => "Hoàn thành",
          EnumTypeDataTruck.Delete => "Đã xóa",
          _ => recordTruck.EnumTypeDataTruck.ToString()
        };
      }

      return new RecordTruckDTO
      {
        RecordTruck = recordTruck,
        No = 1,
        NoLabelAuto = recordTruck.NoLabelAuto,
        NoLabelManual = recordTruck.NoLabelManual,
        NetTime01 = WeightFormatHelper.Format(recordTruck.NetTime01) + " Kg",
        NetTime02 = WeightFormatHelper.Format(recordTruck.NetTime02) + " Kg",
        Time01 = recordTruck.WeighInAt!=null ? ((DateTime)recordTruck.WeighInAt).AddHours(utc).ToString("dd/MM/yyyy HH:mm:ss") : "---",
        Time02 = recordTruck.WeighOutAt != null ? ((DateTime)recordTruck.WeighOutAt).AddHours(utc).ToString("dd/MM/yyyy HH:mm:ss") : "---",
        Status = status,
        EnumTypeDataTruck = recordTruck.EnumTypeDataTruck,
        Client = recordTruck.Client?.Name,
        TypeGoods = recordTruck.TypeGoods?.Name,
        Warehouse = recordTruck.Warehouse?.Name,
        NameDriver = recordTruck.NameDriver,
        IdCard = recordTruck.IdCard,
        LicensePlate = recordTruck.LicensePlate,
        Document = recordTruck.Note,
        Station = recordTruck.Station?.Name,
        Operator = recordTruck.User?.FullName,
      };
    }

    public static List<RecordTruckDTO> ConvertRecordTruckDTO(List<RecordTruck>? recordTrucks)
    {
      if (recordTrucks == null || recordTrucks.Count == 0)
        return new List<RecordTruckDTO>();

      return recordTrucks
        .OrderByDescending(recordTruck => recordTruck.CreatedAt)
        .ThenByDescending(recordTruck => recordTruck.Id)
        .Select((recordTruck, index) =>
        {
          var dto = ConvertRecordTruckDTO(recordTruck);
          dto.No = recordTrucks.Count - index;
          return dto;
        })
        .ToList();
    }

    public static List<RecordWeightDTO> ConvertRecordWeightDTO(List<RecordWeight>? recordWeights)
    {
      if (recordWeights == null || recordWeights.Count == 0)
        return new List<RecordWeightDTO>();

      var orderedRecords = recordWeights
        .OrderByDescending(record => record.CreatedAt)
        .ThenByDescending(record => record.Id)
        .ToList();

      return orderedRecords
        .Select((record, index) => new RecordWeightDTO
        {
          RecordWeight = record,
          SortAt = record.CreatedAt ?? DateTime.MinValue,
          No = orderedRecords.Count - index,
          Datetime = record.CreatedAt!=null ? ((DateTime)record.CreatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") : string.Empty,
          Source = "Cân hàng",
          LicensePlate = record?.LicensePlate,
          Delivery = record?.Delivery?.Name,
          NameDriver = record?.NameDriver,
          ProductGroup = record?.Product?.ProductGroup?.Name,
          Type = EnumHelper.GetDescription(record?.Product?.EnumWasteType),
          Product = record?.Product?.Name,
          CategoryTare = record?.CategoryTare?.Name,
          Gross = WeightFormatHelper.Format((record?.Net ?? 0.0) + (record?.Tare ?? 0.0), 2),
          Net = WeightFormatHelper.Format(record?.Net ?? 0.0, 2),
          Tare = WeightFormatHelper.Format(record?.Tare ?? 0.0, 2),
        })
        .ToList();
    }

    public static List<RecordWeightDTO> ConvertCombinedHistoryDTO(
      IEnumerable<RecordWeight>? recordWeights,
      IEnumerable<RecordTruck>? recordTrucks)
    {
      var result = ConvertRecordWeightDTO(recordWeights?.ToList());

      result.AddRange((recordTrucks ?? Enumerable.Empty<RecordTruck>())
        .Select(record =>
        {
          var firstWeight = record.NetTime01;
          var secondWeight = record.NetTime02;
          var gross = Math.Max(firstWeight, secondWeight);
          var vehicleNet = Math.Abs(secondWeight - firstWeight);
          var netAfterTare = Math.Max(0, vehicleNet - record.Tare);
          var time = record.UpdatedAt ?? record.CreatedAt ?? DateTime.MinValue;

          return new RecordWeightDTO
          {
            RecordTruck = record,
            SortAt = time,
            Datetime = time == DateTime.MinValue
              ? string.Empty
              : time.AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss"),
            Source = "Cân xe tải",
            Delivery = record.Client?.Name,
            ProductGroup = record.TypeGoods?.ProductGroup?.Name,
            Type = EnumHelper.GetDescription(record.TypeGoods?.EnumWasteType),
            Product = record.TypeGoods?.Name,
            CategoryTare = record.NameTareForTruck,
            Gross = WeightFormatHelper.Format(gross, 2),
            Net = WeightFormatHelper.Format(record.NetTime01, 2),
            Tare = WeightFormatHelper.Format(record.Tare, 2),
            LicensePlate = record.LicensePlate,
            NameDriver = record.NameDriver,
          };
        }));

      result = result
        .OrderByDescending(item => item.SortAt)
        .ThenByDescending(item => item.RecordWeight?.Id ?? item.RecordTruck?.Id ?? Guid.Empty)
        .ToList();

      for (int index = 0; index < result.Count; index++)
        result[index].No = result.Count - index;

      return result;
    }

    public static List<TypeGoodsDTO>? ConvertTypeGoodsDTO(List<Product>? typeGoods)
    {
      var rsDto = new List<TypeGoodsDTO>();
      if (typeGoods?.Count() > 0)
      {
        typeGoods = typeGoods.OrderBy(e => e.Name).ToList();
        rsDto = typeGoods
          .Select((e, index) => new TypeGoodsDTO
          {
            TypeGoods = e,
            No = index + 1,
            Group = e.ProductGroup?.Name,
            WasteType = EnumHelper.GetDescription(e.EnumWasteType),
            Code = e.Code,
            Name = e.Name,
            TagMappingReport = e.TagMappingReport,
            Description = e.Description,
            UpdatedAt = e?.UpdatedAt != null ? (((DateTime)e?.UpdatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? "") :
                                              (((DateTime)e?.CreatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? ""),
          })
          .OrderBy(e => e.Name)
          .ToList();
      }
      return rsDto;
    }

    public static List<WareHouseDTO>? ConvertWareHouseDTO(List<Warehouse>? warehouses)
    {
      var rsDto = new List<WareHouseDTO>();
      if (warehouses?.Count() > 0)
      {
        warehouses = warehouses.OrderBy(e => e.Name).ToList();
        rsDto = warehouses
          .Select((e, index) => new WareHouseDTO
          {
            Warehouse = e,
            No = index + 1,
            Name = e.Name,
            Description = e.Description,
            UpdatedAt = e?.UpdatedAt != null ? (((DateTime)e?.UpdatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? "") :
                                              (((DateTime)e?.CreatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? ""),
          })
          .OrderBy(e => e.Name)
          .ToList();
      }
      return rsDto;
    }

    public static List<CategoryTareDTO>? ConvertCategoryTareDTO(List<Tare>? categoryTares)
    {
      var rsDto = new List<CategoryTareDTO>();
      if (categoryTares?.Count() > 0)
      {
        categoryTares = categoryTares.OrderBy(e => e.Name).ToList();
        rsDto = categoryTares
          .Select((e, index) => new CategoryTareDTO
          {
            CategoryTare = e,
            No = index + 1,
            Code = e.Code,
            Name = e.Name,
            Description = e.Description,
            Value = WeightFormatHelper.Format(e?.Value ?? 0.0, 2),
            UpdatedAt = e?.UpdatedAt != null ? (((DateTime)e?.UpdatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? "") :
                                              (((DateTime)e?.CreatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? ""),
          })
          .OrderBy(e => e.Name)
          .ToList();
      }
      return rsDto;
    }

    public static List<TareTruckDTO>? ConvertTareTruckDTO(List<Tare>? tareTrucks)
    {
      var rsDto = new List<TareTruckDTO>();
      if (tareTrucks?.Count() > 0)
      {
        tareTrucks = tareTrucks.OrderBy(e => e.Name).ToList();
        rsDto = tareTrucks
          .Select((e, index) => new TareTruckDTO
          {
            TareTruck = e,
            No = index + 1,
            Name = e.Name,
            Description = e.Description,
            Value = WeightFormatHelper.Format(
              Math.Round(e.Value ?? 0.0, MidpointRounding.AwayFromZero)),
            UpdatedAt = e.UpdatedAt != null
              ? e.UpdatedAt.Value.AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss")
              : e.CreatedAt?.AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? string.Empty,
          })
          .OrderBy(e => e.Name)
          .ToList();
      }
      return rsDto;
    }

    public static List<ProductGroupDTO>? ConvertProductGroupDTO(List<ProductGroup>? productGroups)
    {
      var rsDto = new List<ProductGroupDTO>();
      if (productGroups?.Count() > 0)
      {
        productGroups = productGroups.OrderBy(e => e.Name).ToList();
        rsDto = productGroups
          .Select((e, index) => new ProductGroupDTO
          {
            ProductGroup = e,
            No = index + 1,
            Name = e.Name,
            Description = e.Description,
            UpdatedAt = e?.UpdatedAt != null ? (((DateTime)e?.UpdatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? "") :
                                              (((DateTime)e?.CreatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? ""),
          })
          .OrderBy(e => e.Name)
          .ToList();
      }
      return rsDto;
    }

    public static List<DeliveryDTO>? ConvertDeliveryDTO(List<ClientDelivery>? deliveries)
    {
      var rsDto = new List<DeliveryDTO>();
      if (deliveries?.Count > 0)
      {
        rsDto = deliveries
          .OrderBy(delivery => delivery.Name)
          .Select((delivery, index) => new DeliveryDTO
          {
            ClientDelivery = delivery,
            No = index + 1,
            Name = delivery.Name,
            OfficeAddress = delivery.OfficeAddress,
            PhoneForOfficeAddress = delivery.PhoneForOfficeAddress,
            AgentAddress = delivery.AgentAddress,
            AgentAddressForOfficeAddress = delivery.PhoneForAgentAddress,
            Description = delivery.Description,
            UpdatedAt = (delivery.UpdatedAt ?? delivery.CreatedAt)?.AddHours(utc)
              .ToString("dd-MM-yyyy HH:mm:ss") ?? string.Empty,
          })
          .ToList();
      }
      return rsDto;
    }

    public static List<DocumentDataDTO> ConvertDocumentDataDTO(
      List<DocumentData>? documents)
    {
      return documents?
        .OrderByDescending(document => document.Date)
        .ThenBy(document => document.Document)
        .Select((document, index) => new DocumentDataDTO
        {
          DocumentData = document,
          No = index + 1,
          Date = document.Date?.AddHours(utc).ToString("dd-MM-yyyy") ?? string.Empty,
          Document = document.Document,
          Description = document.Description,
          UpdatedAt = (document.UpdatedAt ?? document.CreatedAt)?.AddHours(utc)
            .ToString("dd-MM-yyyy HH:mm:ss") ?? string.Empty,
        })
        .ToList() ?? new List<DocumentDataDTO>();
    }

    public static List<ProductDTO>? ConvertProductDTO(List<Product>? products)
    {
      var rsDto = new List<ProductDTO>();
      if (products?.Count() > 0)
      {
        products = products.OrderBy(e => e.Name).ToList();
        rsDto = products
          .Select((e, index) => new ProductDTO
          {
            Product = e,
            No = index + 1,
            Group = e.ProductGroup?.Name,
            Code = e.Code,
            Name = e.Name,
            WasteType = EnumHelper.GetDescription(e.EnumWasteType),
            TagMappingReport = e.TagMappingReport,
            Description = e.Description,
            UpdatedAt = e?.UpdatedAt != null ? (((DateTime)e?.UpdatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? "") :
                                              (((DateTime)e?.CreatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? ""),
          })
          .OrderBy(e => e.Name)
          .ToList();
      }
      return rsDto;
    }


    public static List<UserDTO> ConvertUserDTO(List<User>? users)
    {
      if (users == null || users.Count == 0)
        return new List<UserDTO>();

      return users
        .OrderBy(e => e.FullName)
        .ThenBy(e => e.Username)
        .Select((e, index) => new UserDTO
        {
          User = e,
          No = index + 1,
          FullName = e.FullName,
          DisplayName = e.DisplayName,
          Username = e.Username,
          EmployeeCode = e.EmployeeCode,
          UpdatedAt = (e.UpdatedAt ?? e.CreatedAt)?.AddHours(utc)
            .ToString("dd-MM-yyyy HH:mm:ss") ?? string.Empty,
        })
        .ToList();
    }

    public static DTOPrintLabel? ConvertProductDTO(RecordWeight recordWeight)
    {
      return new DTOPrintLabel()
      {
        ProductGroup = recordWeight?.Product?.ProductGroup?.Name ?? string.Empty,
        Product = recordWeight?.Product?.Name ?? string.Empty,
        TypeTare = recordWeight?.CategoryTare?.Name ?? string.Empty,
        Net = recordWeight?.Net ?? 0.0,
        Tare = recordWeight?.Tare ?? 0.0,
        Datetime = recordWeight?.CreatedAt?.ToString("dd-MM-yyyy HH:mm:ss")
      };
    }



    











  }
}
