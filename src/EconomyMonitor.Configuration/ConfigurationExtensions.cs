using System.Reflection;
using Microsoft.Extensions.Configuration;
using static System.Reflection.Assembly;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Configuration;

/// <summary>
/// Содержит методы расширения для <see cref="IConfigurationBuilder"/>.
/// </summary>
/// <exception cref="ArgumentNullException"/>
/// <exception cref="NullReferenceException"/>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Загружет конфигурацию приложения и конфигурирует <see cref="IConfigurationBuilder"/>.
    /// </summary>
    /// <param name="configurationBuilder">Экземпляр типа, используемого для построения конфигурации.</param>
    /// <param name="resourcePath">Путь до конфигурации в ресурсах сборки.</param>
    /// <returns>Сконфигурированный экземпляр типа, используемый для построения конфигурации.</returns>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="resourcePath"/> - <see langword="null"/>.
    /// </exception>
    /// <exception cref="NullReferenceException">
    /// Возникает, если не удалось с помощью <see cref="Assembly.GetManifestResourceInfo(string)"/> 
    /// получить поток <see cref="Stream"/> располагающийся в <paramref name="resourcePath"/>.
    /// </exception>
    /// <remarks>
    /// Пытается найти встроенный ресурс в сборку по пути <paramref name="resourcePath"/>.
    /// Если не находит, то вызывает исключение <see cref="NullReferenceException"/>.
    /// После чего создаёт поток <see cref="Stream"/> ресурса сборки и конфигурирует им <paramref name="configurationBuilder"/>,
    /// используя метод <see cref="JsonConfigurationExtensions.AddJsonStream(IConfigurationBuilder, Stream)"/>.
    /// Если не удалось создать поток <see cref="Stream"/> ресурса сборки, то вызывает <see cref="NullReferenceException"/>.
    /// </remarks>
    public static IConfigurationBuilder ConfigureConfiguration( //ToDo: переименовать Setup...
        this IConfigurationBuilder configurationBuilder,
        string resourcePath)
    {
        _ = ThrowIfArgumentNull(resourcePath);

        Assembly assembly = GetCallingAssembly();

        string upperResourcePath = resourcePath.ToUpperInvariant();
        string? resourceName = assembly.GetManifestResourceNames()
            .SingleOrDefault(n => n.ToUpperInvariant().Contains(upperResourcePath));

        if (ThrowIfNull(resourceName))
        {
            return configurationBuilder;
        }

        Stream? stream = assembly.GetManifestResourceStream(resourceName);

        if (ThrowIfNull(stream))
        {
            return configurationBuilder;
        }

        configurationBuilder.AddJsonStream(stream);

        return configurationBuilder;
    }

    /// <inheritdoc cref="ConfigureConfiguration(IConfigurationBuilder, string)"/>
    public static IConfigurationBuilder ConfigureConfiguration(this IConfigurationBuilder configurationBuilder) //ToDo: переименовать Setup...
    {
        return configurationBuilder.ConfigureConfiguration(Configuration.AppsettingsFile);
    }

    /// <summary>
    /// <inheritdoc cref="ConfigureConfiguration(IConfigurationBuilder, string)"/> Для среды разработки.
    /// </summary>
    /// <inheritdoc cref="ConfigureConfiguration(IConfigurationBuilder, string)"/>
    public static IConfigurationBuilder ConfigureDevConfiguration(this IConfigurationBuilder configurationBuilder) //ToDo: переименовать Setup...
    {
        return configurationBuilder.ConfigureConfiguration(Configuration.AppsettingsDevFile);
    }

    /// <summary>
    /// Предоставляет строку подключения, полученную из <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">Экземпляр конфигурации.</param>
    /// <returns>Строка подключения.</returns>
    /// <remarks>
    /// Вернёт <see langword="null"/>, если не найдёт строку подключения.
    /// </remarks>
    public static string? GetConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString(Configuration.ConnectionName);
    }
}
