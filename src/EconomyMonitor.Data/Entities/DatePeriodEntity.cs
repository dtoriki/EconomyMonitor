using EconomyMonitor.Data.Abstracts.Base;

namespace EconomyMonitor.Data.Entities;

/// <summary>
/// Сущность периода дат в таблице базы данных.
/// </summary>
/// <remarks>
/// Наследует <see cref="EntityBase"/>.
/// Реализует <see cref="IDatePeriodEntity"/>.
/// </remarks>
public sealed class DatePeriodEntity : EntityBase, IDatePeriodEntity
{
    /// <inheritdoc/>
    public DateOnly StartingDate { get; set; }

    /// <inheritdoc/>
    public DateOnly EndingDate { get; set; }

    /// <inheritdoc/>
    public decimal Income { get; set; }

    /// <summary>
    /// Создаёт экземпляр сущности периода дат.
    /// </summary>
    public DatePeriodEntity()
    {

    }
}
