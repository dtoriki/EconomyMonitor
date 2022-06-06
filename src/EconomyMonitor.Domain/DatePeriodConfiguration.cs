using EconomyMonitor.Abstacts;
using EconomyMonitor.Primitives.Enums;

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
    /// Возвращает или задаёт тип дня месяца, с начала которого заканчивается старый и начинается новый периоды.
    /// </summary>
    public MonthDayType MonthDayType { get; set; }

    /// <summary>
    /// Возвращает или задаёт день месяца, с начала которого заканчивается старый и начинается новый периоды.
    /// </summary>
    public int? MonthDay { get; set; }

    /// <summary>
    /// Возвращает или задёт признак того, что данная конфигурация используется по-умолчанию: 
    /// <see langword="true"/>, если текущий вариант конфигурации
    /// является конфигурацией по-умолчанию, иначе - <see langword="false"/>.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// Возвращает или задаёт перечисление дней, разделяющих период.
    /// </summary>
    public IEnumerable<IMonthDay> PeriodSplits { get; }

    /// <summary>
    /// Возвращает или задаёт перечисление лимитов трат.
    /// </summary>
    public IEnumerable<ISpendingQuota> SpendingQuotas { get; }

    /// <summary>
    /// Создаёт конфигурацию периодов дат.
    /// </summary>
    public DatePeriodConfiguration()
    {
        PeriodSplits = new List<IMonthDay>();
        SpendingQuotas = new List<ISpendingQuota>();
    }
}
