using EconomyMonitor.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data;

internal class AppRepositorySqliteFactory : IDesignTimeDbContextFactory<AppRepository>
{
    public AppRepository CreateDbContext(string[] args)
    {
        string? connectionString = new ConfigurationBuilder()
            .SetupConfiguration()
            .Build()
            .GetSqliteConnectionString();

        if (ThrowIfNull(connectionString))
        {
            return null!;
        }

        return (AppRepository)IAppRepository.CreateSqlite(connectionString);
    }
}
