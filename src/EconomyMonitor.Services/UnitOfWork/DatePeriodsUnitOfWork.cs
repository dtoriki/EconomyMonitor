using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.EfSets;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Domain;
using EconomyMonitor.Mapping.AutoMapper;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Services.UnitOfWork;

internal sealed class DatePeriodsUnitOfWork<TRepository> : IDatePeriodsUnitOfWork, IDisposable, IAsyncDisposable
    where TRepository : class, IRepository, IDatePeriodSet
{
    private readonly TRepository _periodRepository;
    private readonly IEntityWithDtoMapper _mapper;

    private bool _isDisposed;

    public DatePeriodsUnitOfWork(TRepository periodRepository, IEntityWithDtoMapper mapper)
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

        _ = ThrowIfArgumentNull(period);

        DatePeriodEntity periodEntity = _mapper.Map<DatePeriodEntity>(period);

        _ = await _periodRepository.CreateAsync(periodEntity, cancellationToken)
            .ConfigureAwait(false);

        DatePeriod result = _mapper.Map<DatePeriod>(periodEntity);

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
