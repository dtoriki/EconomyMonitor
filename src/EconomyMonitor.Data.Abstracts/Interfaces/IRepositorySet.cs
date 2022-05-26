namespace EconomyMonitor.Data.Abstracts.Interfaces;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице репозитория, 
/// хранящей сущности типа <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">Тип сущностей, хранящихся в таблице.</typeparam>
public interface IRepositorySet<out TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных, способный формировать запросы к ней (таблице). 
    /// </summary>
    IQueryable<TEntity> EntitySet { get; }
}
