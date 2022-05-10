using EconomyMonitor.Abstacts;

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
}
