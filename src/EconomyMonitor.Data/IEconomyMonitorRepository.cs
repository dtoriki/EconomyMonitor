using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.DI;
using EconomyMonitor.Data.EfSets;
using Microsoft.EntityFrameworkCore;

namespace EconomyMonitor.Data;

/// <summary>
/// Предоставляет тип хранилища данных приложения.
/// </summary>
/// <remarks>Наследует: <see cref="IRepository"/>, <see cref="IDatePeriodSet"/>.</remarks>
public interface IEconomyMonitorRepository : IDatePeriodSet, IRepository
// ToDo: изменить название.
{
    /// <summary>
    /// Создаёт реализацию хранилища данных приложения <see cref="IEconomyMonitorRepository"/>.
    /// </summary>
    /// <param name="options">Настройки подключения к контексту базы данных.</param>
    /// <returns>Реализация хранилища данных приложения <see cref="IEconomyMonitorRepository"/>.</returns>
    public static IEconomyMonitorRepository Create(DbContextOptions options)
    {
        // ToDo: не хватает проверки на null.
        return new EconomyMonitorRepository(options);
    }

    /// <summary>
    /// Создаёт реализацию хранилища данных приложения <see cref="IEconomyMonitorRepository"/> 
    /// с использованием поставщика данных Sqlite.
    /// </summary>
    /// <param name="connectionString">Строка подключения.</param>
    /// <returns>
    /// Реализация хранилища данных приложения <see cref="IEconomyMonitorRepository"/>.
    /// </returns>
    /// <remarks>
    /// Для настройки подключения к хранилищу данных используется метод 
    /// <see cref="DbContextOptionsBuilderExtensions.ConfigureSqliteDbContextOptionsBuilder{TContext}(DbContextOptionsBuilder{TContext}, string)"/>
    /// </remarks>
    public static IEconomyMonitorRepository CreateSqlite(string connectionString)
    {
        // ToDo: не хватает проверки на null.
        DbContextOptions<EconomyMonitorRepository> options = new DbContextOptionsBuilder<EconomyMonitorRepository>()
            .ConfigureSqliteDbContextOptionsBuilder(connectionString)
            .Options;

        return new EconomyMonitorRepository(options);
    }
}
