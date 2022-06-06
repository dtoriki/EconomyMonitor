namespace EconomyMonitor.Data.Abstracts.Interfaces;

/// <summary>
/// Представляет тип репозитория.
/// </summary>
/// <remarks>
/// Наследует: <see cref="IDisposable"/>, <see cref="IAsyncDisposable"/>.
/// </remarks>
public interface IRepository : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Асинхронно создаёт сущность в хранилище данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип создаваемой сущности.</typeparam>
    /// <param name="entity">Создаваемая сущность.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    /// <returns>Присвоенный уникальный идентификатор сущности <see cref="Guid"/>.</returns>
    Task<Guid> CreateAsync<TEntity>(
        TEntity entity, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхронно создаёт множество сущностей в хранилище данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип создаваемых сущностей.</typeparam>
    /// <param name="entities">Перечислеие создаваемых сущностей.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    Task CreateBulkAsync<TEntity>(
        IEnumerable<TEntity> entities, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Формирует и возвращает запрос на чтение сущностей из хранилища данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип сущностей запроса на чтение.</typeparam>
    /// <param name="predicate">
    /// Условия выборки сущностей. По-умолчанию - <see langword="null"/>
    /// </param>
    /// <returns><see cref="IQueryable{T}"/> запрос на чтение сущностей из хранилища данных.</returns>
    IQueryable<TEntity> ReadAll<TEntity>(
        Func<TEntity, bool>? predicate = null) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхронно обновляет сущность в хранилище данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип обновляемой сущности.</typeparam>
    /// <param name="entity">Обновляемая сущность.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    Task UpdateAsync<TEntity>(
        TEntity entity, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхронно обновляет множество сущностей в хранилище данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип обновляемых сущностей.</typeparam>
    /// <param name="entities">Перечисление обновляемых сущностей.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    Task UpdateBulkAsync<TEntity> (
        IEnumerable<TEntity> entities, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхронно удаляет сущность из хранилища данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип удаляемой сущности.</typeparam>
    /// <param name="entity">Удаляемая сущность.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    Task DeleteAsync<TEntity> (
        TEntity entity, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхронно удаляет множество сущностей из хранилища данных.
    /// </summary>
    /// <typeparam name="TEntity">Тип удаляемых сущностей.</typeparam>
    /// <param name="entities">Перечисление удаляемых сущностей.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    Task DeleteBulkAsync<TEntity>(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхронно удаляет сущность из хранилища данных по её уникальному идентификатору <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TEntity">Тип удаляемой сущности.</typeparam>
    /// <param name="id">Уникальный идентификатор удаляемой сущности.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    Task DeleteByIdAsync<TEntity> (
        Guid id, 
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхронно удаляет множество сущностей из хранилища данных 
    /// по их уникальному идентификатору <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TEntity">Тип удаляемых сущностей.</typeparam>
    /// <param name="ids">Уникальные идентфикаторы удаляемых сущностей.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    Task DeleteByIdBulkAsync<TEntity>(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхроно ищет в харинилище данных сущность по уникальному идентификатору <see cref="Guid"/>
    /// ивозвращает её, если находит. Если  найти не удалось, то возвращает <see langword="null"/>.
    /// </summary>
    /// <typeparam name="TEntity">Тип искомой сущности.</typeparam>
    /// <param name="id">Уникальный идентификатор искомой сущности.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    /// <returns>Найденная сущность. Если найти не удалось - <see langword="null"/>.</returns>
    Task<TEntity?> FindAsync<TEntity>(
        Guid id,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

    /// <summary>
    /// Асинхроно ищет в харинилище данных сущности по их уникальномым идентификаторам <see cref="Guid"/>
    /// и возвращает асинхронное перечисение этих сущностей.
    /// </summary>
    /// <typeparam name="TEntity">Тип искомых сущностей.</typeparam>
    /// <param name="ids">Перечисление уникальных идентификаторов искомых сущностей.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="default"/>.
    /// </param>
    /// <returns>
    /// Асинхронное перечисление искомых сущностей. 
    /// Может быть пустым, если не удалось найти ни одной сущности.
    /// </returns>
    IAsyncEnumerable<TEntity> FindManyAsync<TEntity>(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default) where TEntity : class, IEntity;

}
