namespace EconomyMonitor.Wpf.MVVM;

/// <summary>
/// Defines <see cref="Task{TResult}"/> completion notify interface.
/// </summary>
/// <typeparam name="TResult">Type of task's result.</typeparam>
public interface INotifyTaskCompletion<out TResult> : INotifyTaskCompletion
{
    /// <summary>
    /// Gets result of task's completion.
    /// </summary>
    TResult? Result { get; }
}
