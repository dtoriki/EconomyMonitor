using System.Windows.Input;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Domain;
using EconomyMonitor.Services.UnitOfWork;
using EconomyMonitor.Wpf.MVVM.Commands;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Periods;

/// <summary>
/// Presents add period view model.
/// </summary>
/// <remarks>
/// Inherits <see cref="ViewModelBase"/>.
/// </remarks>
public sealed class AddPeriodDialogViewModel : ViewModelBase, IPeriod, IDisposable, IAsyncDisposable
{
    private readonly IPeriodsUnitOfWork _periodsUnitOfWork;

    private DateOnly? _startPeriod;
    private DateOnly? _endPeriod;
    private decimal? _income;
    private bool _isOpen;
    private bool _disposed;

    /// <summary>
    /// Gets or sets opened state of dialog.
    /// </summary>
    public bool IsOpen 
    { 
        get => _isOpen; 
        set => _ = SetPropertyNotifiable(ref _isOpen, value); 
    }

    /// <inheritdoc/>
    public DateOnly StartPeriod
    {
        get => _startPeriod ??= DateOnly.FromDateTime(DateTime.Now);
        set => _ = SetPropertyNotifiable(ref _startPeriod, value);
    }

    /// <inheritdoc/>
    public DateOnly EndPeriod
    {
        get => _endPeriod ??= DateOnly.FromDateTime(DateTime.Now);
        set => _ = SetPropertyNotifiable(ref _endPeriod, value);
    }

    /// <inheritdoc/>
    public decimal Income
    {
        get => _income ??= decimal.Zero;
        set => _ = SetPropertyNotifiable(ref _income, value);
    }

    /// <summary>
    /// Gets create period command.
    /// </summary>
    public ICommand CreatePeriodCommand { get; }

    public AddPeriodDialogViewModel(IPeriodsUnitOfWork periodsUnitOfWork)
    {
        _periodsUnitOfWork = periodsUnitOfWork;
        CreatePeriodCommand = new RelayCommand(ExecuteCreatePeriod, CanCreatePeriod);
    }

    private bool CanCreatePeriod(object? parameter)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        if (_endPeriod < _startPeriod)
        {
            return false;
        }

        if (_income is null || _income == decimal.Zero)
        {
            return false;
        }

        return true;
    }

    private async void ExecuteCreatePeriod(object? parameter)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        await _periodsUnitOfWork.CreatePeriodAsync(new Period
        {
            EndPeriod = EndPeriod,
            StartPeriod = StartPeriod,
            Income = Income
        });

        IsOpen = false;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_periodsUnitOfWork is IDisposable disposable)
        {
            disposable.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        if (_periodsUnitOfWork is IAsyncDisposable disposable)
        {
            await disposable.DisposeAsync();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}