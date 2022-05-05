using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Abstracts;

/// <summary>
/// Base <see cref="ICommand"/> implementation.
/// </summary>
/// <remarks>
/// Delegates <see cref="CanExecuteChanged"/> event suscribing/unsubscribing to <see cref="CommandManager.RequerySuggested"/>.
/// </remarks>
public abstract class CommandBase : ICommand
{
    /// <inheritdoc/>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Creates command exemplar.
    /// </summary>
    /// <remarks>
    /// Implements <see cref="ICommand"/>. Delegates <see cref="CanExecuteChanged"/> event suscribing/unsubscribing to <see cref="CommandManager.RequerySuggested"/>.
    /// </remarks>
    protected CommandBase() { }

    /// <inheritdoc/>
    public abstract bool CanExecute(object? parameter);

    /// <inheritdoc/>
    public abstract void Execute(object? parameter);
}
