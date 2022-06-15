using EconomyMonitor.Wpf.MVVM.ViewModels.Header;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Application;

internal sealed class ApplicationViewModel : NotifyPropertyChangedBase
{
    private HeaderMenuViewModel? _header;

    public HeaderMenuViewModel? Header
    {
        get => _header;
        set => _ = SetPropertyNotifiable(ref _header, value);
    }

    public ApplicationViewModel()
    {
        
    }  
}
