using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Base;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Primitives.Enums;

namespace EconomyMonitor.Data.Entities;

/// <summary>
/// Сущность конфигурации периодов дат.
/// </summary>
/// <remarks>
/// <para>
/// От этого типа нельзя унаследоваться.
/// </para>
/// <para>
/// Наследует <see cref="EntityBase"/>.
/// </para>
/// <para>
/// Реализует <see cref="IDatePeriodConfigurationEntity"/>.
/// </para>
/// </remarks>
public sealed class DatePeriodConfigurationEntity : EntityBase, IDatePeriodConfigurationEntity
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
    /// Возвращает или задаёт коллекцию дней, разделяющих период.
    /// </summary>
    public ICollection<PeriodSplitEntity> PeriodSplits { get; set; }

    /// <summary>
    /// Возвращает или задаёт коллекцию лимитов трат.
    /// </summary>
    public ICollection<SpendingQuotaEntity> SpendingQuotas { get; set; }

    IEnumerable<IMonthDay> IDatePeriodConfiguration.PeriodSplits => PeriodSplits;

    IEnumerable<ISpendingQuota> IDatePeriodConfiguration.SpendingQuotas => SpendingQuotas;

    /// <summary>
    /// Создаёт сущность конфигурации периодов дат.
    /// </summary>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public DatePeriodConfigurationEntity()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    {

    }
}
