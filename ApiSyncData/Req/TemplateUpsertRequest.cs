namespace ApiSyncData.Req
{
  public sealed class TemplateUpsertRequest
  {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? SerialCode { get; set; }
    public bool IsBBGN { get; set; }
    public string TemplateFilePath { get; set; } = string.Empty;
  }
}
