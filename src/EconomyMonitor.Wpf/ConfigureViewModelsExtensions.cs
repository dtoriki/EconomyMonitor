using EconomyMonitor.Wpf.MVVM;
using EconomyMonitor.Wpf.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EconomyMonitor.Wpf;

/// <summary>
/// <see cref="IServiceCollection"/> extensions for configuring view models.
/// </summary>
public static class ConfigureViewModelsExtensions
{
    /// <summary>
    /// Configures <see cref="IServiceCollection"/> with view models.
    /// </summary>
    /// <param name="services">Services.</param>
    /// <returns>Configured services.</returns>
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
