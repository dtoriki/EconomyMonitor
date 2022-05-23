using System.Reflection;
using Microsoft.Extensions.Configuration;
using static System.Reflection.Assembly;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Configuration;

/// <summary>
/// Contains <see cref="IConfigurationBuilder"/> extensions.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Configures <see cref="IConfigurationBuilder"/>.
    /// </summary>
    /// <param name="configurationBuilder">Configuration builder.</param>
    /// <param name="resourcePath">Path to configuration resources.</param>
    /// <returns>Configured configuration builder.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static IConfigurationBuilder ConfigureConfiguration(
        this IConfigurationBuilder configurationBuilder,
        string resourcePath)
    {
        _ = ThrowIfArgumentNull(configurationBuilder);
        _ = ThrowIfArgumentNull(resourcePath);

        Assembly? assembly = GetCallingAssembly();

        if (ThrowIfNull(assembly))
        {
            return configurationBuilder;
        }

        string upperResourcePath = resourcePath.ToUpperInvariant();
        string resourceName = assembly.GetManifestResourceNames()
            .Single(n => n.ToUpperInvariant().Contains(upperResourcePath));

        Stream? stream = assembly.GetManifestResourceStream(resourceName);

        if (ThrowIfNull(stream))
        {
            return configurationBuilder;
        }

        configurationBuilder.AddJsonStream(stream);

        return configurationBuilder;
    }

    /// <inheritdoc cref="ConfigureConfiguration(IConfigurationBuilder, string)"/>
    public static IConfigurationBuilder ConfigureConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder.ConfigureConfiguration(Configuration.AppsettingsFile);
    }

    /// <summary>
    /// Configures <see cref="IConfigurationBuilder"/> as development.
    /// </summary>
    /// <param name="configurationBuilder">Configuration builder.</param>
    /// <param name="resourcePath">Path to configuration resources.</param>
    /// <returns>Configured configuration builder.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static IConfigurationBuilder ConfigureDevConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder.ConfigureConfiguration(Configuration.AppsettingsDevFile);
    }

    /// <summary>
    /// Gets connection string from <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Connection string.</returns>
    public static string? GetConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString(Configuration.ConnectionName);
    }
}
