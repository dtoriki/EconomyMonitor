using EconomyMonitor.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data;

internal class SqliteEconomyMonitorRepositoryFactory : IDesignTimeDbContextFactory<EconomyMonitorRepository>
{
    public EconomyMonitorRepository CreateDbContext(string[] args)
    {
        string? connectionString = new ConfigurationBuilder()
            .ConfigureConfiguration()
            .Build()
            .GetConnectionString();

        if (ThrowIfNull(connectionString))
        {
            return null!;
        }

        return (EconomyMonitorRepository)IEconomyMonitorRepository.CreateSqlite(connectionString);
    }
}
