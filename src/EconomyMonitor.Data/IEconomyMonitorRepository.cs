using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.EfSets;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data;

/// <summary>
/// Defines Economy monitor repository.
/// </summary>
/// <remarks>Inherits <see cref="IRepository"/>, <see cref="IPeriodSet"/>.</remarks>
public interface IEconomyMonitorRepository : IPeriodSet, IRepository
{
    public static IEconomyMonitorRepository Create(DbContextOptions options)
    {
        return new EconomyMonitorRepository(options);
    }
}
