using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Domain;

/// <summary>
/// Present period data.
/// </summary>
public sealed class Period : IPeriod, IUniqueRequired
{
    /// <summary>
    /// Gets or sets unique identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <inheritdoc/>
    public DateOnly StartPeriod { get; set; }

    /// <inheritdoc/>
    public DateOnly EndPeriod { get; set; }

    /// <inheritdoc/>
    public decimal Income { get; set; }
}
