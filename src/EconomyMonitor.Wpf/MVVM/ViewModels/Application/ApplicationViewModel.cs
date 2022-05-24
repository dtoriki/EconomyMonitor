using EconomyMonitor.Wpf.MVVM.ViewModels.Header;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Application;

/// <summary>
/// Present the application view model.
/// </summary>
public sealed class ApplicationViewModel : ViewModelBase
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
