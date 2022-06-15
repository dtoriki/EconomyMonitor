namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип конфигурации периода дат.
/// </summary>
/// <remarks>
/// Наследует <see cref="IMonthDay"/>.
/// </remarks>
public interface IDatePeriodConfiguration
{
    /// <summary>
    /// Возвращает дату начала периода включительно.
    /// </summary>
    DateOnly StartPeriodDateInclusive { get; }

    /// <summary>
    /// Возвращает дату окончания периода исключительно.
    /// </summary>
    DateOnly EndPeriodDateExclusive { get; }
}
