namespace EconomyMonitor.Services;

/// <summary>
/// Тип сервиса управления бюджетом.
/// </summary>
public interface IBudgetService
{
    /// <summary>
    /// Асинхронно возвращает текущий бюджет.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Текущий бюджет</returns>
    Task<decimal> GetBudgetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Асинхронно устанавливает текущий бюджет.
    /// </summary>
    /// <param name="budget">Текущий бюджет.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task SetBudgetAsync(decimal budget, CancellationToken cancellationToken = default);
}