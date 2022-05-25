namespace EconomyMonitor.Abstacts;

/// <summary>
/// Defines a period properties.
/// </summary>
public interface IDatePeriod
{
    /// <summary>
    /// Gets or sets period starting date.
    /// </summary>
    DateOnly StartingDate { get; set; }

    /// <summary>
    /// Gets or sets period ending date.
    /// </summary>
    DateOnly EndingDate { get; set; }

    /// <summary>
    /// Gets or sets income value for period.
    /// </summary>
    decimal Income { get; set; }
}
