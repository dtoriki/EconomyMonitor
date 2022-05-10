using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.Abstracts.Interfaces;

/// <summary>
/// Defines entity properties.
/// </summary>
/// <remarks>Inherits <see cref="IUniqueRequired"/>.</remarks>
public interface IEntity : IUniqueRequired
{
    /// <summary>
    /// Gets creation date.
    /// </summary>
    DateTime DateCreated { get; }

    /// <summary>
    /// Gets modification date.
    /// </summary>
    DateTime DateModified { get; }
}
