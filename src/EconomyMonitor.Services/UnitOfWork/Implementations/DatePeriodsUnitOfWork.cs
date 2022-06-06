using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.EfSets;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Mapping.AutoMapper.DatePeriod;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Services.UnitOfWork.Implementations;

internal sealed class DatePeriodsUnitOfWork<TRepository> : IDatePeriodsUnitOfWork, IDisposable, IAsyncDisposable
    where TRepository : class, IRepository, IDatePeriodSet<DatePeriodEntity>
{
    private readonly TRepository _periodRepository;
    private readonly IDatePeriodMapper _mapper;

    private bool _isDisposed;

    public DatePeriodsUnitOfWork(TRepository periodRepository, IDatePeriodMapper mapper)
    {
        _ = ThrowIfArgumentNull(periodRepository);
        _ = ThrowIfArgumentNull(mapper);

        _isDisposed = false;
        _periodRepository = periodRepository;
        _mapper = mapper;
    }

    public async Task<IDatePeriod> CreatePeriodAsync<TPeriod>(TPeriod period, CancellationToken cancellationToken = default)
        where TPeriod : class, IDatePeriod
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        if (ThrowIfArgumentNull(period))
        {
            return period;
        }

        DatePeriodEntity periodEntity = _mapper.DatePeriodMap<TPeriod, DatePeriodEntity>(period);

        _ = await _periodRepository.CreateAsync(periodEntity, cancellationToken)
            .ConfigureAwait(false);

        TPeriod result = _mapper.DatePeriodMap<DatePeriodEntity, TPeriod>(periodEntity);

        return result;
    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            _periodRepository.Dispose();
        }

        _isDisposed = true;
    }

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
