using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.DI;

/// <summary>
/// Содержит методы расширения для конфигурации <see cref="DbContextOptionsBuilder"/>.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Конфигурирует конструктор параметров подключения к контексту базы данных <paramref name="builder"/>, используя поставщик данных Sqlite.
    /// </summary>
    /// <param name="builder">Конструктор параметров подключения к контексту базы данных.</param>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    /// <returns>Конструктор параметров подключения к контексту базы данных.</returns>
    /// <remarks>
    /// <para>
    /// Методом <see cref="RelationalDbContextOptionsBuilder{TBuilder, TExtension}.MigrationsAssembly(string?)"/>
    /// устанавливает EconomyMonitor.Migrations сборкой для миграций.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="connectionString"/> - <see langword="null"/>.
    /// </exception>
    public static DbContextOptionsBuilder ConfigureSqliteDbContextOptionsBuilder(
        this DbContextOptionsBuilder builder, 
        string connectionString)
    {
        _ = ThrowIfArgumentNull(connectionString);

        return builder
            .UseSqlite(
                connectionString,
                x => x.MigrationsAssembly("EconomyMonitor.Migrations"));
    }

    /// <inheritdoc cref="ConfigureSqliteDbContextOptionsBuilder(DbContextOptionsBuilder, string)"/>
    /// <typeparam name="TContext">Тип контекста базы данных <see cref="DbContext"/>.</typeparam>
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
