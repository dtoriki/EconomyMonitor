using EconomyMonitor.Wpf.MVVM.Generic;

namespace EconomyMonitor.Wpf.MVVM.Commands.Generic;

/// <inheritdoc/>
/// <typeparam name="TResult">Тип возвращаемого командой значения.</typeparam>
public interface IAsyncCommand<out TResult> : IAsyncCommand
{
    /// <summary>
    /// Возвращает объект уведомления завершения работы задачи <see cref="Task{TResult}"/>.
    /// </summary>
    new ITaskCompletion<TResult>? Execution { get; }
}
