using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.EfSets;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице "DatePeriods", 
/// хранящей сущности типа <see cref="DatePeriodEntity"/>.
/// </summary>
/// <remarks>
/// Наследует <see cref="IRepositorySet{TEntity}"/>, 
/// где тип сущности - <see cref="DatePeriodEntity"/>.
/// </remarks>
public interface IDatePeriodSet : IRepositorySet<IDatePeriodEntity>
{

    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных "DatePeriods", 
    /// способный формировать запросы к ней (таблице).
    /// </summary>
    public DbSet<DatePeriodEntity> DatePeriods { get; }
}
