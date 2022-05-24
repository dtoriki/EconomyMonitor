using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM;

/// <summary>
/// Presents notification for <see cref="Task"/> completion.
/// </summary>
/// <remarks>
/// Inherits <see cref="NotifyPropertyChangedBase"/>.
/// Implements 
/// <see cref="INotifyTaskCompletion"/>,
/// <see cref="IDisposable"/>,
/// <see cref="IAsyncDisposable"/>.
/// </remarks>
/// <exception cref="ObjectDisposedException"/>
/// <exception cref="ArgumentNullException"/>
public class NotifyTaskCompletion : NotifyPropertyChangedBase, IDisposable, IAsyncDisposable, INotifyTaskCompletion
{
    private Task? _taskCompletion;

    private Exception? _exception;

    protected bool _disposed;
    protected readonly Task _task;

    /// <inheritdoc/>
    /// <remarks>
    /// Decorates <see cref="Task.Status"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException"/>
    public TaskStatus Status
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _task.Status;
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Decorates <see cref="Task.IsCompleted"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException"/>
    public bool IsCompleted
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _task.IsCompleted;
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException"/>
    public bool IsNotCompleted => !IsCompleted;

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException"/>
    public bool IsSuccessfullyCompleted
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _task.Status == TaskStatus.RanToCompletion;
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Decorates <see cref="Task.IsCanceled"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException"/>
    public bool IsCanceled
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _task.IsCanceled;
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Decorates <see cref="Task.IsFaulted"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException"/>
    public bool IsFaulted
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _task.IsFaulted;
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException"/>
    public Exception? Exception
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _exception;
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Decorates <see cref="Exception.Message"/> in <see cref="Exception"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException"/>
    public string? ErrorMessage
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return Exception?.Message;
        }
    }

    /// <summary>
    /// Creates notification for <paramref name="task"/> completion.
    /// </summary>
    /// <param name="task"><see cref="Task"/> for observe.</param>
    /// <exception cref="ArgumentNullException"/>
    public NotifyTaskCompletion(Task task)
    {
        ThrowIfArgumentNull(task);

        _task = task;
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException"/>
    public Task TaskCompletion()
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        return _taskCompletion ??= WatchTaskAsync();
    }

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

    /// <summary>
    /// Awaits when <see cref="Task"/> will complete and notifies about completion.
    /// </summary>
    protected virtual async Task WatchTaskAsync()
    {
        try
        {
            await _task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }

        OnPropertyChanged(nameof(Status));
        OnPropertyChanged(nameof(IsCompleted));
        OnPropertyChanged(nameof(IsNotCompleted));

        if (IsFaulted)
        {
            OnPropertyChanged(nameof(IsFaulted));
            OnPropertyChanged(nameof(Exception));
            OnPropertyChanged(nameof(ErrorMessage));

            return;
        }

        if (IsCanceled)
        {
            OnPropertyChanged(nameof(IsCanceled));

            return;
        }

        OnPropertyChanged(nameof(IsSuccessfullyCompleted));
    }

    /// <inheritdoc cref="Dispose"/>.
    /// <param name="disposing">Have to dispose managed state.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _task.Dispose();

            if (_taskCompletion is not null)
            {
                if (!_taskCompletion.IsCompleted)
                {
                    _taskCompletion.Wait();
                }
                _taskCompletion.Dispose();  
            }
        }

        _disposed = true;
    }

    /// <inheritdoc cref="DisposeAsync"/>.
    /// <param name="disposing">Have to dispose managed state.</param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _task.Dispose();

            if (_taskCompletion is not null)
            {
                if (!_taskCompletion.IsCompleted)
                {
                    await _taskCompletion.ConfigureAwait(false);
                }
                _taskCompletion.Dispose();  
            }
        }

        _disposed = true;
    }
}
