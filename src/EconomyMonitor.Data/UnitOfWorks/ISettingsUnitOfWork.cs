using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.UnitOfWorks;

/// <summary>
/// Тип части работы с хранилищем данных, который предоставляет доступ к данным с типо <see cref="ISettings"/>.
/// </summary>
public interface ISettingsUnitOfWork
{
    /// <summary>
    /// Асинхронно получает экземпляр типа настроек приложения <see cref="ISettings"/>.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Экземпляр типа настроек приложения <see cref="ISettings"/>.</returns>
    /// <remarks>
    /// Вернёт <see langword="null"/>, если не обнаружит экземпляр типа настроек приложения в хранилище данных.
    /// </remarks>
    Task<ISettings?> GetSettingsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Асинхронно сохраняет полученные настройки приложения.
    /// </summary>
    /// <param name="settings">Экземпляр сохраняемых настроек приложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task SaveSettingsAsync(ISettings settings, CancellationToken cancellationToken = default);
}
