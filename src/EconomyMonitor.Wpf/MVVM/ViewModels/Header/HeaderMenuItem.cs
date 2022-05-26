using System.Windows;
using EconomyMonitor.Wpf.EventArguments;
using EconomyMonitor.Wpf.MVVM.Primitives;
using static EconomyMonitor.Helpers.SetHelper;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Header;

internal sealed class HeaderMenuItem
{
    private IconWithText? _iconWithText;
    private bool _isSelected;
    private UIElement? _uiElement;

    public IconWithText IconWithText
    {
        get => _iconWithText ??= new IconWithText();
        set => _ = NullCheckSet(ref _iconWithText, value);
    }

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

    public UIElement? UIElement
    {
        get => _uiElement;
        set => _ = Set(ref _uiElement, value);
    }

    public event EventHandler<PropertyValueChangedEventArgs<bool>>? SelectedChanged;

    public HeaderMenuItem()
    {

    }
}
