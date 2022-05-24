using System.Windows.Input;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Base <see cref="IAsyncCommand"/> implementation.
/// </summary>
/// <remarks>
/// Inherits <see cref="NotifiableCommandBase"/>.
/// Implements 
/// <see cref="IAsyncCommand"/>,
/// <see cref="IDisposable"/>,
/// <see cref="IAsyncDisposable"/>.
/// Delegates <see cref="CanExecuteChanged"/> event suscribing/unsubscribing to <see cref="CommandManager.RequerySuggested"/>.
/// </remarks>
public abstract class AsyncCommandBase : NotifiableCommandBase, IAsyncCommand, IDisposable, IAsyncDisposable
{
    private readonly CancelCommand _cancelCommand;
    private INotifyTaskCompletion? _execution;
    private Task? _executionTask;
    private bool _disposed;

    /// <inheritdoc/>
    public INotifyTaskCompletion? Execution
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _execution;
        }

        protected set
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            _ = SetPropertyNotifiable(ref _execution, value);
        }
    }

    /// <inheritdoc/>
    public ICancelCommand CancelCommand
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _cancelCommand;
        }
    }

    /// <summary>
    /// Creates aync command.
    /// </summary>
    /// <param name="execute"></param>
    protected AsyncCommandBase() => _cancelCommand = new CancelCommand();

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true).ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    Task IAsyncCommand.ExecuteAsync(object? parameter)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        return ExecuteAsync(parameter);
    }

    protected override void Execute(object? parameter)
    {
        _executionTask?.Dispose();
        _executionTask = ExecuteAsync(parameter);
    }

    /// <inheritdoc cref="IAsyncCommand.ExecuteAsync(object?)"/>
    protected abstract Task ExecuteAsync(object? parameter);

    /// <inheritdoc cref="IAsyncDisposable.DisposeAsync"/>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            return;
        }

        if (disposing)
        {
            if (Execution is IAsyncDisposable execution)
            {
                await execution.DisposeAsync().ConfigureAwait(false);
            }

            if (CancelCommand is IAsyncDisposable cancelCommand)
            {
                await cancelCommand.DisposeAsync().ConfigureAwait(false);
            }

            Dispose(disposing);
        }

        _disposed = true;
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            return;
        }

        if (disposing)
        {
            if (Execution is IDisposable execution)
            {
                execution.Dispose();
            }

            if (CancelCommand is IDisposable cancelCommand)
            {
                cancelCommand.Dispose();
            }

            _executionTask?.Dispose();
        }

        _disposed = true;
    }
}
