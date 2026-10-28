namespace ApiSyncData.Req
{
  public sealed class DocumentUpsertRequest
  {
    public Guid? Id { get; set; }
    public DateTime DocumentDate { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool DeletedFlag { get; set; }
  }
}
