using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Services;

/// <summary>
/// Тип сервиса управления состоянием пользовательских настроек приложения.
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// Асинхронно возвращает пользовательские настройки приложения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пользовательские настройки приложения.</returns>
    Task<ISettings?> GetSettingsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Асинхронно сохраняет полученные пользовательские настройки приложения.
    /// </summary>
    /// <param name="settings">Пользовательские настройки приложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// <see langword="true"/>, если пользовательские настройки приложения успешно созранились,
    /// иначе - <see langword="false"/>.
    /// </returns>
    Task<bool> SaveSettingsAsync(ISettings settings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Асинхронно загружает пользовательские настройки приложения из хранилища данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пользовательские настройки приложения.</returns>
    Task<ISettings?> LoadSettingsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает текущие пользовательские настройки приложения без обращения к хранилищу.
    /// </summary>
    /// <returns>Пользовательские настройки приложения.</returns>
    ISettings? GetCurrentSettingsValue();

    /// <summary>
    /// Асинхронно устанавливает значение стартового бюджета.
    /// </summary>
    /// <param name="startingBudget">Значение стартового бюджета.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// <see langword="true"/>, если удалось установить значение бюджета, 
    /// иначе - <see langword="false"/>
    /// </returns>
    Task<bool> SaveStartingBudgetAsync(decimal startingBudget, CancellationToken cancellationToken = default);
}
