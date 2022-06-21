using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.Abstracts.Interfaces.EfSets;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице "Settings", 
/// хранящей сущности типа <see cref="ISettingsEntity"/>.
/// </summary>
/// <typeparam name="TSettings">
/// Тип, хранимой сущности, которая реализует <see cref="ISettingsEntity"/>.
/// </typeparam>
/// <remarks>
/// Наследует <see cref="IRepositorySet{TEntity}"/>, 
/// где тип сущности - <see cref="ISettingsEntity"/>.
/// </remarks>
public interface ISettingsSet<TSettings> : IRepositorySet<ISettingsEntity>
    where TSettings : class, ISettingsEntity
{
    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных "MoneyFlowsForDays", 
    /// способный формировать запросы к ней (таблице).
    /// </summary>
    DbSet<TSettings> Settings { get; }
}
