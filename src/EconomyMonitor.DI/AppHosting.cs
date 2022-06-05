using EconomyMonitor.Configuration;
using EconomyMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.DI;

/// <summary>
/// Предоставляет методы расширения для хостинга <see cref="IHost"/>. 
/// </summary>
public static class AppHosting
{
    /// <summary>
    /// Производит инициализацию хоста приложения.
    /// </summary>
    /// <param name="servicesConfiguration">Делегат конфигурации сервисов.</param>
    /// <returns>Иницилизированный хостинг.</returns>
    /// <remarks>
    /// <para>
    /// Методом <see cref="Host.CreateDefaultBuilder"/> создаёт экземпляр <see cref="IHostBuilder"/>.
    /// </para>
    /// <para>
    /// Методом <see cref="HostingHostBuilderExtensions.UseContentRoot(IHostBuilder, string)"/>
    /// устанавливает корневой котолог в <see cref="Environment.CurrentDirectory"/>.
    /// </para>
    /// <para>
    /// Методом <see cref="HostingHostBuilderExtensions.ConfigureAppConfiguration(IHostBuilder, Action{IConfigurationBuilder})"/>
    /// устанавливает конфигурацию приложения <see cref="Environment.CurrentDirectory"/>:
    /// <list type="bullet">
    /// <item>
    /// Для конфигурации решения DEBUG конфигурация устанавливается методом 
    /// <see cref="Configuration.ConfigurationExtensions.SetupDevConfiguration(IConfigurationBuilder)"/>.
    /// </item>
    /// <item>
    /// Для конфигурации решения RELEASE конфигурация устанавливается методом 
    /// <see cref="Configuration.ConfigurationExtensions.SetupConfiguration(IConfigurationBuilder)"/>.
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// Методом <see cref="IHostBuilder.ConfigureServices(Action{HostBuilderContext, IServiceCollection})"/>
    /// устанавливается делегат конфигурации сервисов <paramref name="servicesConfiguration"/>.
    /// </para>
    /// <para>
    /// Вызывает метод <see cref="IHostBuilder.Build"/> для сборки хоста.
    /// </para>
    /// </remarks>
    public static IHost BuildHost(Action<IServiceCollection>? servicesConfiguration = null)
    {
        IHost host = Host.CreateDefaultBuilder()
            .UseContentRoot(Environment.CurrentDirectory)
            .ConfigureAppConfiguration((host, config) =>
            {
#if DEBUG
                config.SetupDevConfiguration();
#else
                config.ConfigureConfiguration();
#endif

            })
            .ConfigureServices((_, services) => servicesConfiguration?.Invoke(services))
            .Build();

        return host;
    }

    /// <summary>
    /// Асинхронно создаёт локальное хранилище данных.
    /// </summary>
    /// <param name="host">Хост приложения.</param>
    /// <returns>Хост приложения.</returns>
    public static async Task<IHost> CreateLocalStorageAsync(this IHost host)
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
        
        var context = (DbContext)scope.ServiceProvider.GetRequiredService<IAppRepository>();

        await context.Database
            .MigrateAsync()
            .ConfigureAwait(false);

        return host;
    }
}
