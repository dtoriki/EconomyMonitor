namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип лимита трат.
/// </summary>
public interface ISpendingQuota
{
    /// <summary>
    /// Возвращает значение лимита трат.
    /// </summary>
    decimal Quota { get; }
}
