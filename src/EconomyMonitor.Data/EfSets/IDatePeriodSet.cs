using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.EfSets;

/// <summary>
/// Defines the set of <see cref="DatePeriodEntity"/> as <see cref="DbSet{TEntity}"/>.
/// </summary>
/// <remarks>Inherits <see cref="IRepositorySet{TEntity}"/>.</remarks>
public interface IDatePeriodSet : IRepositorySet<IDatePeriodEntity>
{
    /// <summary>
    /// Gets the <see cref="DatePeriodEntity"/> set.
    /// </summary>
    public DbSet<DatePeriodEntity> DatePeriods { get; }
}
