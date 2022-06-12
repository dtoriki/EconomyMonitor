using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.Abstracts.Interfaces.EfSets;

/// <summary>
/// Представляет тип, поддерживающий запросы к таблице "SpendingQuotas", 
/// хранящей сущности типа <see cref="ISpendingQuotaEntity"/>.
/// </summary>
/// <typeparam name="TQuota">
/// Тип, хранимой сущности, которая реализует <see cref="ISpendingQuotaEntity"/>.
/// </typeparam>
/// <remarks>
/// Наследует <see cref="IRepositorySet{TEntity}"/>, 
/// где тип сущности - <see cref="ISpendingQuotaEntity"/>.
/// </remarks>
public interface ISpendingQuotaSet<TQuota> : IRepositorySet<ISpendingQuotaEntity>
    where TQuota : class, ISpendingQuotaEntity
{
    /// <summary>
    /// Возвращает экземпляр доступа к таблице базы данных "SpendingQuotas", 
    /// способный формировать запросы к ней (таблице).
    /// </summary>
    DbSet<TQuota> SpendingQuotas { get; }
}
