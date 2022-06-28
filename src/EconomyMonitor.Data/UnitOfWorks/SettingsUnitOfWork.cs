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

    private bool _disosed;

    public SettingsUnitOfWork(TRepository repository, ISettingsMapper mapper)
    {
        _ = ThrowIfArgumentNull(repository);
        _ = ThrowIfArgumentNull(mapper);

        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ISettings?> GetSettingsAsync(CancellationToken cancellationToken = default)
    {
        if (_disosed)
        {
            ThrowDisposed(this);
        }

        SettingsEntity? settingsEntity = await _repository
            .ReadAll<SettingsEntity>()
            .SingleOrDefaultAsync(cancellationToken);

        return _mapper.Map<Settings>(settingsEntity);
    }
    public void Dispose()
    {
        if (_disosed)
        {
            return;
        }

        _repository.Dispose();
        GC.SuppressFinalize(this);

        _disosed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disosed)
        {
            return;
        }

        await _repository.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);

        _disosed = true;
    }
}
