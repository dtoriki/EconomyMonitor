using EconomyMonitor.Wpf.MVVM;
using EconomyMonitor.Wpf.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EconomyMonitor.Wpf;

internal static class ConfigureViewModelsExtensions
{
    public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
    {
        Type[] viewModelTypes = typeof(ViewModelLocator)
            .GetProperties()
            .Where(x => x.PropertyType.BaseType is not null)
            .Where(x => x.PropertyType.BaseType!.Equals(typeof(NotifyPropertyChangedBase)))
            .Select(x => x.PropertyType)
            .ToArray();

        foreach (Type viewModelType in viewModelTypes)
        {
            services.AddScoped(viewModelType);
        }

        return services;
    }
}
