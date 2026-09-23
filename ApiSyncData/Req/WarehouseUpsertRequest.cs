namespace ApiSyncData.Req
{
  public sealed class WarehouseUpsertRequest
  {
    /// <summary>
    /// Để trống khi tạo mới; truyền Id của kho khi cập nhật.
    /// </summary>
    public Guid? Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? SerialCode { get; set; }
    public string? Description { get; set; }
    public bool DeletedFlag { get; set; }
  }
}
