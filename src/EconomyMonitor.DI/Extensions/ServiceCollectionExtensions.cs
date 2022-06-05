using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EconomyMonitor.Configuration;

namespace EconomyMonitor.DI.Extensions;

/// <summary>
/// Содержит методы расширения для <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Пытается найти строку подключеня к хранилищу данных из <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Строка подключения.</returns>
    /// <remarks>
    /// <para>
    /// Ищет в <paramref name="services"/> сервис <see cref="IConfiguration"/>.
    /// После чего методом <see cref="Configuration.ConfigurationExtensions.GetSqliteConnectionString"/>
    /// пытается найти строку подключения.
    /// </para>
    /// <para>
    /// Вернёт <see langword="null"/>, если не найдёт строку подключения.
    /// </para>
    /// </remarks>
    public static string? GetConnectionString(this IServiceCollection services)
    {
        return services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetSqliteConnectionString();
    }
}
