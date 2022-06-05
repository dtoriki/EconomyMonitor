using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.Abstracts.Interfaces.EfSets;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице "DatePeriodConfigurations", 
/// хранящей сущности типа <see cref="IDatePeriodConfigurationEntity"/>.
/// </summary>
/// <typeparam name="TDatePeriodConfigurationEntity">
/// Тип, хранимой сущности, которая реализует <see cref="IDatePeriodConfigurationEntity"/>.
/// </typeparam>
/// <remarks>
/// Наследует <see cref="IRepositorySet{TEntity}"/>, 
/// где тип сущности - <see cref="IDatePeriodConfigurationEntity"/>.
/// </remarks>
public interface IDatePeriodConfigurationSet<TDatePeriodConfigurationEntity> : IRepositorySet<IDatePeriodConfigurationEntity>
    where TDatePeriodConfigurationEntity : class, IDatePeriodConfigurationEntity
{
    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных "DatePeriodConfigurations", 
    /// способный формировать запросы к ней (таблице).
    /// </summary>
    DbSet<TDatePeriodConfigurationEntity> DatePeriodConfigurations { get; }
}
