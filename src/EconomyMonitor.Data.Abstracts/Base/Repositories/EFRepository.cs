using System.Runtime.CompilerServices;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Extensions;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;
using static EconomyMonitor.Literals.ExceptionMessages;

namespace EconomyMonitor.Data.Abstracts.Base.Repositories;

/// <summary>
/// Defines base EntityFramework repository.
/// </summary>
/// <remarks>Implements <see cref="IRepository"/>.</remarks>
/// <exception cref="ArgumentNullException"/>
/// <exception cref="DbUpdateException"/>
/// <exception cref="DbUpdateConcurrencyException"/>
/// <exception cref="OperationCanceledException"/>
public abstract class EfRepository : DbContext, IRepository
{
    protected bool _isDisposed;

    /// <summary>
    /// Creates EntityFramework repository.
    /// </summary>
    protected EfRepository() : base()
    {
        _isDisposed = false;
    }

    /// <summary>
    /// Creates EntityFramework repository.
    /// </summary>
    /// <param name="options">Context options.</param>
    protected EfRepository(DbContextOptions options) : base(options)
    {
        _isDisposed = false;
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public async Task<Guid> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        _ = ThrowIfArgumentNull(entity);

        await Set<TEntity>().AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        await SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return entity.Id;
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public async Task CreateBulkAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        _ = ThrowIfArgumentNull(entities);
        _ = entities.ThrowIfAnyItemIsNull();

        TEntity[] entitiesArray = entities as TEntity[] ?? entities.ToArray();

        await Set<TEntity>().AddRangeAsync(entitiesArray, cancellationToken)
            .ConfigureAwait(false);

        await SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }


    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        DeleteInternal(entity);

        return SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public Task DeleteBulkAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        DeleteBulkInternal(entities);

        return SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public async Task DeleteByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        TEntity? entity = await FindAsync<TEntity>(id, cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return;
        }

        DeleteInternal(entity);

        await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public async Task DeleteByIdBulkAsync<TEntity>(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        _ = ThrowIfArgumentNull(ids);

        Guid[] idsArray = ids as Guid[] ?? ids.ToArray();

        IAsyncEnumerable<TEntity> entities = FindManyAsync<TEntity>(idsArray, cancellationToken);

        await foreach (TEntity entity in entities)
        {
            DeleteInternal(entity);
        }

        await SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public IQueryable<TEntity> ReadAll<TEntity>(Func<TEntity, bool>? predicate = null, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        if (predicate is null)
        {
            return Set<TEntity>();
        }

        return Set<TEntity>().Where(predicate).AsQueryable();
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        _ = ThrowIfArgumentNull(entity);

        Set<TEntity>().Update(entity);

        return SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public Task UpdateBulkAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        _ = ThrowIfArgumentNull(entities);
        _ = entities.ThrowIfAnyItemIsNull();

        TEntity[] entitiesArray = entities as TEntity[] ?? entities.ToArray();

        Set<TEntity>().UpdateRange(entitiesArray);

        return SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public async Task<TEntity?> FindAsync<TEntity>(
        Guid id,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        TEntity? entity = await Set<TEntity>()
            .FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return entity;
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public async IAsyncEnumerable<TEntity> FindManyAsync<TEntity>(
        IEnumerable<Guid> ids,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        _ = ThrowIfArgumentNull(ids);

        Guid[] idsArray = ids as Guid[] ?? ids.ToArray();

        foreach (Guid id in idsArray)
        {
            TEntity? entity = await FindAsync<TEntity>(id, cancellationToken).ConfigureAwait(false);

            if (entity is not null)
            {
                yield return entity;
            }
        }
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        base.Dispose();
        GC.SuppressFinalize(this);
        _isDisposed = true;
    }

    /// <inheritdoc/>
    public override async ValueTask DisposeAsync()
    {
        if (_isDisposed)
        {
            return;
        }

        await base.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
        _isDisposed = true;
    }

    private void DeleteBulkInternal<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        _ = ThrowIfArgumentNull(entities);
        _ = entities.ThrowIfAnyItemIsNull();

        TEntity[] entitiesArray = entities as TEntity[] ?? entities.ToArray();

        Set<TEntity>().RemoveRange(entitiesArray);
    }

    private void DeleteInternal<TEntity>(TEntity entity)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            Throw<ObjectDisposedException>(OBJECT_DISPOSED);
        }

        _ = ThrowIfArgumentNull(entity);

        Set<TEntity>().Remove(entity);
    }
}
