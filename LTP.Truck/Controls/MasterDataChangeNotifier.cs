namespace LTP.Truck.Controls
{
  public static class MasterDataChangeNotifier
  {
    public static event EventHandler<Type>? Changed;

    public static void Notify<TEntity>()
    {
      Changed?.Invoke(null, typeof(TEntity));
    }
  }
}
