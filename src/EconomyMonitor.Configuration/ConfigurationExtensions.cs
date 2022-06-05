using System.Reflection;
using Microsoft.Extensions.Configuration;
using static System.Environment;
using static System.IO.Path;
using static System.Reflection.Assembly;
using static EconomyMonitor.Configuration.Configuration;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Configuration;

/// <summary>
/// Содержит методы расширения для <see cref="IConfiguration"/> и <see cref="IConfigurationBuilder"/>.
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
    public static IConfigurationBuilder SetupConfiguration(
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

    /// <inheritdoc cref="SetupConfiguration(IConfigurationBuilder, string)"/>
    public static IConfigurationBuilder SetupConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder.SetupConfiguration(AppsettingsFile);
    }

    /// <summary>
    /// <inheritdoc cref="SetupConfiguration(IConfigurationBuilder, string)"/> Для среды разработки.
    /// </summary>
    /// <inheritdoc cref="SetupConfiguration(IConfigurationBuilder, string)"/>
    public static IConfigurationBuilder SetupDevConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder.SetupConfiguration(AppsettingsDevFile);
    }

    /// <summary>
    /// Предоставляет строку подключения к хранилищу данных Sqlite.
    /// </summary>
    /// <param name="configuration">Экземпляр конфигурации.</param>
    /// <returns>Строка подключения.</returns>
    /// <remarks>
    /// <para>
    /// Ищет имя файла локального хранилища в <paramref name="configuration"/>,
    /// после чего формирует строку подключения, используя директорию <see cref="SpecialFolder.UserProfile"/>.
    /// </para>
    /// <para>
    /// Вернёт <see langword="null"/>, если не удалось сформировать строку подключения.
    /// </para>
    /// </remarks>
    public static string? GetSqliteConnectionString(this IConfiguration configuration)
    {
        string? pathToFile = configuration.GetPathToSqliteStorageFile();
        if (string.IsNullOrWhiteSpace(pathToFile))
        {
            return null;
        }

        string? fileName = configuration.GetLocalStorageFileName();
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return null;
        }

        return configuration.GetConnectionString(ConnectionName)?
            .Replace(fileName, configuration.GetPathToSqliteStorageFile());
    }

    /// <summary>
    /// Возвращает путь до файла локального хранилища данных Sqlite.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Путь до файла локального хранилища.</returns>
    public static string? GetPathToSqliteStorageFile(this IConfiguration configuration)
    {
        const string ECONOMY_MONITOR = "EconomyMonitor";

        string? fileName = configuration.GetLocalStorageFileName();

        if (fileName is null)
        {
            return null;
        }

        return Combine(GetFolderPath(SpecialFolder.UserProfile), ECONOMY_MONITOR, fileName);
    }

    /// <summary>
    /// Возвращает имя файла локального хранилища, взятого из <paramref name="configuration"/>.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Имя файла локального хранилища.</returns>
    public static string? GetLocalStorageFileName(this IConfiguration configuration)
    {
        const string DATA_SOURCE_LITERAL = "DATA SOURCE";
        const char ASSIGNMENT_SYMBOL = '=';
        const char SEQUENCE_SEPARATOR = ';';

        string? connectionString = configuration.GetConnectionString(ConnectionName);

        string[]? dataSourceSegments = connectionString?
            .Split(
                SEQUENCE_SEPARATOR,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .FirstOrDefault(x => x.ToUpper().Contains(DATA_SOURCE_LITERAL))?
            .Split(
                ASSIGNMENT_SYMBOL,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (dataSourceSegments?.Length != 2)
        {
            return null;
        }

        return dataSourceSegments.Last();
    }

    /// <summary>
    /// Возвращает путь до директории в которой хранится файл локального хранилища.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Путь до директории, в которой хранится файл локального хранилища.</returns>
    public static string? GetPathToSqliteStorageDirectory(this IConfiguration configuration)
    {
        string? filePath = configuration.GetPathToSqliteStorageFile();
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return null;
        }

        string[] pathSegments = filePath.Split(
            DirectorySeparatorChar,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        string[] pathToFolderSegments = pathSegments
            .Take(pathSegments.Length - 1)
            .ToArray();

        return Combine(pathToFolderSegments);
    }
}
