using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.EfSets;
using EconomyMonitor.Mapping.AutoMapper;

namespace EconomyMonitor.Services.UnitOfWork;

/// <summary>
/// Defines unit of work for <see cref="IPeriod"/>.
/// </summary>
public interface IPeriodsUnitOfWork
{
    /// <summary>
    /// Asynchronusly creates period in internal repoestory.
    /// </summary>
    /// <typeparam name="TPeriod">Type of <see cref="IPeriod"/>.</typeparam>
    /// <param name="period">Period.</param>
    /// <param name="cancellationToken">Cancel token.</param>
    /// <returns>Returns back entry period.</returns>
    Task<IPeriod> CreatePeriodAsync<TPeriod>(TPeriod period, CancellationToken cancellationToken = default)
        where TPeriod : class, IPeriod;

    /// <summary>
    /// Creates <see cref="IPeriodsUnitOfWork"/> implementation.
    /// </summary>
    /// <typeparam name="TRepository">
    /// Type of <see cref="IRepository"/>. Also have to implement <see cref="IPeriodSet"/>.
    /// </typeparam>
    /// <param name="repository">Repository.</param>
    /// <param name="mapper">Mapper.</param>
    /// <returns><see cref="IPeriodsUnitOfWork"/> implementation.</returns>
    public static IPeriodsUnitOfWork Create<TRepository>(TRepository repository, IEntityWithDtoMapper mapper)
        where TRepository : class, IRepository, IPeriodSet
    {
        return new PeriodsUnitOfWork<TRepository>(repository, mapper);
    }
}
