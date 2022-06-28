using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Data.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace EconomyMonitor.Data.DI;

/// <summary>
/// Содержит методы расширения <see cref="IServiceCollection"/> для конфигурации частей работы с данными из хранилища данных.
/// </summary>
public static class ConfigureUnitsOfWorkExtensions
{
    /// <summary>
    /// Конфигурирет зависимость <see cref="ISettingsUnitOfWork"/>, предназначенную для работы с сущностями типа <see cref="ISettingsEntity"/>.
    /// </summary>
    /// <param name="services">Коллекция зависмостей приложеня.</param>
    /// <returns>Сконфигурированная коллекциязависимостей приложения.</returns>
    public static IServiceCollection ConfigureSettingsUnitOfWork(this IServiceCollection services)
    {
        return services
            .AddSettingsMapperScoped()
            .AddScoped<ISettingsUnitOfWork, SettingsUnitOfWork<AppRepository>>();
    }
}
