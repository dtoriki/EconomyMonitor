using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.EfSets;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Data.Mappers;
using EconomyMonitor.Domain;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.UnitOfWorks;

internal sealed class SettingsUnitOfWork<TRepository> : ISettingsUnitOfWork, IDisposable, IAsyncDisposable
    where TRepository : IRepository, ISettingsSet<SettingsEntity>
{
    private readonly TRepository _repository;
    private readonly ISettingsMapper _mapper;

    private bool _disposed;

    public SettingsUnitOfWork(TRepository repository, ISettingsMapper mapper)
    {
        _ = ThrowIfArgumentNull(repository);
        _ = ThrowIfArgumentNull(mapper);

        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ISettings?> GetSettingsAsync(CancellationToken cancellationToken = default)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        SettingsEntity? settingsEntity = await GetSettingsAsyncInternal(cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<Settings>(settingsEntity);
    }

    private Task<SettingsEntity?> GetSettingsAsyncInternal(CancellationToken cancellationToken)
    {
        return _repository
            .ReadAll<SettingsEntity>()
            .SingleOrDefaultAsync(cancellationToken);
    }

    public Task SaveSettingsAsync(ISettings settings, CancellationToken cancellationToken = default)
    {
        _ = ThrowIfArgumentNull(settings);

        SettingsEntity entity = _mapper.Map<SettingsEntity>(settings);

        return _repository.CreateAsync(entity, cancellationToken);
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
