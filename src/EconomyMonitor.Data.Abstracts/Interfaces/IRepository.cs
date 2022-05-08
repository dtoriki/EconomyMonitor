namespace EconomyMonitor.Data.Abstracts.Interfaces;

/// <summary>
/// Defines data repository.
/// </summary>
/// <remarks>
/// Inherits: <see cref="IDisposable"/>, <see cref="IAsyncDisposable"/>.
/// </remarks>
public interface IRepository : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Asynchronously creates new entity in repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="entity">Entity.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    /// <returns>Unique <see cref="Guid"/> of the created entity.</returns>
    Task<Guid> CreateAsync<TEntity>(
        TEntity entity, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously creates new many entities in repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entities.</typeparam>
    /// <param name="entity">Entities.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    Task CreateBulkAsync<TEntity>(
        IEnumerable<TEntity> entities, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Read entities from repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entities.</typeparam>
    /// <param name="predicate">Predicate.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    /// <returns><see cref="IQueryable{T}"/> sequence of readed entities.</returns>
    IQueryable<TEntity> ReadAll<TEntity>(
        Func<TEntity, bool>? predicate = null, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously updates entity in repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="entity">Entity.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    Task UpdateAsync<TEntity>(
        TEntity entity, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously updates many entities in repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entities.</typeparam>
    /// <param name="entities">Entities.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    Task UpdateBulkAsync<TEntity> (
        IEnumerable<TEntity> entities, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously delete entity from repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="entity">Entity.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    Task DeleteAsync<TEntity> (
        TEntity entity, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously deletes many entities from repository.
    /// </summary>
    /// <typeparam name="TEntity">Type of entities.</typeparam>
    /// <param name="entities">Entities.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    Task DeleteBulkAsync<TEntity>(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously delete entity from repository by id as <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="id">Unique id.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    Task DeleteByIdAsync<TEntity> (
        Guid id, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously deletes many entities from repository by ids as <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entities.</typeparam>
    /// <param name="ids">Unique ids.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    Task DeleteByIdBulkAsync<TEntity>(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously finds entity in repository by id as <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    /// <param name="id">Unique id.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    /// <returns>Founded entity, if it exists, otherwise - <see langword="null"/>.</returns>
    Task<TEntity?> FindAsync<TEntity>(
        Guid id,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Asynchronously finds many entities in repository by ids as <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entities.</typeparam>
    /// <param name="ids">Unique ids.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    /// <returns>Asynchronous sequence of entites. May be empty if was found nothing.</returns>
    IAsyncEnumerable<TEntity?> FindManyAsync<TEntity>(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

}
