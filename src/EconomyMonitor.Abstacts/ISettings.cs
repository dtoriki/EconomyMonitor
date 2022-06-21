namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип настроек приложения.
/// </summary>
public interface ISettings
{
    /// <summary>
    /// Возвращает стартовый бюджет.
    /// </summary>
    decimal StartingBudget { get; }
}
