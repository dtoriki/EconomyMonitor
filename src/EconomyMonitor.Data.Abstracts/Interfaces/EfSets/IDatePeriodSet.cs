using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.Abstracts.Interfaces.EfSets;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице "DatePeriods", 
/// хранящей сущности типа <see cref="IDatePeriodEntity"/>.
/// </summary>
/// <typeparam name="TDatePeriodEntity">
/// Тип, хранимой сущности, которая реализует <see cref="IDatePeriodEntity"/>.
/// </typeparam>
/// <remarks>
/// Наследует <see cref="IRepositorySet{TEntity}"/>, 
/// где тип сущности - <see cref="IDatePeriodEntity"/>.
/// </remarks>
public interface IDatePeriodSet<TDatePeriodEntity> : IRepositorySet<IDatePeriodEntity>
    where TDatePeriodEntity : class, IDatePeriodEntity
{
    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных "DatePeriods", 
    /// способный формировать запросы к ней (таблице).
    /// </summary>
    DbSet<TDatePeriodEntity> DatePeriods { get; }
}
