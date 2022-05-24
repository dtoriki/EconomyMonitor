using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM;

/// <summary>
/// Presents notification for <see cref="Task"/> completion.
/// </summary>
/// <typeparam name="TResult">Type of task's completion result.</typeparam>
/// <remarks>
/// Inherits <see cref="NotifyTaskCompletion"/>.
/// Implements 
/// <see cref="INotifyTaskCompletion{TResult}"/>.
/// </remarks>
/// <exception cref="ObjectDisposedException"/>
/// <exception cref="ArgumentNullException"/>
public sealed class NotifyTaskCompletion<TResult> : NotifyTaskCompletion, INotifyTaskCompletion<TResult>
{
    /// <inheritdoc/>
    /// <remarks>
    /// Decorates <see cref="Task{TResult}.Result"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException"/>
    public TResult? Result
    {
        get
        {
            if (_disposed)
            {
                ThrowDisposed(this);
            }

            return _task.Status == TaskStatus.RanToCompletion
                ? ((Task<TResult>)_task).Result
                : default;
        }
    }

    /// <summary>
    /// Creates notification for <paramref name="task"/> completion.
    /// </summary>
    /// <param name="task"><see cref="Task{TResult}"/> for observe.</param>
    /// <exception cref="ArgumentNullException"/>
    public NotifyTaskCompletion(Task<TResult> task) : base(task) => ThrowIfArgumentNull(task);

    /// <inheritdoc/>
    protected override async Task WatchTaskAsync()
    {
        await base.WatchTaskAsync().ConfigureAwait(false);

        if (IsSuccessfullyCompleted)
        {
            OnPropertyChanged(nameof(Result));
        }
    }
}
