using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.Abstracts.Base.Repositories;

/// <summary>
/// Представляет хранилище данных на основе <see cref="DbContext"/>.
/// </summary>
/// <remarks>
/// Нследует <see cref="DbContext"/>.
/// Реализует <see cref="IRepository"/>.
/// </remarks>
/// <exception cref="ArgumentNullException"/>
/// <exception cref="DbUpdateException"/>
/// <exception cref="DbUpdateConcurrencyException"/>
/// <exception cref="OperationCanceledException"/>
/// <exception cref="ObjectDisposedException"/>
public abstract class EfRepository : DbContext, IRepository
{
    protected bool _isDisposed;

    /// <summary>
    /// Создаёт хранилище данных на основе <see cref="DbContext"/>.
    /// </summary>
    protected EfRepository() : base() => _isDisposed = false;

    /// <summary>
    /// Создаёт хранилище данных на основе <see cref="DbContext"/>.
    /// </summary>
    /// <param name="options">Настройки контекста базы данных.</param>
    protected EfRepository(DbContextOptions options) : base(options) => _isDisposed = false;

    /// <inheritdoc/>
    /// <remarks>
    /// После создания сущности <paramref name="entity"/>, 
    /// ей автоматически присваивается уникальный идентификатор <see cref="Guid"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="entity"/> оказался <see langword="null"/>.
    /// </exception>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public async Task<Guid> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        _ = ThrowIfArgumentNull(entity);

        await Set<TEntity>().AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        await SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return entity.Id;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// После создания сущностей <paramref name="entities"/>,
    /// им автоматически присваивается уникальный идентификатор <see cref="Guid"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="entities"/> оказался <see langword="null"/>.
    /// </exception>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public async Task CreateBulkAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        _ = ThrowIfArgumentNull(entities);
        _ = entities.ThrowIfAnyItemIsNull();

        TEntity[] entitiesArray = entities as TEntity[] ?? entities.ToArray();

        await Set<TEntity>()
            .AddRangeAsync(entitiesArray, cancellationToken)
            .ConfigureAwait(false);

        await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="entity"/> оказался <see langword="null"/>.
    /// </exception>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        DeleteInternal(entity);

        return SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="entities"/> оказался <see langword="null"/>.
    /// </exception>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public Task DeleteBulkAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        DeleteBulkInternal(entities);

        return SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
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
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="ids"/> оказался <see langword="null"/>.
    /// </exception>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
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
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public IQueryable<TEntity> ReadAll<TEntity>(Expression<Func<TEntity, bool>>? predicate = null)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        if (predicate is null)
        {
            return Set<TEntity>();
        }

        return Set<TEntity>().Where(predicate);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="entity"/> оказался <see langword="null"/>.
    /// </exception>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public async Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        await UpdateInternalAsync(entity, cancellationToken).ConfigureAwait(false);

        await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="entities"/> оказался <see langword="null"/>.
    /// </exception>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public async Task UpdateBulkAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        _ = ThrowIfArgumentNull(entities);
        _ = entities.ThrowIfAnyItemIsNull();

        TEntity[] entitiesArray = entities as TEntity[] ?? entities.ToArray();

        foreach(TEntity entity in entitiesArray)
        {
            await UpdateInternalAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        await SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public Task<TEntity?> FindAsync<TEntity>(
        Guid id,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        return Set<TEntity>()
            .FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken)
            .AsTask();
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="ids"/> оказался <see langword="null"/>.
    /// </exception>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Возникает, если операция была отменена: 
    /// был выполнен запрос на отмену операции через <paramref name="cancellationToken"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public async IAsyncEnumerable<TEntity> FindManyAsync<TEntity>(
        IEnumerable<Guid> ids,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
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

    /// <summary>
    /// Высвобождает ресурсы экземпляра.
    /// </summary>
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

    /// <summary>
    /// Асинхронно высвобождает ресурсы экземпляра.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Сохраняет все изменения, сделанные в этом контексте.
    /// </summary>
    /// <returns>Количество записей, записанных в репозиторий.</returns>
    /// <remarks>
    /// <para>
    /// Устанвливает значения 
    /// <see cref="IEntity.DateCreated"/> и <see cref="IEntity.DateModified"/> 
    /// в <see cref="DateTime.UtcNow"/>
    /// при записи новой сущности в репозиторий.
    /// </para>
    /// <para>
    /// Устанавливает значение 
    /// <see cref="IEntity.DateModified"/> 
    /// в <see cref="DateTime.UtcNow"/>
    /// при изменении сущности в репозитории.
    /// </para>
    /// </remarks>
    /// <exception cref="DbUpdateException">
    /// Возникает, если произошла ошибка при измении состояния хранилища данных.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Возникает, если при изменении состояния хранилища данных обнаружено нарушение параллелизма. 
    /// Нарушение параллелизма возникает, когда во время сохранения затрагивается неожиданное количество строк. 
    /// Обычно это происходит из-за того, что данные в хранилище данных были изменены после их загрузки в память.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Возникает, если текущий экземпляр был высвобожден.
    /// </exception>
    public override int SaveChanges()
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        SetDates();

        return base.SaveChanges();
    }

    /// <inheritdoc cref="SaveChanges"/>
    /// <param name="acceptAllChangesOnSuccess">
    /// Указывает, что <see cref="ChangeTracker.AcceptAllChanges"/> 
    /// должен вызваться после успешной отправки изменений в репозиторий.
    /// </param>
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        SetDates();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    
    /// <inheritdoc cref="SaveChangesAsync(CancellationToken)"/>
    /// <inheritdoc cref="SaveChanges(bool)"/>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        SetDates();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// Асинхронно охраняет все изменения, сделанные в этом контексте.
    /// </summary>
    /// <inheritdoc cref="SaveChanges"/>
    /// /// <param name="cancellationToken">Токен отмены операции.</param>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        SetDates();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetDates()
    {
        foreach(EntityEntry entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ((EntityBase)entry.Entity).DateCreated = DateTime.UtcNow;
                    ((EntityBase)entry.Entity).DateModified = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    ((EntityBase)entry.Entity).DateModified = DateTime.UtcNow;
                    break;

                default:
                    break;
            }
        }
    }

    private void DeleteBulkInternal<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
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
            ThrowDisposed(this);
        }

        _ = ThrowIfArgumentNull(entity);

        Set<TEntity>().Remove(entity);
    }

    private async Task UpdateInternalAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        _ = ThrowIfArgumentNull(entity);

        if (await FindAsync<TEntity>(entity.Id, cancellationToken).ConfigureAwait(false) is null)
        {
            return;
        }

        Set<TEntity>().Update(entity);
    }
}
