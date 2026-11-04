using static iSoft.Database.EnumData;

namespace iSoft.Database.Models
{
  /// <summary>
  /// Bản ghi cân hợp nhất. Các cột nghiệp vụ lấy RecordTruck làm chuẩn.
  /// </summary>
  public class Record : BaseModel
  {
    public EnumTypeDataLocation TypeData { get; set; }

    public string? NoLabelAuto { get; set; }
    public string? NoLabelManual { get; set; }

    public double NetTimeTemp { get; set; }
    public double TareTimeTemp { get; set; }
    public double NetTime01 { get; set; }
    public double TareTime01 { get; set; }
    public double NetTime02 { get; set; }
    public double TareTime02 { get; set; }
    public double Tare { get; set; }
    public int? NumberTare { get; set; } = 1;
    public EnumTypeDataTruck EnumTypeDataTruck { get; set; }

    public string? NameDriver { get; set; }
    public string? IdCard { get; set; }
    public string? LicensePlate { get; set; }
    public string? Note { get; set; }
    public string? ReasonDelete { get; set; }
    public DateTime? WeighInAt { get; set; }
    public DateTime? WeighOutAt { get; set; }
    public string? Type { get; set; } //Nhập hàng, Xuất hàng, Khác

    public Guid? ClientId { get; set; }
    public ClientDelivery? Client { get; set; }

    public Guid? TypeGoodsId { get; set; }
    public Product? TypeGoods { get; set; }

    public Guid? CategoryTareId { get; set; }
    public Tare? CategoryTare { get; set; }

    public Guid? WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? StationId { get; set; }
    public Station? Station { get; set; }
  }
}
