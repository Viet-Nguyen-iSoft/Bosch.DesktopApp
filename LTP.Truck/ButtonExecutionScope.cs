namespace LTP.Truck
{
  /// <summary>
  /// Prevents a button from being clicked again while its asynchronous handler is running.
  /// </summary>
  internal sealed class ButtonExecutionScope : IDisposable
  {
    private readonly Control? _button;
    private readonly bool _wasEnabled;

    private ButtonExecutionScope(object? sender)
    {
      _button = sender as Control;
      if (_button == null || _button.IsDisposed)
        return;

      _wasEnabled = _button.Enabled;
      _button.Enabled = false;
    }

    public static ButtonExecutionScope Enter(object? sender)
    {
      return new ButtonExecutionScope(sender);
    }

    public void Dispose()
    {
      if (_button == null || _button.IsDisposed)
        return;

      _button.Enabled = _wasEnabled;
    }
  }
}
