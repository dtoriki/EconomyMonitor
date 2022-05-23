using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.DI;

/// <summary>
/// <see cref="IEconomyMonitorRepository"/> configuration extensions.
/// </summary>
public static class ConfigureEconomyMonitorRepositoryExtensions
{
    /// <summary>
    /// Configures <see cref="IEconomyMonitorRepository"/> as <see cref="ServiceLifetime.Scoped"/>.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="connectionString">Connection string.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection ConfigureEconomyMonitorRepositoryScoped(
        this IServiceCollection services, 
        string connectionString)
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
