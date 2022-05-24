using System.Windows;
using EconomyMonitor.DI;
using EconomyMonitor.DI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static EconomyMonitor.DI.EconomyMonitorHosting;

namespace EconomyMonitor.Wpf;

/// <summary>
/// Application class.
/// </summary>
public partial class App : Application
{
    
    private static readonly long CACHE_SIZE_LIMIT = 400000000;

    public static IHost? Host { get; private set; }

    /// <summary>
    /// Creates application.
    /// </summary>
    /// <param name="host">Application host.</param>
    public App() 
    { 

    }

    /// <inheritdoc/>
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Host = BuildHost(ConfigureServices);

        await Host.InitDatabaseAsync().ConfigureAwait(false);
        await Host.StartAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        if (Host is not null)
        {
            await Host.StopAsync().ConfigureAwait(false);
            Host.Dispose(); 
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache(x =>
        {
            x.TrackLinkedCacheEntries = true;
            x.SizeLimit = CACHE_SIZE_LIMIT;
        });

        services
            .ConfigureSqliteEconomyMonitorRepository()
            .ConfigureEntityWithDtoMappers()
            .ConfigureUnitsOfWorkScoped()
            .ConfigureViewModels();
    }    
}
