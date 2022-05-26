using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.DI;

/// <summary>
/// Содержит методы расширения, конфигурирующие <see cref="IEconomyMonitorRepository"/>.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public static class ConfigureEconomyMonitorRepositoryExtensions // ToDo: Переименовать.
{
    /// <summary>
    /// Конфигурирует <see cref="IEconomyMonitorRepository"/> с временем существования <see cref="ServiceLifetime.Scoped"/>
    /// и добавляет его в <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <returns>Коллекция сервисов.</returns>
    /// <remarks>
    /// <para>
    /// Добавляет <see cref="IEconomyMonitorRepository"/> в <paramref name="services"/> методом 
    /// <see cref="EntityFrameworkServiceCollectionExtensions.AddDbContext{TContextService, TContextImplementation}(IServiceCollection, Action{DbContextOptionsBuilder}?, ServiceLifetime, ServiceLifetime)"/>.
    /// </para>
    /// <para>
    /// Конфигурирует <see cref="DbContextOptionsBuilder"/>, вызывая метод <see cref="DbContextOptionsBuilderExtensions.ConfigureSqliteDbContextOptionsBuilder(DbContextOptionsBuilder, string)"/>.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="connectionString"/> - <see langword="null"/>.
    /// </exception>
    public static IServiceCollection ConfigureEconomyMonitorRepositoryScoped( 
        this IServiceCollection services, 
        string connectionString) // ToDo: Переименовать.
    {
        _ = ThrowIfArgumentNull(connectionString);

        services
            .AddDbContext<IEconomyMonitorRepository, EconomyMonitorRepository>(
                options => _ = options.ConfigureSqliteDbContextOptionsBuilder(connectionString),
                ServiceLifetime.Scoped, 
                ServiceLifetime.Scoped);

        return services;
    }
}
