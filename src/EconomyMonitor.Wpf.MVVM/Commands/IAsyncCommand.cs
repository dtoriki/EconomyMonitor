using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Тип, описывающий асинхронную команду.
/// </summary>
/// <remarks>Наследует <see cref="ICommand"/>.</remarks>
public interface IAsyncCommand : ICommand
{
    /// <summary>
    /// Возвращает объект уведомления завершения работы задачи <see cref="Task"/>.
    /// </summary>
    ITaskCompletion? Execution { get; }

    /// <summary>
    /// Возвращает команду отмены операции.
    /// </summary>
    ICancelCommand CancelCommand { get; }

    /// <summary>
    /// Асинхронно запускает команду.
    /// </summary>
    /// <param name="parameter">Параметр команды.</param>
    Task ExecuteAsync(object? parameter);
}
