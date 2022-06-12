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

    /// <summary>
    /// Возвращет процент от допустимого уровня трат.
    /// </summary>
    decimal? Percent { get; }
}
