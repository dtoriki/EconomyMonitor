using EconomyMonitor.Data.Abstracts.Base;

namespace EconomyMonitor.Data.Entities;

/// <summary>
/// Presents period entity.
/// </summary>
/// <remarks>
/// Inherits <see cref="EntityBase"/>.
/// Implements <see cref="IDatePeriodEntity"/>.
/// </remarks>
public sealed class DatePeriodEntity : EntityBase, IDatePeriodEntity
{
    /// <inheritdoc/>
    public DateOnly StartingDate { get; set; }

    /// <inheritdoc/>
    public DateOnly EndingDate { get; set; }

    /// <inheritdoc/>
    public decimal Income { get; set; }
}
