using System.Runtime.CompilerServices;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Extensions;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ArgsHelper;

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
    /// <summary>
    /// Creates EntityFramework repository.
    /// </summary>
    protected EfRepository() : base()
    {

    }

    /// <summary>
    /// Creates EntityFramework repository.
    /// </summary>
    /// <param name="options">Context options.</param>
    protected EfRepository(DbContextOptions options) : base(options)
    {

    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DbUpdateException"/>
    /// <exception cref="DbUpdateConcurrencyException"/>
    /// <exception cref="OperationCanceledException"/>
    public async Task<Guid> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        _ = ThrowIfNull(entity);

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
        _ = ThrowIfNull(entities);
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
        _ = ThrowIfNull(ids);

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
        _ = ThrowIfNull(entity);

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
        _ = ThrowIfNull(entities);
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
        _ = ThrowIfNull(ids);

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IEntity>()
            .HasIndex(x => x.Id)
            .IsUnique();

        modelBuilder.Entity<IEntity>()
            .Property(x => x.DateCreated)
            .IsRequired()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<IEntity>()
            .Property(x => x.DateModified)
            .IsRequired()
            .ValueGeneratedOnAddOrUpdate();

        base.OnModelCreating(modelBuilder);
    }

    private void DeleteBulkInternal<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IEntity
    {
        _ = ThrowIfNull(entities);
        _ = entities.ThrowIfAnyItemIsNull();

        TEntity[] entitiesArray = entities as TEntity[] ?? entities.ToArray();

        Set<TEntity>().RemoveRange(entitiesArray);
    }

    private void DeleteInternal<TEntity>(TEntity entity)
        where TEntity : class, IEntity
    {
        _ = ThrowIfNull(entity);

        Set<TEntity>().Remove(entity);
    }
}
