namespace EconomyMonitor.Abstacts;

/// <summary>
/// Представляет тип с уникальным идентификатором.
/// </summary>
public interface IUniqueRequired
{
    /// <summary>
    /// Возвращает уникальный идентификатор.
    /// </summary>
    Guid Id { get; }
}
