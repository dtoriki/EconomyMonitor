using System.Reflection;
using Microsoft.Extensions.Configuration;
using static System.Reflection.Assembly;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.DI.Extensions;

/// <summary>
/// Contains <see cref="IConfigurationBuilder"/> extensions.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public static class Configuration
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

        Assembly? assembly = GetEntryAssembly();

        if (ThrowIfArgumentNull(assembly))
        {
            return configurationBuilder;
        }

        string upperResourcePath = resourcePath.ToUpperInvariant();
        string resourceName = assembly.GetManifestResourceNames()
            .Single(n => n.ToUpperInvariant().Contains(upperResourcePath));

        Stream? stream = assembly.GetManifestResourceStream(resourceName);

        if (ThrowIfArgumentNull(stream))
        {
            return configurationBuilder;
        }

        configurationBuilder.AddJsonStream(stream);

        return configurationBuilder;
    }
}
