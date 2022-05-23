using EconomyMonitor.Configuration;
using EconomyMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.DI;

/// <summary>
/// Provides methods for hosting. 
/// </summary>
public static class EconomyMonitorHosting
{
    /// <summary>
    /// Builds host.
    /// </summary>
    /// <param name="servicesConfiguration">Services configuration handler.</param>
    /// <returns>Host.</returns>
    public static IHost BuildHost(Action<IServiceCollection>? servicesConfiguration = null)
    {
        IHost host = Host.CreateDefaultBuilder()
            .UseContentRoot(Environment.CurrentDirectory)
            .ConfigureAppConfiguration((host, config) =>
            {
#if DEBUG
                config.ConfigureDevConfiguration();
#else
                config.ConfigureConfiguration();
#endif

            })
            .ConfigureServices((_, services) => servicesConfiguration?.Invoke(services))
            .Build();

        return host;
    }

    /// <summary>
    /// Asynchronusly initializes data base. 
    /// </summary>
    /// <param name="host">Host.</param>
    /// <returns>Host.</returns>
    public static async Task<IHost> InitDatabaseAsync(this IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();

        string[]? relativePathSegments = scope.ServiceProvider
            .GetRequiredService<IConfiguration>()
            .GetConnectionString()?
            .Split(Path.PathSeparator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault(x => x.ToUpper().Contains("DATA SOURCE"))?
            .Split('=', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .LastOrDefault()?
            .Split(Path.DirectorySeparatorChar, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (ThrowIfNull(relativePathSegments))
        {
            return null!;
        }

        IEnumerable<string> pathSegments = Environment.CurrentDirectory
            .Split(Path.DirectorySeparatorChar, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Concat(relativePathSegments);

        string folderPath = Path.Combine(pathSegments.Take(pathSegments.Count() - 1).ToArray());
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        
        var context = (DbContext)scope.ServiceProvider.GetRequiredService<IEconomyMonitorRepository>();

        await context.Database
            .MigrateAsync()
            .ConfigureAwait(false);

        return host;
    }
}
