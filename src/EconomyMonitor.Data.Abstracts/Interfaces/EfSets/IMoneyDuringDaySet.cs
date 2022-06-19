using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.Abstracts.Interfaces.EfSets;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице "MoneyDuringDays", 
/// хранящей сущности типа <see cref="IMoneyDuringDayEntity"/>.
/// </summary>
/// <typeparam name="TFlow">
/// Тип, хранимой сущности, которая реализует <see cref="IMoneyDuringDayEntity"/>.
/// </typeparam>
/// <remarks>
/// Наследует <see cref="IRepositorySet{TEntity}"/>, 
/// где тип сущности - <see cref="IMoneyDuringDayEntity"/>.
/// </remarks>
public interface IMoneyDuringDaySet<TFlow> : IRepositorySet<IMoneyDuringDayEntity>
    where TFlow : class, IMoneyDuringDayEntity
{
    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных "MoneyFlowsForDays", 
    /// способный формировать запросы к ней (таблице).
    /// </summary>
    DbSet<TFlow> MoneyDuringDays { get; }
}
