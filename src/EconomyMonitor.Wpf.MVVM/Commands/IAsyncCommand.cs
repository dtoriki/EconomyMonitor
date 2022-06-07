using System.Windows.Input;
using EconomyMonitor.Wpf.MVVM.Generic;

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
    /// Возвращает объект уведомления завершения проверки на возможность запустить команду.
    /// </summary>
    /// <remarks>
    /// Результатом выполнения проверки будет <see langword="true"/>, 
    /// если можно запустить команду, иначе - <see langword="false"/>.
    /// </remarks>
    ITaskCompletion<bool>? CanExecution { get; }

    /// <summary>
    /// Возвращает команду отмены операции.
    /// </summary>
    ICancelCommand CancelCommand { get; }

    /// <summary>
    /// Асинхронно запускает команду на выполнение.
    /// </summary>
    /// <param name="parameter">Параметр команды.</param>
    Task ExecuteAsync(object? parameter);

    /// <summary>
    /// Асинхронно выполняет проверку на возможность запуска команды.
    /// </summary>
    /// <param name="parameter">Параметр команды.</param>
    Task<bool> CanExecuteAsync(object? parameter);
}
