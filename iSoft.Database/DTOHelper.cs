using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections.Generic;
using static HelperManager.EnumData;
using static iSoft.Database.EnumData;


namespace iSoft.Database
{
  public static class DTOHelper
  {
    public static int utc = int.Parse(Environment.GetEnvironmentVariable("UTC"));
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
        Document = recordTruck.Document,
        Station = recordTruck.Station?.Name,
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
          No = orderedRecords.Count - index,
          Datetime = record.CreatedAt!=null ? ((DateTime)record.CreatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") : string.Empty,
          LicensePlate = record?.LicensePlate,
          ProductGroup = record?.Product?.ProductGroup?.Name,
          Product = record?.Product?.Name,
          CategoryTare = record?.CategoryTare?.Name,
          Gross = WeightFormatHelper.Format((record?.Net ?? 0.0) + (record?.Tare ?? 0.0), 3),
          Net = WeightFormatHelper.Format(record?.Net ?? 0.0, 3),
          Tare = WeightFormatHelper.Format(record?.Tare ?? 0.0, 3),
        })
        .ToList();
    }

    public static List<ClientDTO>? ConvertClientDTO(List<Client>? clients)
    {
      var rsDto = new List<ClientDTO>();
      if (clients?.Count()>0)
      {
        clients = clients.OrderBy(e => e.Name).ToList();
        rsDto = clients
          .Select((e, index) => new ClientDTO
          {
            Client = e,
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

    public static List<TypeGoodsDTO>? ConvertTypeGoodsDTO(List<TypeGoods>? typeGoods)
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
            Code = e.Code,
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

    public static List<CategoryTareDTO>? ConvertCategoryTareDTO(List<CategoryTare>? categoryTares)
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
            Value = WeightFormatHelper.Format(e?.Value ?? 0.0, 3),
            UpdatedAt = e?.UpdatedAt != null ? (((DateTime)e?.UpdatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? "") :
                                              (((DateTime)e?.CreatedAt).AddHours(utc).ToString("dd-MM-yyyy HH:mm:ss") ?? ""),
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
