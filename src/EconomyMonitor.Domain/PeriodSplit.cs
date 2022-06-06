using EconomyMonitor.Abstacts;
using EconomyMonitor.Primitives.Enums;

namespace EconomyMonitor.Domain;

/// <summary>
/// Содержит информацию о разделении периода на части.
/// </summary>
/// <remarks>
/// <para>
/// От этого типа нельзя унаследоваться.
/// </para>
/// Реализует <see cref="IMonthDay"/>.
/// </remarks>
public sealed class PeriodSplit : IMonthDay
{
    /// <summary>
    /// Возвращает или задаёт тип дня месяца, по которому будет делиться период.
    /// </summary>
    public MonthDayType MonthDayType { get; set; }

    /// <summary>
    /// Возвращает или задаёт день, по которому будет делиться период.
    /// </summary>
    public int? MonthDay { get; set; }

    /// <summary>
    /// Создаёт информацию о разделении периода на части.
    /// </summary>
    public PeriodSplit()
    {

    }
}
