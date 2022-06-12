using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.Abstracts.Interfaces.EfSets;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице "PeriodSplits", 
/// хранящей сущности типа <see cref="IPeriodSplitEntity"/>.
/// </summary>
/// <typeparam name="TSplit">
/// Тип, хранимой сущности, которая реализует <see cref="IPeriodSplitEntity"/>.
/// </typeparam>
/// <remarks>
/// Наследует <see cref="IRepositorySet{TEntity}"/>, 
/// где тип сущности - <see cref="IPeriodSplitEntity"/>.
/// </remarks>
public interface IPeriodSplitSet<TSplit> : IRepositorySet<IPeriodSplitEntity>
    where TSplit : class, IPeriodSplitEntity
{
    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных "PeriodSplits", 
    /// способный формировать запросы к ней (таблице).
    /// </summary>
    DbSet<TSplit> PeriodSplits { get; }
}
