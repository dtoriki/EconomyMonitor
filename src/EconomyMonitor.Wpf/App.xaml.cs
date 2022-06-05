using System.Windows;
using EconomyMonitor.DI;
using EconomyMonitor.DI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static EconomyMonitor.DI.AppHosting;

namespace EconomyMonitor.Wpf;

internal partial class App : Application
{
    
    private static readonly long CACHE_SIZE_LIMIT = 400000000;

    public static IHost? Host { get; private set; }

    public App() 
    { 

    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Host = BuildHost(ConfigureServices);

        await Host.CreateLocalStorageAsync().ConfigureAwait(false);
        await Host.StartAsync().ConfigureAwait(false);
    }

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
            .ConfigureSqliteAppRepository()
            .ConfigureMappers()
            .ConfigureUnitsOfWorkScoped()
            .ConfigureViewModels();
    }    
}
