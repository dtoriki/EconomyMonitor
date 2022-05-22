using EconomyMonitor.Services.Exceptions;
using EconomyMonitor.Wpf.MVVM.ViewModels.Application;
using EconomyMonitor.Wpf.MVVM.ViewModels.Header;
using EconomyMonitor.Wpf.MVVM.ViewModels.Periods;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.ViewModels;

/// <summary>
/// Presents view models locator.
/// </summary>
public class ViewModelLocator
{
    /// <summary>
    /// Gets <see cref="ApplicationViewModel"/>.
    /// </summary>
    public ApplicationViewModel ApplicationViewModel => GetServiceOrThrow<ApplicationViewModel>();

    /// <summary>
    /// Gets <see cref="HeaderMenuViewModel"/>.
    /// </summary>
    public HeaderMenuViewModel HeaderMenuViewModel => GetServiceOrThrow<HeaderMenuViewModel>();

    /// <summary>
    /// Gets <see cref="AddPeriodViewModel"/>.
    /// </summary>
    public AddPeriodViewModel AddPeriodViewModel => GetServiceOrThrow<AddPeriodViewModel>();

    /// <summary>
    /// Creates view model locator.
    /// </summary>
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

        Throw<ServiceNotFoundException>(typeof(TService));

        return service;
    }
}
