using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.EfSets;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Data.Mappers;
using EconomyMonitor.Primitives.Comparers;
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

        SettingsEntity? settingsEntity = await GetSettingsAsync(withTracking: true, cancellationToken);

        return settingsEntity;
    }

    public async Task<ISettingsEntity?> GetSettingsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        SettingsEntity? settingsEntity = await GetSettingsAsync(withTracking: false, cancellationToken);

        return settingsEntity;
    }

    public async Task SaveSettingsAsync(ISettings settings, CancellationToken cancellationToken = default)
    {
        if (ThrowIfArgumentNull(settings))
        {
            return;
        }

        SettingsEntity? settingsEntity = await GetSettingsAsync(withTracking: true, cancellationToken);

        if (SettingsEqualityComparer.IsEquals(settings, settingsEntity))
        {
            return;
        }

        if (settingsEntity is null)
        {
            SettingsEntity newEntity = _settingsMapper.Map<SettingsEntity>(settings);

            await _repository.CreateAsync(newEntity, cancellationToken);

            return;
        }

        SettingsEntity updatedEntity = _settingsMapper.Pour(settings, settingsEntity);
        await _repository.UpdateAsync(updatedEntity, cancellationToken);
    }

    public async Task SaveStartingBudgetAsync(decimal startingBudget, CancellationToken cancellationToken = default)
    {
        SettingsEntity? settingsEntity = await GetSettingsAsync(withTracking: true, cancellationToken);

        if (settingsEntity is null)
        {
            settingsEntity = new SettingsEntity
            {
                StartingBudget = startingBudget
            };

            await _repository.CreateAsync(settingsEntity, cancellationToken);

            return;
        }

        settingsEntity.StartingBudget = startingBudget;

        await _repository.UpdateAsync(settingsEntity, cancellationToken);
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
        return _repository.ReadAll<SettingsEntity>();
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

        await _repository.DisposeAsync();
        GC.SuppressFinalize(this);

        _disposed = true;
    }
}
