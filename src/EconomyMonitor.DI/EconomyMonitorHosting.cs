using EconomyMonitor.DI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EconomyMonitor.DI;

/// <summary>
/// Provides methods for hosting. 
/// </summary>
public static class EconomyMonitorHosting
{
    private static readonly string APP_SETTINGS_FILE = "appsettings.json";
    private static readonly string APP_SETTINGS_DEV_FILE = "appsettings.Development.json";

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
                config.ConfigureConfiguration(APP_SETTINGS_FILE);

                if (host.HostingEnvironment.IsDevelopment())
                {
                    config.ConfigureConfiguration(APP_SETTINGS_DEV_FILE);
                }
            })
            .ConfigureServices((_, services) => servicesConfiguration?.Invoke(services))
            .Build();

        return host;
    }
}
