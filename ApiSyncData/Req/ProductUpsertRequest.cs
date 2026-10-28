namespace ApiSyncData.Req
{
  public sealed class ProductUpsertRequest
  {
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? SerialCode { get; set; }
    public string? Description { get; set; }
    public Guid? ProductGroupId { get; set; }
    public int WasteType { get; set; }
    public string? TagNameProductGoods { get; set; }
    public int TypeWeight { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool DeletedFlag { get; set; }
  }
}
