using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Base;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Primitives;

namespace EconomyMonitor.Data.Entities;

/// <summary>
/// Сущность, которая содержит информацию о разделении периода на части.
/// </summary>
/// <remarks>
/// <para>
/// От этого типа нельзя унаследоваться.
/// </para>
/// <para>
/// Наследует <see cref="EntityBase"/>.
/// </para>
/// Реализует <see cref="IPeriodSplitEntity"/>.
/// </remarks>
public sealed class PeriodSplitEntity : EntityBase, IPeriodSplitEntity
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
    /// Возвращает или задаёт уникальный идентификатор сущности конфигурации периода дат, 
    /// хранящейся в хранилище данных.
    /// </summary>
    public Guid? DatePeriodOptionId { get; set; }

    /// <summary>
    /// Возвращает или задаёт сущность кофигурации периода дат.
    /// </summary>
    public DatePeriodConfigurationEntity? DatePeriodOption { get; set; }

    IDatePeriodConfiguration? IDatePeriodConfigured.DatePeriodOption => DatePeriodOption;
}
