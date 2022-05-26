using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Команда отмены операции.
/// </summary>
/// <remarks>
/// <para>
/// Наследует <see cref="CommandBase"/>.
/// </para>
/// <para>
/// Реализует 
/// <see cref="ICancelCommand"/>,
/// <see cref="IDisposable"/>.
/// </para>
/// <para>
/// От этого класса нельзя наследоваться.
/// </para>
/// </remarks>
public sealed class CancelCommand : CommandBase, IDisposable, ICancelCommand
{
    private CancellationTokenSource _cts;
    private bool _commandExecuting;
    private bool _disposed;

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public CancellationToken CancellationToken
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _cts.Token;
        }
    }

    /// <summary>
    /// Создаёт команду отмены операции.
    /// </summary>
    public CancelCommand() : base() => _cts = new CancellationTokenSource();

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
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
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    public void NotifyCommandFinished()
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        _commandExecuting = false;

        RiseCanExecuteChanged();
    }

    /// <summary>
    /// Высвобождает текущий экземпляр.
    /// </summary>
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

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    protected override bool CanExecute(object? parameter)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        return _commandExecuting && !_cts.IsCancellationRequested;
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    protected override void Execute(object? parameter)
    {
        if (_disposed)
        {
            ThrowDisposed(this);
        }

        _cts.Cancel();
        RiseCanExecuteChanged();
    }
}
