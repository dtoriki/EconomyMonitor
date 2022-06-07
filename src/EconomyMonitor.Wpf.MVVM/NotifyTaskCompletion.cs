using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM;

/// <summary>
/// Уведомляет об изменении состояния выполнения задачи <see cref="Task"/>.
/// </summary>
/// <remarks>
/// <para>
/// Наследует <see cref="NotifyPropertyChangedBase"/>.
/// </para>
/// <para>
/// Реализует 
/// <see cref="ITaskCompletion"/>,
/// <see cref="IDisposable"/>,
/// <see cref="IAsyncDisposable"/>.
/// </para>
/// </remarks>
/// <exception cref="ObjectDisposedException"/>
/// <exception cref="ArgumentNullException"/>
public class NotifyTaskCompletion : NotifyPropertyChangedBase, IDisposable, IAsyncDisposable, ITaskCompletion
{
    private Task? _taskCompletion;

    private Exception? _exception;

    protected bool _disposed;
    protected readonly Task _task;

    /// <inheritdoc/>
    /// <remarks>
    /// Оборачивает <see cref="Task.Status"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
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
    /// Оборачивает <see cref="Task.IsCompleted"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
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
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public bool IsNotCompleted => !IsCompleted;

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
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
    /// Оборачивает <see cref="Task.IsCanceled"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
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
    /// Оборачивает <see cref="Task.IsFaulted"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
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
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
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
    /// Оборачивает <see cref="Exception.Message"/> из <see cref="Exception"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
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
    /// Создаёт экземпляр, уведомляющий об изменении состояния выполнения задачи <paramref name="task"/>.
    /// </summary>
    /// <param name="task"><see cref="Task"/> для отслеживания состояния выполнения.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="task"/> оказался <see langword="null"/>.
    /// </exception>
    public NotifyTaskCompletion(Task task)
    {
        ThrowIfArgumentNull(task);

        _task = task;
    }

    /// <inheritdoc cref="ITaskCompletion.TaskCompletionAsync"/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public Task TaskCompletionAsync()
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        return _taskCompletion ??= WatchTaskAsync();
    }

    /// <summary>
    /// Высвобождает текущий экземпляр.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Асинхронно высвобождает текущий экземпляр.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true).ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Ожидает когда задача <see cref="Task"/> завершит выполнение, после чего уводомит об её завршении.
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

    /// <inheritdoc cref="Dispose"/>
    /// <param name="disposing">
    /// Указывает, нужно ли высвобождать управляемые ресурсы.
    /// </param>
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

    /// <inheritdoc cref="DisposeAsync"/>
    /// <param name="disposing">
    /// Указывает, нужно ли высвобождать управляемые ресурсы.
    /// </param>
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

    Task ITaskCompletion.TaskCompletionAsync() => TaskCompletionAsync();
}
