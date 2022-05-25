using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Domain;

/// <summary>
/// Present period data.
/// </summary>
public sealed class DatePeriod : IDatePeriod, IUniqueRequired
{
    /// <summary>
    /// Gets or sets unique identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <inheritdoc/>
    public DateOnly StartingDate { get; set; }

    /// <inheritdoc/>
    public DateOnly EndingDate { get; set; }

    /// <inheritdoc/>
    public decimal Income { get; set; }
}
