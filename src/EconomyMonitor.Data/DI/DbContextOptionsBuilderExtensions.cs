using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.DI;

/// <summary>
/// <see cref="DbContextOptionsBuilder"/> extensions.
/// </summary>
public static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Configures <paramref name="builder"/> by Sqlite provider.
    /// </summary>
    /// <param name="builder">Db context options builder.</param>
    /// <param name="connectionString">Connection string.</param>
    /// <returns>Db context options builder.</returns>
    public static DbContextOptionsBuilder ConfigureSqliteDbContextOptionsBuilder(
        this DbContextOptionsBuilder builder, 
        string connectionString)
    {
        _ = ThrowIfArgumentNull(connectionString);

        return builder
            .UseSqlite(
                connectionString,
                x => x.MigrationsAssembly("EconomyMonitor.Migrations"))
            .EnableThreadSafetyChecks();
    }

    /// <inheritdoc cref="ConfigureSqliteDbContextOptionsBuilder(DbContextOptionsBuilder, string)"/>
    /// <typeparam name="TContext">Type of <see cref="DbContext"/>.</typeparam>
    public static DbContextOptionsBuilder<TContext> ConfigureSqliteDbContextOptionsBuilder<TContext>(
        this DbContextOptionsBuilder<TContext> builder,
        string connectionString) where TContext : DbContext
    {
        _ = ThrowIfArgumentNull(connectionString);

        var commonBuilder = (DbContextOptionsBuilder)builder;

        return (DbContextOptionsBuilder<TContext>)commonBuilder
            .ConfigureSqliteDbContextOptionsBuilder(connectionString);
    }
}
