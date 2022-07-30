using EconomyMonitor.DI.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.ViewModels;

internal static class ViewModelLocator
{
    public static WindowControlViewModel WindowControlViewModel => GetServiceOrThrow<WindowControlViewModel>();
    public static BudgetInfoViewModel BudgetInfoViewModel => GetServiceOrThrow<BudgetInfoViewModel>();

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
