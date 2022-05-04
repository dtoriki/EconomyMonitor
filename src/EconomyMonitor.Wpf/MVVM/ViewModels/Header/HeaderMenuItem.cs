using System.Windows;
using EconomyMonitor.Helpers;
using EconomyMonitor.Wpf.MVVM.Primitives;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Header;

/// <summary>
/// Presents header's menu item.
/// </summary>
public class HeaderMenuItem : DependencyObject
{
    private IconWithText? _iconWithText;

    /// <summary>
    /// Gets or sets icon with text.
    /// </summary>
    public IconWithText IconWithText
    { 
        get => _iconWithText ??= new IconWithText(); 
        set => _ = ArgsHelper.NullCheckSet(ref _iconWithText, value); 
    }

    /// <summary>
    /// Creates header's menu item.
    /// </summary>
    public HeaderMenuItem()
    {

    }
}
