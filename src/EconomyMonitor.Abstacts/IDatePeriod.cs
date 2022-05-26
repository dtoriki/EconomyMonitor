namespace EconomyMonitor.Abstacts;

/// <summary>
/// Представляет тип периода дат.
/// </summary>
public interface IDatePeriod
{
    /// <summary>
    /// Возвращает дату начала периода.
    /// </summary>
    DateOnly StartingDate { get; set; }

    /// <summary>
    /// Возвращает дату окончания периода.
    /// </summary>
    DateOnly EndingDate { get; set; }

    /// <summary>
    /// Возвращает или задаёт доход за период.
    /// </summary>
    decimal Income { get; set; }
}
