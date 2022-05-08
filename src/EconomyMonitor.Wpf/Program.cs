using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EconomyMonitor.Wpf;

/// <summary>
/// Application entry point class.
/// </summary>
internal sealed class Program
{
    private const string APP_SETTINGS_FILE = "appsettings.json";
    private const string APP_SETTINGS_DEV_FILE = "appsettings.Development.json";

    private Program() { }

    /// <summary>
    /// Application entry point.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        App app = new();
        app.InitializeComponent();
        app.Run();
    }

    /// <summary>
    /// Creates, configure and returns host builder.
    /// </summary>
    /// <param name="args">CL arguments.</param>
    /// <returns>Host builder.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseContentRoot(Environment.CurrentDirectory)
            .ConfigureAppConfiguration((host, config) =>
            {
                config
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile(APP_SETTINGS_FILE);

                if (host.HostingEnvironment.IsDevelopment())
                {
                    config.AddJsonFile(APP_SETTINGS_DEV_FILE);
                }

            });
}
