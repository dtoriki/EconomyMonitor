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
    /// <inheritdoc/>
    public Guid Id { get; }

    /// <inheritdoc/>
    public DateTime DateCreated { get; }

    /// <inheritdoc/>
    public DateTime DateModified { get; }
}
