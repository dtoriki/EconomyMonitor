using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.EfSets;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Domain;
using EconomyMonitor.Mapping.AutoMapper.DatePeriodConfiguration;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Services.UnitOfWork.Implementations;

internal sealed class DatePeriodConfigurationUnitOfWork<TRepository> : IDatePeriodConfigurationUnitOfWork, IDisposable, IAsyncDisposable
    where TRepository : class, IRepository, IDatePeriodConfigurationSet<DatePeriodConfigurationEntity>
{
    private readonly TRepository _repository;
    private readonly IDatePeriodConfigurationMapper _mapper;

    private bool _isDisposed;

    public DatePeriodConfigurationUnitOfWork(TRepository repository, IDatePeriodConfigurationMapper mapper)
    {
        _ = ThrowIfArgumentNull(repository);
        _ = ThrowIfArgumentNull(mapper);

        _isDisposed = false;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IDatePeriodConfiguration> CreateConfigurationAsync<TConfig>(TConfig configuration, CancellationToken cancellationToken = default)
        where TConfig : class, IDatePeriodConfiguration
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        if (ThrowIfArgumentNull(configuration))
        {
            return configuration;
        }

        DatePeriodConfigurationEntity? periodEntity = _mapper.DatePeriodConfigurationMap<TConfig, DatePeriodConfigurationEntity>(configuration);

        _ = await _repository.CreateAsync(periodEntity, cancellationToken)
            .ConfigureAwait(false);

        TConfig result = _mapper.DatePeriodConfigurationMap<DatePeriodConfigurationEntity, TConfig>(periodEntity);

        return result;
    }

    //public async Task<IDatePeriodConfiguration?> GetDefaultConfigurationAsync(CancellationToken cancellationToken = default)
    //{
    //    if (_isDisposed)
    //    {
    //        ThrowDisposed(this);
    //    }

    //    DatePeriodConfigurationEntity? defaultConfigurationEntity = await _repository
    //        .ReadAll<DatePeriodConfigurationEntity>(x => x.IsDefault)
    //        .Include(x => x.SpendingQuotas)
    //        .Include(x => x.PeriodSplits)
    //        .SingleOrDefaultAsync(cancellationToken)
    //        .ConfigureAwait(false);

    //    return _mapper.DatePeriodConfigurationMap<DatePeriodConfigurationEntity, DatePeriodConfiguration>(defaultConfigurationEntity);
    //}

    public void Dispose()
    {
        if (!_isDisposed)
        {
            _repository.Dispose();
        }

        _isDisposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (!_isDisposed)
        {
            await _repository.DisposeAsync()
                .ConfigureAwait(false);
        }

        _isDisposed = true;
    }
}
