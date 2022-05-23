using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.DI;
using EconomyMonitor.Data.EfSets;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data;

/// <summary>
/// Defines Economy monitor repository.
/// </summary>
/// <remarks>Inherits <see cref="IRepository"/>, <see cref="IPeriodSet"/>.</remarks>
public interface IEconomyMonitorRepository : IPeriodSet, IRepository
{
    /// <summary>
    /// Creates <see cref="IEconomyMonitorRepository"/> implementation.
    /// </summary>
    /// <param name="options">Context opetions.</param>
    /// <returns><see cref="IEconomyMonitorRepository"/> implementation.</returns>
    public static IEconomyMonitorRepository Create(DbContextOptions options)
    {
        return new EconomyMonitorRepository(options);
    }

    /// <summary>
    /// Creates <see cref="IEconomyMonitorRepository"/> implementation by Sqlite data provider.
    /// </summary>
    /// <param name="connectionString">Connection string.</param>
    /// <returns><see cref="IEconomyMonitorRepository"/> implementation.</returns>
    public static IEconomyMonitorRepository CreateSqlite(string connectionString)
    {
        DbContextOptions<EconomyMonitorRepository> options = new DbContextOptionsBuilder<EconomyMonitorRepository>()
            .ConfigureSqliteDbContextOptionsBuilder(connectionString)
            .Options;

        return new EconomyMonitorRepository(options);
    }
}
