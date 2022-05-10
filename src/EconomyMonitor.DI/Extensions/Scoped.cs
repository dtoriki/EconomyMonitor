using EconomyMonitor.Data;
using EconomyMonitor.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EconomyMonitor.DI.Extensions;

/// <summary>
/// Provides adds for scope services.
/// </summary>
public static class Scoped
{
    private static readonly string DefaultConnectionStringName = "DefaultConnectionString";

    /// <summary>
    /// Adds scoped <see cref="IEconomyMonitorRepository"/> with Sql Lite provider.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection.</returns>
    public static IServiceCollection AddSqlLiteEconomyMonitorRepositoryScoped(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider()
            .GetRequiredService<IConfiguration>();

        string connectionName = DIOptions.ConnectionStringName ?? DefaultConnectionStringName;
        string? connectionString = configuration.GetConnectionString(connectionName);
        if (ArgsHelper.ThrowIfNull(connectionString))
        {
            return services;
        }

        DbContextOptions options = new DbContextOptionsBuilder()
            .UseSqlite(connectionString)
            .EnableThreadSafetyChecks()
            .Options;

        services.AddScoped(_ => IEconomyMonitorRepository.Create(options));

        return services;
    }
}
