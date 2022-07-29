using EconomyMonitor.Data.DI;
using EconomyMonitor.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace EconomyMonitor.Services.DI;

/// <summary>
/// Конфигурирует сервисы.
/// </summary>
public static class ConfigureServicesExtensions
{
    /// <summary>
    /// Конфигурирует зависимость <see cref="ISettingsService"/> со временем жизни <see cref="ServiceLifetime.Scoped"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Сконфигурированная коллекция сервисов.</returns>
    public static IServiceCollection ConfigureSettingsServiceScoped(this IServiceCollection services)
    {
        return services
            .AddSettingsMapperScoped()
            .ConfigureSettingsUnitOfWork()
            .AddScoped<ISettingsService, SettingsService>();
    }

    public static IServiceCollection ConfigureBudgetServiceScoped(this IServiceCollection services)
    {
        return services
            .AddScoped<IBudgetService, BudgetService>();
    }
}
