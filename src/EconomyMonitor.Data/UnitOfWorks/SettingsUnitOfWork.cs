using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.EfSets;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Data.Mappers;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.UnitOfWorks;

internal sealed class SettingsUnitOfWork<TRepository> : ISettingsUnitOfWork, IDisposable, IAsyncDisposable
    where TRepository : IRepository, ISettingsSet<SettingsEntity>
{
    private readonly TRepository _repository;
    private readonly ISettingsMapper _settingsMapper;

    private bool _disposed;

    public SettingsUnitOfWork(TRepository repository, ISettingsMapper settingsMapper)
    {
        _ = ThrowIfArgumentNull(repository);
        _ = ThrowIfArgumentNull(settingsMapper);

        _repository = repository;
        _settingsMapper = settingsMapper;
    }

    public async Task<ISettingsEntity?> GetSettingsAsync(CancellationToken cancellationToken = default)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        SettingsEntity? settingsEntity = await GetSettingsAsync(withTracking: true, cancellationToken)
            .ConfigureAwait(false);

        return settingsEntity;
    }

    public async Task<ISettingsEntity?> GetSettingsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        SettingsEntity? settingsEntity = await GetSettingsAsync(withTracking: false, cancellationToken)
            .ConfigureAwait(false);

        return settingsEntity;
    }

    public async Task SaveSettingsAsync(ISettings settings, CancellationToken cancellationToken = default)
    {
        if (ThrowIfArgumentNull(settings))
        {
            return;
        }

        SettingsEntity? settingsEntity = await GetSettingsAsync(withTracking: true, cancellationToken)
            .ConfigureAwait(false);

        if (settingsEntity is null)
        {
            SettingsEntity newEntity = _settingsMapper.Map<SettingsEntity>(settings);

            await _repository.CreateAsync(newEntity, cancellationToken)
                .ConfigureAwait(false);

            return;
        }

        SettingsEntity updatedEntity = _settingsMapper.Pour(settings, settingsEntity, s => new { s.StartingBudget });
        await _repository.UpdateAsync(updatedEntity, cancellationToken).ConfigureAwait(false);
    }

    private Task<SettingsEntity?> GetSettingsAsync(bool withTracking, CancellationToken cancellationToken)
    {
        IQueryable<SettingsEntity> query = withTracking
            ? ReadAllSettingsInternal()
            : ReadAllSettingsInternalNoTracking();

        return query.SingleOrDefaultAsync(cancellationToken);
    }

    private IQueryable<SettingsEntity> ReadAllSettingsInternalNoTracking()
    {
        return ReadAllSettingsInternal().AsNoTracking();
    }

    private IQueryable<SettingsEntity> ReadAllSettingsInternal()
    {
        return _repository
            .ReadAll<SettingsEntity>();
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _repository.Dispose();
        GC.SuppressFinalize(this);

        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        await _repository.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);

        _disposed = true;
    }
}
