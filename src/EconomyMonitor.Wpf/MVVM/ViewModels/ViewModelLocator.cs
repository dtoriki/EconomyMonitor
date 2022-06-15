using EconomyMonitor.Services.Exceptions;
using EconomyMonitor.Wpf.MVVM.ViewModels.Application;
using EconomyMonitor.Wpf.MVVM.ViewModels.Header;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.ViewModels;

internal static class ViewModelLocator
{
    public static ApplicationViewModel ApplicationViewModel => GetServiceOrThrow<ApplicationViewModel>();

    public static HeaderMenuViewModel HeaderMenuViewModel => GetServiceOrThrow<HeaderMenuViewModel>();

    //public static AddDatePeriodConfigurationDialogViewModel AddDatePeriodViewModel => GetServiceOrThrow<AddDatePeriodConfigurationDialogViewModel>();

    private static TService GetServiceOrThrow<TService>()
        where TService : class
    {
        TService? service = App.Host?.Services.GetRequiredService<TService>();
        if (service is not null)
        {
            return service;
        }

        Throw<DependencyNotFoundException>(typeof(TService));

        return service;
    }
}
