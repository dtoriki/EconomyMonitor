namespace EconomyMonitor.Wpf.MVVM.Commands.Generic;

/// <summary>
/// Defines asynchronusly executing command.
/// </summary>
/// <typeparam name="TResult">Type of command result.</typeparam>
/// <remarks>Inherits <see cref="IAsyncCommand"/>.</remarks>
public interface IAsyncCommand<out TResult> : IAsyncCommand
{
    /// <summary>
    /// Gets <see cref="Task{TResult}"/> execution completion.
    /// </summary>
    new INotifyTaskCompletion<TResult>? Execution { get; }
}
