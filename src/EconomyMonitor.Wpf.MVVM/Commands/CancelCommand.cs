using System.Windows.Input;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Presents cancel command.
/// </summary>
/// <remarks>
/// Inherits <see cref="CommandBase"/>.
/// Implements 
/// <see cref="ICancelCommand"/>
/// <see cref="IDisposable"/>.
/// </remarks>
public sealed class CancelCommand : CommandBase, IDisposable, ICancelCommand
{
    private CancellationTokenSource _cts;
    private bool _commandExecuting;
    private bool _disposed;
    
    /// <inheritdoc/>
    public CancellationToken CancellationToken => _cts.Token;

    /// <summary>
    /// Creates cancel command.
    /// </summary>
    public CancelCommand() => _cts = new CancellationTokenSource();

    /// <inheritdoc/>
    public void NotifyCommandStarting()
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        _commandExecuting = true;

        if (!_cts.IsCancellationRequested)
        {
            return;
        }

        _cts.Dispose();
        _cts = new CancellationTokenSource();

        RiseCanExecuteChanged();
    }

    /// <inheritdoc/>
    public void NotifyCommandFinished()
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        _commandExecuting = false;

        RiseCanExecuteChanged();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _cts.Dispose();
        GC.SuppressFinalize(this);
        _disposed = true;
    }

    /// <inheritdoc cref="ICommand.CanExecute(object?)"/>
    protected override bool CanExecute(object? parameter)
    {
        return _commandExecuting && !_cts.IsCancellationRequested;
    }

    /// <inheritdoc cref="ICommand.Execute(object?)"/>
    protected override void Execute(object? parameter)
    {
        _cts.Cancel();
        RiseCanExecuteChanged();
    }
}
