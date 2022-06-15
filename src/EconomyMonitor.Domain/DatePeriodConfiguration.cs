using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Domain;

/// <summary>
/// Конфигурация периодов дат.
/// </summary>
/// <remarks>
/// <para>
/// От этого типа нельзя унаследоваться.
/// </para>
/// <para>
/// Реализует <see cref="IDatePeriodConfiguration"/>.
/// </para>
/// </remarks>
public sealed class DatePeriodConfiguration : IDatePeriodConfiguration
{
    /// <summary>
    /// Возвращает или задаёт дату начала периода включительно.
    /// </summary>
    public DateOnly StartPeriodDateInclusive { get; set; }

    /// <summary>
    /// Возвращает или задаёт дату окончания периода исключительно.
    /// </summary>
    public DateOnly EndPeriodDateExclusive { get; set; }

    /// <summary>
    /// Создаёт конфигурацию периодов дат.
    /// </summary>
    public DatePeriodConfiguration()
    {
        
    }
}
