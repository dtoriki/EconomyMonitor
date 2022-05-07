using EconomyMonitor.Wpf.MVVM.Abstracts;
using EconomyMonitor.Wpf.MVVM.ViewModels.Header;

namespace EconomyMonitor.Wpf.MVVM.ViewModels;

/// <summary>
/// Present the application view model.
/// </summary>
public class ApplicationViewModel : ViewModelBase
{
    private HeaderMenuViewModel? _header;

    /// <summary>
    /// Gets or sets header as <see cref="HeaderMenuViewModel"/>.
    /// </summary>
    public HeaderMenuViewModel? Header
    {
        get => _header;
        set => _ = SetPropertyNotifiable(ref _header, value);
    }

    /// <summary>
    /// Creates the application view model.
    /// </summary>
    public ApplicationViewModel()
    {

    }
}