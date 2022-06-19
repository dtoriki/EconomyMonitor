using EconomyMonitor.Data.Abstracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.DI;

/// <summary>
/// Содержит методы расширения, конфигурирующие <see cref="IAppRepository"/>.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public static class ConfigureAppRepositoryExtensions
{
    /// <summary>
    /// Конфигурирует <see cref="IAppRepository"/> с временем существования <see cref="ServiceLifetime.Scoped"/>
    /// и добавляет его в <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <returns>Коллекция сервисов.</returns>
    /// <remarks>
    /// <para>
    /// Добавляет <see cref="IAppRepository"/> в <paramref name="services"/> методом 
    /// <see cref="EntityFrameworkServiceCollectionExtensions.AddDbContext{TContextService, TContextImplementation}(IServiceCollection, Action{DbContextOptionsBuilder}?, ServiceLifetime, ServiceLifetime)"/>.
    /// </para>
    /// <para>
    /// Конфигурирует <see cref="DbContextOptionsBuilder"/>, вызывая метод <see cref="DbContextOptionsBuilderExtensions.ConfigureSqliteDbContextOptionsBuilder(DbContextOptionsBuilder, string)"/>.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="connectionString"/> - <see langword="null"/>.
    /// </exception>
    public static IServiceCollection ConfigureAppRepositoryScoped( 
        this IServiceCollection services, 
        string connectionString)
    {
        _ = ThrowIfArgumentNull(connectionString);

        services
            .AddDbContext<IRepository, AppRepository>(
                options => _ = options.ConfigureSqliteDbContextOptionsBuilder(connectionString),
                ServiceLifetime.Scoped, 
                ServiceLifetime.Scoped);

        return services;
    }
}
