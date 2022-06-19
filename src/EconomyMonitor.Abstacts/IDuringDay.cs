namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип, поддерживающий счёт времени в течении дня.
/// </summary>
public interface IDuringDay
{
    /// <summary>
    /// Возвращает время.
    /// </summary>
    TimeOnly Time { get; }
}
