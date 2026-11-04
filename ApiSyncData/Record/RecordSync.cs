using static iSoft.Database.EnumData;

namespace ApiSyncData.Record
{
  /// <summary>
  /// Payload đồng bộ bản ghi cân hợp nhất lên API desktop.
  /// RecordType: 1 = cân xe tải, 2 = cân hàng.
  /// </summary>
  public sealed class RecordSync
  {
    public Guid Id { get; set; }
    public EnumTypeDataLocation RecordType { get; set; }
    public string? NoLabelAuto { get; set; }
    public string? NoLabelManual { get; set; }
    public double NetTime01 { get; set; }
    public double Tare01 { get; set; }
    public double NetTime02 { get; set; }
    public double Tare02 { get; set; }
    public EnumTypeDataTruck EnumTypeDataTruck { get; set; }
    public string? NameDriver { get; set; }
    public string? IdCard { get; set; }
    public string? LicensePlate { get; set; }
    public string? Document { get; set; }
    public Guid? StationId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? TypeGoodsId { get; set; }
    public Guid? CategoryTareId { get; set; }
    public Guid? ProductionId { get; set; }
    public Guid? WarehouseId { get; set; }
    public DateTime? WeighInAt { get; set; }
    public DateTime? WeighOutAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool DeletedFlag { get; set; }
    public string? ReasonDelete { get; set; }
  }
}
