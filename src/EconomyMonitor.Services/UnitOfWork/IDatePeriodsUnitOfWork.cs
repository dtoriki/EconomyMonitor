using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.EfSets;
using EconomyMonitor.Mapping.AutoMapper;

namespace EconomyMonitor.Services.UnitOfWork;

/// <summary>
/// Defines unit of work for <see cref="IDatePeriod"/>.
/// </summary>
public interface IDatePeriodsUnitOfWork
{
    /// <summary>
    /// Asynchronusly creates period in internal repoestory.
    /// </summary>
    /// <typeparam name="TPeriod">Type of <see cref="IDatePeriod"/>.</typeparam>
    /// <param name="period">Period.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    /// <returns>Returns back entry period.</returns>
    Task<IDatePeriod> CreatePeriodAsync<TPeriod>(TPeriod period, CancellationToken cancellationToken = default)
        where TPeriod : class, IDatePeriod;

    /// <summary>
    /// Creates <see cref="IDatePeriodsUnitOfWork"/> implementation.
    /// </summary>
    /// <typeparam name="TRepository">
    /// Type of <see cref="IRepository"/>. Also have to implement <see cref="IDatePeriodSet"/>.
    /// </typeparam>
    /// <param name="repository">Repository.</param>
    /// <param name="mapper">Mapper.</param>
    /// <returns><see cref="IDatePeriodsUnitOfWork"/> implementation.</returns>
    public static IDatePeriodsUnitOfWork Create<TRepository>(TRepository repository, IEntityWithDtoMapper mapper)
        where TRepository : class, IRepository, IDatePeriodSet
    {
        return new DatePeriodsUnitOfWork<TRepository>(repository, mapper);
    }
}
