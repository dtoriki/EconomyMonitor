using EconomyMonitor.Configuration;
using EconomyMonitor.Data.Abstracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    /// Конфигурирует <see cref="IRepository"/> с временем существования <see cref="ServiceLifetime.Scoped"/>
    /// и добавляет его в <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <returns>Коллекция сервисов.</returns>
    /// <remarks>
    /// <para>
    /// Добавляет <see cref="IRepository"/> в <paramref name="services"/> методом 
    /// <see cref="EntityFrameworkServiceCollectionExtensions.AddDbContext{TContextService, TContextImplementation}(IServiceCollection, Action{DbContextOptionsBuilder}?, ServiceLifetime, ServiceLifetime)"/>.
    /// </para>
    /// <para>
    /// Конфигурирует <see cref="DbContextOptionsBuilder"/>, вызывая метод <see cref="DbContextOptionsBuilderExtensions.ConfigureSqliteDbContextOptionsBuilder(DbContextOptionsBuilder, string)"/>.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="connectionString"/> - <see langword="null"/>.
    /// </exception>
    public static IServiceCollection ConfigureAppSqliteRepositoryScoped( 
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

    /// <summary>
    /// Конфигурирует <see cref="IRepository"/>, используя поставщик данных Sqlite 
    /// с временем существования <see cref="ServiceLifetime.Scoped"/>
    /// и добавляет его в <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов.</returns>
    /// <remarks>
    /// <para>
    /// Методом <see cref="ServiceCollectionExtensions.GetConnectionString(IServiceCollection)"/>
    /// пытается полчить строку подключения. Если строка подключения не найдена, то вызывает <see cref="ArgumentNullException"/>.
    /// </para>
    /// <para>
    /// Методом <see cref="ConfigureAppSqliteRepositoryScoped(IServiceCollection, string)"/>
    /// конфигурирует экземпляр <see cref="IAppRepository"/> со временем жизни <see cref="ServiceLifetime.Scoped"/>
    /// и помещает его в <paramref name="services"/>.
    /// </para>
    /// </remarks>
    /// <exception cref="NullReferenceException">
    /// Возникает, если не удалось найти строку подключения.
    /// </exception>
    public static IServiceCollection ConfigureSqliteAppRepository(this IServiceCollection services)
    {
        string? connectionString = services.GetSqliteConnectionString();
        if (ThrowIfNull(connectionString))
        {
            return services;
        }

        services.ConfigureAppSqliteRepositoryScoped(connectionString);

        return services;
    }

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
    public static string? GetSqliteConnectionString(this IServiceCollection services)
    {
        return services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetSqliteConnectionString();
    }
}
