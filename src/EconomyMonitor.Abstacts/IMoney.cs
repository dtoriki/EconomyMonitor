namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип денег.
/// </summary>
public interface IMoney
{
    /// <summary>
    /// Возвращает количество денег.
    /// </summary>
    decimal Amount { get; }
}
