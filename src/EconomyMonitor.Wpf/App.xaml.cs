using System.Windows;
using Microsoft.Extensions.Hosting;

namespace EconomyMonitor.Wpf;

/// <summary>
/// Application class.
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    /// <summary>
    /// Creates application.
    /// </summary>
    /// <param name="host">Application host.</param>
    public App(IHost host) => _host = host;

    
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    private App() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    /// <inheritdoc/>
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        await _host.StartAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        await _host.StopAsync().ConfigureAwait(false);
        _host.Dispose();
    }
}
