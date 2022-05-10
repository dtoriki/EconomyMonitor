using System.Windows.Controls;
using EconomyMonitor.Wpf.EventArguments;
using EconomyMonitor.Wpf.MVVM.Primitives;
using static EconomyMonitor.Helpers.SetHelper;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Header;

/// <summary>
/// Presents header's menu item.
/// </summary>
public sealed class HeaderMenuItem
{
    private IconWithText? _iconWithText;
    private bool _isSelected;
    private Control? _control;

    /// <summary>
    /// Gets or sets icon with text.
    /// </summary>
    public IconWithText IconWithText
    {
        get => _iconWithText ??= new IconWithText();
        set => _ = NullCheckSet(ref _iconWithText, value);
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

            if (NullCheckSet(ref _isSelected, value))
            {
                SelectedChanged?.Invoke(this, new PropertyValueChangedEventArgs<bool>
                {
                    NewValue = value,
                    OldValue = oldValue
                });
            }
        }
    }

    /// <summary>
    /// Gets or sets control for present.
    /// </summary>
    public Control? Control
    {
        get => _control;
        set => _ = Set(ref _control, value);
    }

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
