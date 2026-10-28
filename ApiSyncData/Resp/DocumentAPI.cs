namespace ApiSyncData.Resp
{
  public sealed class DocumentAPI
  {
    public string? Status { get; set; }
    public DataDocument? Data { get; set; }
  }

  public sealed class DataDocument
  {
    public int? TotalRecord { get; set; }
    public List<ListDatumDocument>? ListData { get; set; }
  }

  public sealed class ListDatumDocument : IServerRecord
  {
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public DateTime? DocumentDate { get; set; }
    public string? Description { get; set; }
    public int? CreateFrom { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
  }
}
