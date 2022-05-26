namespace EconomyMonitor.Wpf.EventArguments;

internal sealed class PropertyValueChangedEventArgs<T> : EventArgs
{
    public T? OldValue { get; set; }

    public T? NewValue { get; set; }

    public PropertyValueChangedEventArgs() : base() { }
}
