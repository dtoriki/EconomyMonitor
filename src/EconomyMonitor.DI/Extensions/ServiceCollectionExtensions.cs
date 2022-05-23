using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EconomyMonitor.Configuration;

namespace EconomyMonitor.DI.Extensions;

/// <summary>
/// <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Gets connection string from <paramref name="services"/>.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <returns>Connection string.</returns>
    public static string? GetConnectionString(this IServiceCollection services)
    {
        return services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetConnectionString();
    }
}
