using EconomyMonitor.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data;

// ToDo: изменить название.
internal class SqliteEconomyMonitorRepositoryFactory : IDesignTimeDbContextFactory<AppRepository>
{
    public AppRepository CreateDbContext(string[] args)
    {
        string? connectionString = new ConfigurationBuilder()
            .SetupConfiguration()
            .Build()
            .GetConnectionString();

        if (ThrowIfNull(connectionString))
        {
            return null!;
        }

        return (AppRepository)IAppRepository.CreateSqlite(connectionString);
    }
}
