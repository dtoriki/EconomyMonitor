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
public class NotifyTaskCompletion : NotifyPropertyChangedBase, ITaskCompletion
{
    protected readonly Func<object?, CancellationToken, Task> _execution;

    private Exception? _exception;
    private bool _isInProgress;
    private bool _isCanceled;
    private bool _isFaulted;
    private bool _isCompletedSccesfully;
    private string? _errorMessage;

    protected bool _disposed;

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public bool IsInProgress
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _isInProgress;
        }

        private set => _ = SetPropertyNotifiable(ref _isInProgress, value);
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public bool IsCompletedSuccessfully
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _isCompletedSccesfully;
        }

        private set => _ = SetPropertyNotifiable(ref _isCompletedSccesfully, value);
    }

    /// <inheritdoc/>
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

            return _isCanceled;
        }

        private set => _ = SetPropertyNotifiable(ref _isCanceled, value);
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
            return _isFaulted;
        }
        private set => _ = SetPropertyNotifiable(ref _isFaulted, value);
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
        private set
        {
            _ = SetPropertyNotifiable(ref _exception, value);
            ErrorMessage = _exception?.InnerException?.Message;
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
            return _errorMessage;
        }
        private set => _ = SetPropertyNotifiable(ref _errorMessage, value);
    }

    /// <summary>
    /// Создаёт экземпляр, уведомляющий об изменении состояния выполнения задачи <paramref name="task"/>.
    /// </summary>
    /// <param name="task"><see cref="Task"/> для отслеживания состояния выполнения.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="task"/> оказался <see langword="null"/>.
    /// </exception>
    public NotifyTaskCompletion(Func<object?, CancellationToken, Task> execution)
    {
        ThrowIfArgumentNull(execution);

        _execution = execution;
    }

    /// <inheritdoc cref="ITaskCompletion.TaskCompletionAsync"/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public Task TaskCompletionAsync(object? parameter, CancellationToken cancellationToken = default)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        return WatchTaskAsync(GetExecutionTask(parameter, cancellationToken));
    }

    /// <summary>
    /// Ожидает когда задача <see cref="Task"/> завершит выполнение, после чего уводомит об её завршении.
    /// </summary>
    protected virtual async Task WatchTaskAsync(Task executionTask)
    {
        IsInProgress = true;

        try
        {
            await executionTask;
        }
        catch (Exception ex)
        {
            Exception = ex;
        }

        IsInProgress = false;
        IsFaulted = executionTask.IsFaulted;
        IsCanceled = executionTask.IsCanceled;
        IsCompletedSuccessfully = executionTask.IsCompletedSuccessfully;
    }

    private Task GetExecutionTask(object? parameter, CancellationToken cancellationToken)
    {
        return _execution(parameter, cancellationToken);
    }

    Task ITaskCompletion.TaskCompletionAsync(object? parameter, CancellationToken cancellationToken) => TaskCompletionAsync(parameter, cancellationToken);
}
