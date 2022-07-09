using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Передаваемая асинхронная команда <see cref="IAsyncCommand"/>.
/// </summary>
/// <remarks>
/// <para>
/// Наследует <see cref="AsyncCommandBase"/>.
/// </para>
/// <para>
/// От этого класса нельзя наследоваться.
/// </para>
/// </remarks>
/// <exception cref="ArgumentNullException"/>
/// <exception cref="ObjectDisposedException"/>
public sealed class RelayAsyncCommand : AsyncCommandBase
{
    private readonly Func<object?, CancellationToken, Task> _execute;
    private readonly Func<object?, CancellationToken, Task<bool>>? _canExecute;

    /// <summary>
    /// Создаёт передаваемую команду.
    /// </summary>
    /// <param name="execute">Делегат выполнения команды.</param>
    /// <param name="canExecute">
    /// Делегат условия выполнения команды.
    /// По-умолчанию - <see langword="null"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, когда <paramref name="execute"/> - <see langword="null"/>.
    /// </exception>
    public RelayAsyncCommand(
        Func<object?, CancellationToken, Task> execute, 
        Func<object?, CancellationToken, Task<bool>>? canExecute = null) : base()
    {
        _ = ThrowIfArgumentNull(execute);

        _execute = execute;
        _canExecute = canExecute;
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    protected override Task<bool> CanExecuteAsync(object? parameter, CancellationToken cancellationToken)
    {
        if (IsDisposed)
        {
            ThrowDisposed(this);
        }

        return _canExecute?.Invoke(parameter, cancellationToken) ?? Task.FromResult(true);
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Вызывается, если при обращении текущий экземпляр был уже высвобожден.
    /// </exception>
    protected override Task ExecuteAsync(object? parameter, CancellationToken cancellationToken)
    {
        if (IsDisposed)
        {
            ThrowDisposed(this);
        }

        return _execute(parameter, cancellationToken);
    }
}
