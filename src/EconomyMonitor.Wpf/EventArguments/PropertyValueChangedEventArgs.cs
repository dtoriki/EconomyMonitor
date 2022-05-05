namespace EconomyMonitor.Wpf.EventArguments;

/// <summary>
/// Presents a property value changed event data.
/// </summary>
/// <typeparam name="T">Type of property.</typeparam>
public sealed class PropertyValueChangedEventArgs<T> : EventArgs
{
    /// <summary>
    /// Gets or sets old value.
    /// </summary>
    public T? OldValue { get; set; }

    /// <summary>
    /// Gets or sets new value.
    /// </summary>
    public T? NewValue { get; set; }

    /// <summary>
    /// Creates a property value changed event data.
    /// </summary>
    public PropertyValueChangedEventArgs() : base() { }
}
