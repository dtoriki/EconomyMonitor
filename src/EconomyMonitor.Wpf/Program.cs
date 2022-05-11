using EconomyMonitor.DI;
using EconomyMonitor.DI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static EconomyMonitor.DI.EconomyMonitorHosting;

namespace EconomyMonitor.Wpf;

/// <summary>
/// Application entry point class.
/// </summary>
internal sealed class Program
{
    private static readonly string CONNECTION_NAME = "LocalStorage";
    private static readonly long CACHE_SIZE_LIMIT = 400000000;

    private Program() { }

    /// <summary>
    /// Application entry point.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        IHost host = BuildHost(ConfigureServices);
        App app = new(host);
        app.InitializeComponent();
        app.Run();
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
            .AddSqlLiteEconomyMonitorRepositoryScoped()
            .AddEntityWithDtoMappers();

        
        
    }
}
