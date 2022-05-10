using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data.EfSets;

/// <summary>
/// Defines the set of <see cref="PeriodEntity"/> as <see cref="DbSet{TEntity}"/>.
/// </summary>
/// <remarks>Inherits <see cref="IRepositorySet{TEntity}"/>.</remarks>
public interface IPeriodSet : IRepositorySet<IPeriodEntity>
{
    /// <summary>
    /// Gets the <see cref="PeriodEntity"/> set.
    /// </summary>
    public DbSet<PeriodEntity> Periods { get; }
}
