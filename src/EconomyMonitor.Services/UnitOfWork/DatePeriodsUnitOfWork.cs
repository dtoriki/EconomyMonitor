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
/// Type of <see cref="IRepository"/>. Also have to inherit <seealso cref="IDatePeriodSet"/>.
/// </typeparam>
/// <remarks>
/// Implements: <see cref="IDatePeriodsUnitOfWork"/>, <see cref="IDisposable"/>, <see cref="IAsyncDisposable"/>.
/// </remarks>
/// <exception cref="ArgumentNullException"/>
/// <exception cref="ObjectDisposedException"/>
internal sealed class DatePeriodsUnitOfWork<TRepository> : IDatePeriodsUnitOfWork, IDisposable, IAsyncDisposable
    where TRepository : class, IRepository, IDatePeriodSet
{
    private readonly TRepository _periodRepository;
    private readonly IEntityWithDtoMapper _mapper;

    private bool _isDisposed;

    /// <summary>
    /// Creates unit of work for periods.
    /// </summary>
    /// <param name="periodRepository">
    /// <see cref="IRepository"/> instance. It have to impement <see cref="IDatePeriodSet"/>.
    /// </param>
    /// <param name="mapper">Mapper.</param>
    /// <exception cref="ArgumentNullException"/>
    public DatePeriodsUnitOfWork(TRepository periodRepository, IEntityWithDtoMapper mapper)
    {
        _ = ThrowIfArgumentNull(periodRepository);
        _ = ThrowIfArgumentNull(mapper);

        _isDisposed = false;
        _periodRepository = periodRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException"/>
    public async Task<IDatePeriod> CreatePeriodAsync<TPeriod>(TPeriod period, CancellationToken cancellationToken = default)
        where TPeriod : class, IDatePeriod
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        _ = ThrowIfArgumentNull(period);

        DatePeriodEntity periodEntity = _mapper.Map<DatePeriodEntity>(period);

        _ = await _periodRepository.CreateAsync(periodEntity, cancellationToken)
            .ConfigureAwait(false);

        DatePeriod result = _mapper.Map<DatePeriod>(periodEntity);

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
