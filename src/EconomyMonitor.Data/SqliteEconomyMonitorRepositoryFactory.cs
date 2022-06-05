using EconomyMonitor.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data;

// ToDo: изменить название.
internal class SqliteEconomyMonitorRepositoryFactory : IDesignTimeDbContextFactory<EconomyMonitorRepository>
{
    public EconomyMonitorRepository CreateDbContext(string[] args)
    {
        string? connectionString = new ConfigurationBuilder()
            .SetupConfiguration()
            .Build()
            .GetConnectionString();

        if (ThrowIfNull(connectionString))
        {
            return null!;
        }

        return (EconomyMonitorRepository)IEconomyMonitorRepository.CreateSqlite(connectionString);
    }
}
