namespace EconomyMonitor.Data.Abstracts.Interfaces;

/// <summary>
/// Defines repository set.
/// </summary>
/// <typeparam name="TEntity">Type of set items.</typeparam>
public interface IRepositorySet<out TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Gets entities set.
    /// </summary>
    IQueryable<TEntity> EntitySet { get; }
}
