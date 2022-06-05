using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.EfSets;
using EconomyMonitor.Mapping.AutoMapper.DatePeriod;

namespace EconomyMonitor.Services.UnitOfWork;

/// <summary>
/// Представляет тип для работы с хранилищем данных, которое реализует <see cref="IDatePeriodSet"/>.
/// </summary>
public interface IDatePeriodsUnitOfWork
{
    /// <summary>
    /// Асинхронно создаёт сущность периода в хранилище данных.
    /// </summary>
    /// <typeparam name="TPeriod">Тип периода дат <see cref="IDatePeriod"/>.</typeparam>
    /// <param name="datePeriod">Период дат.</param>
    /// <param name="cancellationToken">
    /// Токен отмены операции. По-умолчанию - <see langword="null"/>.
    /// </param>
    /// <returns>Тип периода дат.</returns>
    Task<IDatePeriod> CreatePeriodAsync<TPeriod>(TPeriod datePeriod, CancellationToken cancellationToken = default)
        where TPeriod : class, IDatePeriod;

    /// <summary>
    /// Создаёт и возвращает реализацию <see cref="IDatePeriodsUnitOfWork"/>.
    /// </summary>
    /// <typeparam name="TRepository">
    /// Тип хранилища данных, которое реализует <see cref="IDatePeriodSet"/>.
    /// </typeparam>
    /// <param name="repository">Хранилище данных.</param>
    /// <param name="mapper">Экземпляр сопоставления сущностей с объектами передачи данных.</param>
    /// <returns>Реализация <see cref="IDatePeriodsUnitOfWork"/>.</returns>
    public static IDatePeriodsUnitOfWork Create<TRepository>(TRepository repository, IDatePeriodMapper mapper)
        where TRepository : class, IRepository, IDatePeriodSet
    {
        return new DatePeriodsUnitOfWork<TRepository>(repository, mapper);
    }
}
