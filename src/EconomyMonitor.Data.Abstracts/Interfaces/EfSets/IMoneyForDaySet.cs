using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.Abstracts.Interfaces.EfSets;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице "MoneyForDays", 
/// хранящей сущности типа <see cref="IMoneyForDayEntity"/>.
/// </summary>
/// <typeparam name="TFlow">
/// Тип, хранимой сущности, которая реализует <see cref="IMoneyForDayEntity"/>.
/// </typeparam>
/// <remarks>
/// Наследует <see cref="IRepositorySet{TEntity}"/>, 
/// где тип сущности - <see cref="IMoneyForDayEntity"/>.
/// </remarks>
public interface IMoneyForDaySet<TFlow> : IRepositorySet<IMoneyForDayEntity>
    where TFlow : class, IMoneyForDayEntity
{
    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных "MoneyFlowsForDays", 
    /// способный формировать запросы к ней (таблице).
    /// </summary>
    DbSet<TFlow> MoneyForDays { get; }
}
