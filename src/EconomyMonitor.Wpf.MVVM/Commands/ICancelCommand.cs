using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Defines cancel command interface.
/// </summary>
/// Inherits <see cref="ICommand"/>.
public interface ICancelCommand : ICommand
{
    /// <summary>
    /// Gets caneclation token.
    /// </summary>
    CancellationToken CancellationToken { get; }

    /// <summary>
    /// Notifies command execution starting.
    /// </summary>
    void NotifyCommandStarting();

    /// <summary>
    /// Notifies command execution finishing.
    /// </summary>
    void NotifyCommandFinished();
}