using System.Windows.Input;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Базовая реализация асинхронной команды <see cref="IAsyncCommand"/>.
/// </summary>
/// <remarks>
/// <para>
/// Наследует <see cref="NotifiableCommandBase"/>.
/// </para>
/// <para>
/// Реализует 
/// <see cref="IAsyncCommand"/>,
/// <see cref="IDisposable"/>,
/// <see cref="IAsyncDisposable"/>.
/// </para>
/// <para>
/// Событием <see cref="CanExecuteChanged"/> подписывается/отписывается на/от событие(я) <see cref="CommandManager.RequerySuggested"/>.
/// </para>
/// </remarks>
/// <exception cref="ObjectDisposedException"/>
public abstract class AsyncCommandBase : NotifiableCommandBase, IAsyncCommand, IDisposable, IAsyncDisposable
{
    private readonly CancelCommand _cancelCommand;
    private ITaskCompletion? _execution;
    private Task? _executionTask;
    private bool _isDisposed;

    /// <summary>
    /// Возвращает или задаёт статус того, является ли объект высвобожденным.
    /// </summary>
    /// <value>
    /// <see langword="true"/>, если текущий экземпляр высвобожден, иначе - <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// Если объект был высвобожден, то всегда имеет значение <see langword="true"/>, 
    /// даже если попытаться задать <see langword="false"/>.
    /// </remarks>
    protected bool IsDisposed 
    { 
        get => _isDisposed; 
        set => _isDisposed |= value; 
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public ITaskCompletion? Execution
    {
        get
        {
            if (IsDisposed)
            {
                ThrowDisposed(this);
            }

            return _execution;
        }

        protected set
        {
            if (IsDisposed)
            {
                ThrowDisposed(this);
            }

            _ = SetPropertyNotifiable(ref _execution, value);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public ICancelCommand CancelCommand
    {
        get
        {
            if (IsDisposed)
            {
                ThrowDisposed(this);
            }

            return _cancelCommand;
        }
    }

    /// <summary>
    /// Создаёт экземпляр асинхронной команды.
    /// </summary>
    protected AsyncCommandBase() : base() => _cancelCommand = new CancelCommand();

    /// <summary>
    /// Высвобождает текущий экземпляр.
    /// </summary>
    public void Dispose()
    {
        // ToDo: нужен protected метод типа Flush, чтобы очищать подписчиков.
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

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    Task IAsyncCommand.ExecuteAsync(object? parameter)
    {
        if (IsDisposed)
        {
            ThrowDisposed(this);
        }

        return ExecuteAsync(parameter);
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    protected override void Execute(object? parameter)
    {
        if (IsDisposed)
        {
            ThrowDisposed(this);
        }

        _executionTask?.Dispose();
        _executionTask = ExecuteAsync(parameter);
    }

    /// <inheritdoc cref="IAsyncCommand.ExecuteAsync(object?)"/>
    protected abstract Task ExecuteAsync(object? parameter);

    /// <inheritdoc cref="DisposeAsync"/>
    /// <param name="disposing">
    /// Указывает, нужно ли высвобождать управляемые ресурсы.
    /// </param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!IsDisposed)
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

        IsDisposed = true;
    }

    /// <inheritdoc cref="Dispose"/>
    /// <param name="disposing">
    /// Указывает, нужно ли высвобождать управляемые ресурсы.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (!IsDisposed)
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

        IsDisposed = true;
    }
}
