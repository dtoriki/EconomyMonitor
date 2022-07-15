namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип настроек приложения.
/// </summary>
public interface ISettings
{
    /// <summary>
    /// Возвращает или задаёт стартовый бюджет.
    /// </summary>
    decimal? StartingBudget { get; set; }
}
