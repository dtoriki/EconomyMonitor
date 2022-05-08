namespace EconomyMonitor.Abstacts;

/// <summary>
/// Defines a period properties.
/// </summary>
public interface IPeriod
{
    /// <summary>
    /// Gets or sets period starting date.
    /// </summary>
    DateOnly StartPeriod { get; set; }

    /// <summary>
    /// Gets or sets period ending date.
    /// </summary>
    DateOnly EndPeriod { get; set; }

    /// <summary>
    /// Gets or sets income value for period.
    /// </summary>
    decimal Income { get; set; }
}
