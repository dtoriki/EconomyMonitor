using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands.Generic;

/// <summary>
/// Base <see cref="IAsyncCommand{TResult}"/> implementation.
/// </summary>
/// <typeparam name="TResult">Type of command result</typeparam>
/// <remarks>
/// Inherits <see cref="NotifiableCommandBase"/>.
/// Implements 
/// <see cref="IAsyncCommand{TResult}"/>,
/// <see cref="IDisposable"/>,
/// <see cref="IAsyncDisposable"/>.
/// Delegates <see cref="CanExecuteChanged"/> event suscribing/unsubscribing to <see cref="CommandManager.RequerySuggested"/>.
/// </remarks>
public abstract class AsyncCommandBase<TResult> : AsyncCommandBase, IAsyncCommand<TResult>
{
    /// <inheritdoc/>
    protected AsyncCommandBase(/*Func<CancellationToken, Task<TResult>> execute*/) : base()
    {
    }

    /// <inheritdoc/>
    INotifyTaskCompletion? IAsyncCommand.Execution => Execution;

    /// <inheritdoc/>
    new public INotifyTaskCompletion<TResult>? Execution
    {
        get => base.Execution as INotifyTaskCompletion<TResult>;
        set => base.Execution = value;
    }
}
