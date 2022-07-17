using EconomyMonitor.Wpf.MVVM;
using EconomyMonitor.Wpf.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EconomyMonitor.Wpf;

internal static class ConfigureViewModelsExtensions
{
    public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
    {
        IEnumerable<Type> viewModelTypes = typeof(ViewModelLocator)
            .GetProperties()
            .Where(x => x.PropertyType.IsAssignableTo(typeof(NotifyPropertyChangedBase)))
            .Select(x => x.PropertyType);

        foreach (Type viewModelType in viewModelTypes)
        {
            services.AddScoped(viewModelType);
        }

        return services;
    }
}
