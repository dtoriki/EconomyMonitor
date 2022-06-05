using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.DI;
using EconomyMonitor.Data.EfSets;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data;

/// <summary>
/// Предоставляет тип хранилища данных приложения.
/// </summary>
/// <remarks>
/// Наследует 
/// <see cref="IDatePeriodSet"/>,
/// <see cref="IDataProtectionKeyContext"/>,
/// <see cref="IRepository"/>. 
/// </remarks>
public interface IAppRepository : 
    IDatePeriodSet,
    IDataProtectionKeyContext,
    IRepository
{
    /// <summary>
    /// Создаёт реализацию хранилища данных приложения <see cref="IAppRepository"/>.
    /// </summary>
    /// <param name="options">Настройки подключения к контексту базы данных.</param>
    /// <returns>Реализация хранилища данных приложения <see cref="IAppRepository"/>.</returns>
    public static IAppRepository Create(DbContextOptions options)
    {
        _ = ThrowIfArgumentNull(options);

        return new AppRepository(options);
    }

    /// <summary>
    /// Создаёт реализацию хранилища данных приложения <see cref="IAppRepository"/> 
    /// с использованием поставщика данных Sqlite.
    /// </summary>
    /// <param name="connectionString">Строка подключения.</param>
    /// <returns>
    /// Реализация хранилища данных приложения <see cref="IAppRepository"/>.
    /// </returns>
    /// <remarks>
    /// Для настройки подключения к хранилищу данных используется метод 
    /// <see cref="DbContextOptionsBuilderExtensions.ConfigureSqliteDbContextOptionsBuilder{TContext}(DbContextOptionsBuilder{TContext}, string)"/>
    /// </remarks>
    public static IAppRepository CreateSqlite(string connectionString)
    {
        _ = ThrowIfArgumentNull(connectionString);

        DbContextOptions<AppRepository> options = new DbContextOptionsBuilder<AppRepository>()
            .ConfigureSqliteDbContextOptionsBuilder(connectionString)
            .Options;

        return new AppRepository(options);
    }
}
