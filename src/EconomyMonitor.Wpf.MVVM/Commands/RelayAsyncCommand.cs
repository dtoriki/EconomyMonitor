using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Relay <see cref="IAsyncCommand"/>'s implementation.
/// </summary>
/// <remarks>
/// Inherits <see cref="AsyncCommandBase"/>.
/// </remarks>
public class RelayAsyncCommand : AsyncCommandBase
{
    private readonly Func<object?, CancellationToken, Task> _execute;
    private readonly Func<object?, bool>? _canExecute;

    /// <summary>
    /// Creates relay async command.
    /// </summary>
    /// <param name="execute">Execution.</param>
    /// <param name="canExecute">Can execute predicate.</param>
    public RelayAsyncCommand(
        Func<object?, CancellationToken, Task> execute, 
        Func<object?, bool>? canExecute = null) : base()
    {
        _ = ThrowIfArgumentNull(execute);

        _execute = execute;
        _canExecute = canExecute;
    }

    /// <inheritdoc/>
    protected override bool CanExecute(object? parameter)
    {
        return (Execution is null || Execution.IsCompleted)
            && (_canExecute?.Invoke(parameter) ?? true);
    }

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(object? parameter)
    {
        CancelCommand.NotifyCommandStarting();
        Execution = new NotifyTaskCompletion(_execute(parameter, CancelCommand.CancellationToken));
        RiseCanExecuteChanged();
        await Execution.TaskCompletion().ConfigureAwait(false);
        CancelCommand.NotifyCommandFinished();
        RiseCanExecuteChanged();
    }
}
