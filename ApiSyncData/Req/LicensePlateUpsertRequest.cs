namespace ApiSyncData.Req
{
  public sealed class LicensePlateUpsertRequest
  {
    public Guid? Id { get; set; }
    public string LicensePlateCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool DeletedFlag { get; set; }
  }
}
