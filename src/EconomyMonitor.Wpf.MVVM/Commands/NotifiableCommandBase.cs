using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Base notifiable <see cref="ICommand"/> implementation.
/// </summary>
/// <remarks>
/// Inherits <see cref="NotifyPropertyChangedBase"/>.
/// Implements <see cref="ICommand"/>.
/// Delegates <see cref="CanExecuteChanged"/> event suscribing/unsubscribing to <see cref="CommandManager.RequerySuggested"/>.
/// </remarks>
public abstract class NotifiableCommandBase : NotifyPropertyChangedBase, ICommand
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
    protected NotifiableCommandBase() { }

    /// <inheritdoc/>
    bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

    /// <inheritdoc/>
    void ICommand.Execute(object? parameter) => Execute(parameter);

    /// <inheritdoc cref="ICommand.CanExecute(object?)"/>
    protected abstract bool CanExecute(object? parameter);

    /// <inheritdoc cref="ICommand.Execute(object?)"/>
    protected abstract void Execute(object? parameter);

    /// <summary>
    /// Rises <see cref="CommandManager.InvalidateRequerySuggested"/>.
    /// </summary>
    protected static void RiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
