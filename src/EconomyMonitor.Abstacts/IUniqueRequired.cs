namespace EconomyMonitor.Abstacts;

/// <summary>
/// Defines unique identifier.
/// </summary>
public interface IUniqueRequired
{
    /// <summary>
    /// Gets unique identifier.
    /// </summary>
    Guid Id { get; }
}
