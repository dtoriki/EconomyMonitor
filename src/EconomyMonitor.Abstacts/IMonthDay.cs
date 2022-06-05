using EconomyMonitor.Primitives.Enums;

namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип дня месяца.
/// </summary>
public interface IMonthDay
{
    /// <summary>
    /// Возвращает номер дня месяца.
    /// </summary>
    int? MonthDay { get; }

    /// <summary>
    /// Возвращает тип дня месяца.
    /// </summary>
    MonthDayType MonthDayType { get; }
}
