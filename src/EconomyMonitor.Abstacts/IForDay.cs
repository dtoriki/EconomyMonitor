namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип, представляющий день.
/// </summary>
public interface IForDay
{
    /// <summary>
    /// Возвращает дату. 
    /// </summary>
    DateOnly Date { get; }
}
