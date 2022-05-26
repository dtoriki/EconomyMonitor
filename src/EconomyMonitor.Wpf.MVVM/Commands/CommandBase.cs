using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Базовая реализация команды <see cref="ICommand"/>.
/// </summary>
/// <remarks>
/// <para>
/// Реализует 
/// <see cref="ICommand"/>.
/// </para>
/// <para>
/// Событием <see cref="CanExecuteChanged"/> подписывается/отписывается на/от событие(я) <see cref="CommandManager.RequerySuggested"/>.
/// </para>
/// </remarks>
public abstract class CommandBase : ICommand
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
    /// Создаёт команду.
    /// </summary>
    protected CommandBase() { }

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

    bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

    void ICommand.Execute(object? parameter) => Execute(parameter);
}
