using EconomyMonitor.Data.Abstracts.Base;

namespace EconomyMonitor.Data.Entities;

/// <summary>
/// Presents period entity.
/// </summary>
/// <remarks>
/// Inherits <see cref="EntityBase"/>.
/// Implements <see cref="IPeriodEntity"/>.
/// </remarks>
public sealed class PeriodEntity : EntityBase, IPeriodEntity
{
    /// <inheritdoc/>
    public DateOnly StartPeriod { get; set; }

    /// <inheritdoc/>
    public DateOnly EndPeriod { get; set; }

    /// <inheritdoc/>
    public decimal Income { get; set; }
}
