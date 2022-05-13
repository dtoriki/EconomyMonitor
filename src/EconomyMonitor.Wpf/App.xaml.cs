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
    private static readonly string CONNECTION_NAME = "LocalStorage";
    private static readonly long CACHE_SIZE_LIMIT = 400000000;

    private IHost? _host;

    /// <summary>
    /// Creates application.
    /// </summary>
    /// <param name="host">Application host.</param>
    public App() { }

    /// <inheritdoc/>
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = BuildHost(ConfigureServices);
        await _host.StartAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        if (_host is not null)
        {
            await _host.StopAsync().ConfigureAwait(false);
            _host.Dispose(); 
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        DIOptions.ConnectionStringName = CONNECTION_NAME;

        services.AddMemoryCache(x =>
        {
            x.TrackLinkedCacheEntries = true;
            x.SizeLimit = CACHE_SIZE_LIMIT;
        });

        services
            .ConfigureSqlLiteEconomyMonitorRepositoryScoped()
            .ConfigureEntityWithDtoMappers()
            .ConfigureUnitsOfWorkScoped();
    }
}
