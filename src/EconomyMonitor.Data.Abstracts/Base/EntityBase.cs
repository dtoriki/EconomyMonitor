using EconomyMonitor.Data.Abstracts.Interfaces;

namespace EconomyMonitor.Data.Abstracts.Base;

/// <summary>
/// Presents base entity class.
/// </summary>
/// <remarks>
/// Immplemented <see cref="IEntity"/>.
/// </remarks>
public abstract class EntityBase : IEntity
{
    /// <summary>
    /// Gets or sets unique identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets creation date.
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets modification date.
    /// </summary>
    public DateTime DateModified { get; set; }
}
