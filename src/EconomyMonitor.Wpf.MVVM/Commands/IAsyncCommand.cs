using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Defines asynchronusly executing command.
/// </summary>
/// <remarks>Inherits <see cref="ICommand"/>.</remarks>
public interface IAsyncCommand : ICommand
{
    /// <summary>
    /// Gets <see cref="Task"/> execution completion.
    /// </summary>
    INotifyTaskCompletion? Execution { get; }

    /// <summary>
    /// Gets cancel command.
    /// </summary>
    ICancelCommand CancelCommand { get; }

    /// <summary>
    /// Executes command asynchronusly.
    /// </summary>
    /// <param name="parameter">Command parameter.</param>
    Task ExecuteAsync(object? parameter);
}
