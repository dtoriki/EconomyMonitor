using System.Diagnostics.CodeAnalysis;
using EconomyMonitor.Services.Exceptions;
using EconomyMonitor.Wpf.MVVM.ViewModels.Application;
using EconomyMonitor.Wpf.MVVM.ViewModels.Header;
using EconomyMonitor.Wpf.MVVM.ViewModels.Periods;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.ViewModels;

internal class ViewModelLocator
{
    [SuppressMessage("Performance", "CA1822:Пометьте члены как статические", Justification = "<Ожидание>")]
    public ApplicationViewModel ApplicationViewModel => GetServiceOrThrow<ApplicationViewModel>();

    [SuppressMessage("Performance", "CA1822:Пометьте члены как статические", Justification = "<Ожидание>")]
    public HeaderMenuViewModel HeaderMenuViewModel => GetServiceOrThrow<HeaderMenuViewModel>();

    [SuppressMessage("Performance", "CA1822:Пометьте члены как статические", Justification = "<Ожидание>")]
    public AddDatePeriodDialogViewModel AddDatePeriodViewModel => GetServiceOrThrow<AddDatePeriodDialogViewModel>();

    public ViewModelLocator()
    {

    }

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
