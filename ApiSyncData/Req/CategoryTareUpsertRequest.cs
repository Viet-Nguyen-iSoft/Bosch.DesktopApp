namespace ApiSyncData.Req
{
  public sealed class CategoryTareUpsertRequest
  {
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? SerialCode { get; set; }
    public decimal? WeightTare { get; set; }
    public string? Description { get; set; }
    public bool DeletedFlag { get; set; }
  }
}
