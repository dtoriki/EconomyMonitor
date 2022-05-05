using System.Windows.Controls;
using EconomyMonitor.Wpf.EventArguments;
using EconomyMonitor.Wpf.Helpers;
using EconomyMonitor.Wpf.MVVM.Primitives;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Header;

/// <summary>
/// Presents header's menu item.
/// </summary>
public class HeaderMenuItem
{
    private IconWithText? _iconWithText;
    private bool _isSelected;

    /// <summary>
    /// Gets or sets icon with text.
    /// </summary>
    public IconWithText IconWithText
    {
        get => _iconWithText ??= new IconWithText();
        set => _ = ArgsHelper.NullCheckSet(ref _iconWithText, value);
    }

    /// <summary>
    /// Gets or set selection state.
    /// </summary>
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            bool oldValue = _isSelected;

            if (ArgsHelper.NullCheckSet(ref _isSelected, value))
            {
                SelectedChanged?.Invoke(this, new PropertyValueChangedEventArgs<bool>
                {
                    NewValue = value,
                    OldValue = oldValue
                });
            }
        }
    }

    public Control? Control { get; set; }

    /// <summary>
    /// Rises when <see cref="IsSelected"/> was changed.
    /// </summary>
    public event EventHandler<PropertyValueChangedEventArgs<bool>>? SelectedChanged;

    /// <summary>
    /// Creates header's menu item.
    /// </summary>
    public HeaderMenuItem()
    {

    }
}
