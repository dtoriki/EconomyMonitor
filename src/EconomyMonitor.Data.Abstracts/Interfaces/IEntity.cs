namespace EconomyMonitor.Data.Abstracts.Interfaces;

/// <summary>
/// Defines entity properties.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets unique identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets creation date.
    /// </summary>
    DateTime DateCreated { get; }

    /// <summary>
    /// Gets modification date.
    /// </summary>
    DateTime DateModified { get; }
}
