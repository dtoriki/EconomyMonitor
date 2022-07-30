using EconomyMonitor.Abstacts;
using static EconomyMonitor.Helpers.DisposeHelper;

namespace EconomyMonitor.Services.Implementations;

internal sealed class BudgetService : IDisposable, IAsyncDisposable, IBudgetService
{
    private readonly ISettingsService _settingsService;

    private bool _isDisposed;

    public BudgetService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public async Task<decimal> GetBudgetAsync(CancellationToken cancellationToken = default)
    {
        ISettings? settings = await _settingsService.GetSettingsAsync(cancellationToken);

        decimal startingBudget = settings?.StartingBudget ?? decimal.Zero;

        return startingBudget;
    }

    public async Task SetBudgetAsync(decimal budget, CancellationToken cancellationToken = default)
    {
        await _settingsService.SaveStartingBudgetAsync(budget, cancellationToken);
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _ = DisposeObject(_settingsService);
        _isDisposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_isDisposed)
        {
            return;
        }

        _ = await DisposeObjectAsync(_settingsService);
        _isDisposed = true;
    }
}
