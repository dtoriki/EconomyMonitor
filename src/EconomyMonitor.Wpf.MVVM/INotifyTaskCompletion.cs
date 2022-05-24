namespace EconomyMonitor.Wpf.MVVM;

/// <summary>
/// Defines <see cref="Task"/> completion notify interface.
/// </summary>
public interface INotifyTaskCompletion
{
    /// <summary>
    /// Gets error message.
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// Gets exception if <see cref="Task.IsFaulted"/> with an exception.
    /// </summary>
    Exception? Exception { get; }

    /// <summary>
    /// Get <see langword="true"/> if <see cref="Task"/> was canceled, 
    /// otherwise - <see langword="false"/>.
    /// </summary>
    bool IsCanceled { get; }

    /// <summary>
    /// Get <see langword="true"/> if <see cref="Task"/> was completed, 
    /// otherwise - <see langword="false"/>.
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// Get <see langword="true"/> if <see cref="Task"/> was faulted, 
    /// otherwise - <see langword="false"/>.
    /// </summary>
    bool IsFaulted { get; }

    /// <summary>
    /// Get <see langword="true"/> if <see cref="Task"/> was not completed, 
    /// otherwise - <see langword="false"/>.
    /// </summary>
    bool IsNotCompleted { get; }

    /// <summary>
    /// Get <see langword="true"/> if <see cref="Task"/> was successfulle completed, 
    /// otherwise - <see langword="false"/>.
    /// </summary>
    bool IsSuccessfullyCompleted { get; }

    /// <summary>
    /// Gets task's status.
    /// </summary>
    TaskStatus Status { get; }

    /// <summary>
    /// Deliver completion task.
    /// </summary>
    /// <returns></returns>
    Task TaskCompletion();
}
