namespace EconomyMonitor.Wpf.MVVM.Generic;

/// <summary>
/// Тип выполнения задачи, позвращающей значение типа <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TResult">Тип возвращаемого задачей значения.</typeparam>
public interface ITaskCompletion<TResult> : ITaskCompletion
{
    /// <summary>
    /// Возвращает результат выполнения задачи.
    /// </summary>
    TResult? Result { get; }

    /// <summary>
    /// Возращает выполняемую задачу, возвращающую результат.
    /// </summary>
    /// <returns>Выполняемая задача, возвращающая результат.</returns>
    new Task<TResult?> TaskCompletionAsync();
}
