using EconomyMonitor.Data.Abstracts.Base;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;

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
    /// Возвращает или задаёт дату начала периода включительно.
    /// </summary>
    public DateOnly StartPeriodDateInclusive { get; set; }

    /// <summary>
    /// Возвращает или задаёт дату окончания периода исключительно.
    /// </summary>
    public DateOnly EndPeriodDateExclusive { get; set; }

    /// <summary>
    /// Создаёт сущность конфигурации периодов дат.
    /// </summary>
    public DatePeriodConfigurationEntity()
    {

    }
}
