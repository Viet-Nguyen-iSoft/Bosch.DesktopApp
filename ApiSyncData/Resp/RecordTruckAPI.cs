namespace ApiSyncData.Resp
{
  public sealed class RecordTruckAPI
  {
    public string? Status { get; set; }
    public DataRecordTruck? Data { get; set; }
  }

  public sealed class DataRecordTruck
  {
    public int TotalRecord { get; set; }
    public List<ListDatumRecordTruck>? ListData { get; set; }
  }

  public sealed class ListDatumRecordTruck : IServerRecord
  {
    public Guid? Id { get; set; }
    public string? SerialCode { get; set; }
    public string? Plate { get; set; }
    public string? LicensePlate { get; set; }
    public string? NoLabelAuto { get; set; }
    public double Net01 { get; set; }
    public double Tare01 { get; set; }
    public double Net02 { get; set; }
    public double Tare02 { get; set; }
    public int EnumStatus { get; set; }
    public DateTime? WeighInAt { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? TypeGoodsId { get; set; }
    public Guid? WarehouseId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? StationId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
  }
}
