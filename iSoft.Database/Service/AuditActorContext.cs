namespace iSoft.Database.Service
{
  /// <summary>
  /// Thông tin người dùng hiện tại để tầng dữ liệu gắn người thao tác vào audit.
  /// </summary>
  public static class AuditActorContext
  {
    public static Guid? UserId { get; private set; }
    public static string? Username { get; private set; }
    public static string? DisplayName { get; private set; }
    public static string? Source { get; private set; }

    public static void SetActor(
      Guid? userId, string? username, string? displayName)
    {
      UserId = userId;
      Username = username;
      DisplayName = displayName;
    }

    public static void Clear() => SetActor(null, null, null);

    public static void SetSource(string? source) => Source = source;
  }
}
