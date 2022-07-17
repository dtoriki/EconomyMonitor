using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Data.Mappers;
using EconomyMonitor.Data.UnitOfWorks;
using EconomyMonitor.Domain;
using static EconomyMonitor.Helpers.DisposeHelper;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Services.Implementations;

internal sealed class SettingsService : ISettingsService, IDisposable, IAsyncDisposable
{
    private const int MILISECONDS_PER_SECOND = 1000;
    private const int TIMEOUT_IN_SECONDS = 5;
   
    private static readonly SemaphoreSlim SemaphoreSlim = new(1);
    private static readonly int Timeout = TIMEOUT_IN_SECONDS * MILISECONDS_PER_SECOND;

    private static ISettings? _settings;
    private static bool _isSettingsFetched;

    private readonly ISettingsUnitOfWork _settingsUnitOfWork;
    private readonly ISettingsMapper _settingsMapper;

    private bool _isDisposed;

    public SettingsService(ISettingsUnitOfWork settingsUnitOfWork, ISettingsMapper settingsMapper)
    {
        _ = ThrowIfArgumentNull(settingsUnitOfWork);
        _ = ThrowIfArgumentNull(settingsMapper);

        _settingsUnitOfWork = settingsUnitOfWork;
        _settingsMapper = settingsMapper;
    }

    public Task<ISettings?> GetSettingsAsync(CancellationToken cancellationToken = default)
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        if (_isSettingsFetched)
        {
            return Task.FromResult(_settings);
        }

        return FetchSettingsAsync(cancellationToken);
    }

    public ISettings? GetCurrentSettingsValue() => _settings;

    public Task<ISettings?> UploadSettingsAsync(CancellationToken cancellationToken = default)
    {
        return FetchSettingsAsync(cancellationToken);
    }

    public async Task<bool> SaveSettingsAsync(ISettings settings, CancellationToken cancellationToken)
    {
        if (_isDisposed)
        {
            ThrowDisposed(this);
        }

        try
        {
            await _settingsUnitOfWork.SaveSettingsAsync(settings, cancellationToken)
                .ConfigureAwait(false);
        }
        catch
        {
            return false;
        }

        await UpdateSettingsAsync(settings, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

    private async Task<ISettings?> FetchSettingsAsync(CancellationToken cancellationToken)
    {
        ISettingsEntity? settingsEntity = await _settingsUnitOfWork
            .GetSettingsNoTrackingAsync(cancellationToken)
            .ConfigureAwait(false);

        await UpdateSettingsAsync(settingsEntity, cancellationToken)
            .ConfigureAwait(false);

        return _settings;
    }

    private async Task UpdateSettingsAsync(ISettings? settings, CancellationToken cancellationToken)
    {
        await SemaphoreSlim
            .WaitAsync(Timeout, cancellationToken)
            .ConfigureAwait(false);

        _settings = _settingsMapper.Map<Settings>(settings);
        _isSettingsFetched = true;

        SemaphoreSlim.Release();
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        if (DisposeObject(_settingsUnitOfWork))
        {
            GC.SuppressFinalize(this);
        }

        _isDisposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_isDisposed)
        {
            return;
        }

        if (await DisposeObjectAsync(_settingsUnitOfWork).ConfigureAwait(false))
        {
            GC.SuppressFinalize(this);
        }

        _isDisposed = true;
    }
}
