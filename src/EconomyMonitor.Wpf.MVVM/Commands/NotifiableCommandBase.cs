using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Базовая реализация типа команды <see cref="ICommand"/>, поддерживающией уведомления об изменениях состояния.
/// </summary>
/// <remarks>
/// <para>
/// Наследует <see cref="NotifyPropertyChangedBase"/>.
/// </para>
/// <para>
/// Реализует <see cref="ICommand"/>.
/// </para>
/// <para>
/// Событием <see cref="CanExecuteChanged"/> подписывается/отписывается на/от событие(я) <see cref="CommandManager.RequerySuggested"/>.
/// </para>
/// </remarks>
public abstract class NotifiableCommandBase : NotifyPropertyChangedBase, ICommand
{
    /// <summary>
    /// Вызывается, когда изменяется состояние возможности выполнить команду.
    /// </summary>
    /// <remarks>
    /// Подписывается на событие <see cref="CommandManager.RequerySuggested"/>, а так же отписывается от него.
    /// </remarks>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Создаёт экземпляр команды, поддерживающей уведомления об изменении своего состояния.
    /// </summary>
    protected NotifiableCommandBase() : base() { }

    /// <inheritdoc cref="CanExecute(object?)"/>
    bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

    /// <inheritdoc cref="Execute(object?)"/>
    void ICommand.Execute(object? parameter) => Execute(parameter);

    /// <summary>
    /// Проверяет возможность выполнить команду.
    /// </summary>
    /// <param name="parameter">Параметр команды.</param>
    /// <returns>
    /// <see langword="true"/>, если команду выполнить можно, иначе - <see langword="false"/>.
    /// </returns>
    protected abstract bool CanExecute(object? parameter);

    /// <summary>
    /// Запускает команду на выполнение.
    /// </summary>
    /// <param name="parameter">Параметр команды.</param>
    protected abstract void Execute(object? parameter);

    /// <summary>
    /// Вызывает <see cref="CommandManager.InvalidateRequerySuggested"/>.
    /// </summary>
    protected static void RiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
