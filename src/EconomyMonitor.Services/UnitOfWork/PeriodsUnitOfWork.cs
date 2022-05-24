using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.EfSets;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Domain;
using EconomyMonitor.Mapping.AutoMapper;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Services.UnitOfWork;

/// <summary>
/// Unit of work for periods.
/// </summary>
/// <typeparam name="TRepository">
/// Type of <see cref="IRepository"/>. Also have to inherit <seealso cref="IPeriodSet"/>.
/// </typeparam>
/// <remarks>
/// Implements: <see cref="IPeriodsUnitOfWork"/>, <see cref="IDisposable"/>, <see cref="IAsyncDisposable"/>.
/// </remarks>
/// <exception cref="ArgumentNullException"/>
/// <exception cref="ObjectDisposedException"/>
internal sealed class PeriodsUnitOfWork<TRepository> : IPeriodsUnitOfWork, IDisposable, IAsyncDisposable
    where TRepository : class, IRepository, IPeriodSet
{
    private readonly TRepository _periodRepository;
    private readonly IEntityWithDtoMapper _mapper;

    private bool _isDisposed;

    /// <summary>
    /// Creates unit of work for periods.
    /// </summary>
    /// <param name="periodRepository">
    /// <see cref="IRepository"/> instance. It have to impement <see cref="IPeriodSet"/>.
    /// </param>
    /// <param name="mapper">Mapper.</param>
    /// <exception cref="ArgumentNullException"/>
    public PeriodsUnitOfWork(TRepository periodRepository, IEntityWithDtoMapper mapper)
    {
        _ = ThrowIfArgumentNull(periodRepository);
        _ = ThrowIfArgumentNull(mapper);

        _isDisposed = false;
        _periodRepository = periodRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException"/>
    public async Task<IPeriod> CreatePeriodAsync<TPeriod>(TPeriod period, CancellationToken cancellationToken = default)
        where TPeriod : class, IPeriod
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        _ = ThrowIfArgumentNull(period);

        PeriodEntity periodEntity = _mapper.Map<PeriodEntity>(period);

        _ = await _periodRepository.CreateAsync(periodEntity, cancellationToken)
            .ConfigureAwait(false);

        Period result = _mapper.Map<Period>(periodEntity);

        return result;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (!_isDisposed)
        {
            _periodRepository.Dispose(); 
        }

        _isDisposed = true;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (!_isDisposed)
        {
            await _periodRepository.DisposeAsync()
                .ConfigureAwait(false); 
        }

        _isDisposed = true;
    }
}
