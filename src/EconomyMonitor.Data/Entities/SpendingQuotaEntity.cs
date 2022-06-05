using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Base;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Data.Abstracts.Interfaces.Refers;

namespace EconomyMonitor.Data.Entities;

/// <summary>
/// Сущность лимита трат.
/// </summary>
/// <remarks>
/// <para>
/// Этот тип нельзя унаследовать.
/// </para>
/// <para>
/// Наследует <see cref="EntityBase"/>.
/// </para>
/// <para>
/// Реализует <see cref="ISpendingQuotaEntity"/>.
/// </para>
/// </remarks>
public sealed class SpendingQuotaEntity : EntityBase, ISpendingQuotaEntity
{
    /// <summary>
    /// Возвращает или задаёт значение лимита трат.
    /// </summary>
    public decimal Quota { get; set; }

    /// <summary>
    /// Возвращает или задаёт уникальный идентификатор сущности конфигурации периода дат, 
    /// хранящейся в хранилище данных.
    /// </summary>
    public Guid? DatePeriodOptionId { get; set; }

    /// <summary>
    /// Возвращает или задаёт сущность кофигурации периода дат.
    /// </summary>
    public DatePeriodConfigurationEntity? DatePeriodOption { get; set; }

    Guid? IDatePeriodConfigurationRefered.DatePeriodOptionId => DatePeriodOptionId;

    IDatePeriodConfiguration? IDatePeriodConfigured.DatePeriodOption => DatePeriodOption;
}
