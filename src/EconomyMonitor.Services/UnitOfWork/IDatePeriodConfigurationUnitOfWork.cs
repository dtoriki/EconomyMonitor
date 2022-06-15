using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.EfSets;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Mapping.AutoMapper.DatePeriodConfiguration;
using EconomyMonitor.Services.UnitOfWork.Implementations;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Services.UnitOfWork;

/// <summary>
/// Представляет тип для работы с хранилищем данных, которое реализует <see cref="IDatePeriodConfigurationSet{TDatePeriodConfigurationEntity}"/>.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public interface IDatePeriodConfigurationUnitOfWork 
{
    /// <summary>
    /// Асинхронно создаёт сущность конфигурации периода дат в хранилище данных.
    /// </summary>
    /// <typeparam name="TConfig">Тип конфигурации периода дат <see cref="IDatePeriodConfiguration"/>.</typeparam>
    /// <param name="configuration">Период дат.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="null"/>.
    /// </param>
    /// <returns>Тип конфигурации периода дат.</returns>
    Task<IDatePeriodConfiguration> CreateConfigurationAsync<TConfig>(TConfig configuration, CancellationToken cancellationToken = default)
        where TConfig : class, IDatePeriodConfiguration;

    /// <summary>
    /// Создаёт и возвращает реализацию <see cref="IDatePeriodConfigurationUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TRepository">
    /// Тип хранилища данных, которое реализует <see cref="IDatePeriodConfigurationSet{TDatePeriodConfigurationEntity}"/>.
    /// </typeparam>
    /// <param name="repository">Хранилище данных.</param>
    /// <param name="mapper">Экземпляр сопоставления сущностей с объектами передачи данных.</param>
    /// <returns>Реализация <see cref="IDatePeriodConfigurationUnitOfWork"/>.</returns>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="repository"/> или <paramref name="mapper"/>
    /// равны <see langword="null"/>.
    /// </exception>
    public static IDatePeriodConfigurationUnitOfWork Create<TRepository>(TRepository repository, IDatePeriodConfigurationMapper mapper)
        where TRepository : class, IRepository, IDatePeriodConfigurationSet<DatePeriodConfigurationEntity>
    {
        _ = ThrowIfArgumentNull(repository);
        _ = ThrowIfArgumentNull(mapper);

        return new DatePeriodConfigurationUnitOfWork<TRepository>(repository, mapper);
    }
}
