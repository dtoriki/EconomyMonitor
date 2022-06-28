using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Mappers;
using EconomyMonitor.Mapping.DI;
using Microsoft.Extensions.DependencyInjection;

namespace EconomyMonitor.Data.DI;

/// <summary>
/// Содержит методы расширения <see cref="IServiceCollection"/> для конфигурирования типов сопоставления объектов.
/// </summary>
public static class ConfigureEntityMappersExtensions
{
    /// <summary>
    /// Конфигурирует тип сопастовления для объектов типа <see cref="ISettings"/>.
    /// </summary>
    /// <param name="services">Коллекция зависимостей прилоложения.</param>
    /// <returns>Сконфигурированная коллекция зависимостей приложенияю.</returns>
    public static IServiceCollection AddSettingsMapperScoped(this IServiceCollection services)
    {
        return services.ConfigureMapperScoped<SettingsMapProfile, ISettingsMapper>(provier => new SettingsMapper(provier));
    }
}
