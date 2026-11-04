namespace LTP.Truck.Controls
{
  public static class MasterDataChangeNotifier
  {
    public static event EventHandler<Type>? Changed;

    public static void Notify<TEntity>()
    {
      Notify(typeof(TEntity));
    }

    public static void Notify(Type entityType)
    {
      ArgumentNullException.ThrowIfNull(entityType);
      var subscribers = Changed;
      if (subscribers == null)
        return;

      foreach (EventHandler<Type> subscriber in subscribers.GetInvocationList())
      {
        try
        {
          subscriber(null, entityType);
        }
        catch (Exception ex)
        {
          HelperManager.LogHelper.LogErrorToFileLog(
            new InvalidOperationException(
              $"Không thể cập nhật giao diện master data {entityType.Name}.",
              ex),
            AppCore.Ins._folderFileLog);
        }
      }
    }
  }
}
