namespace EconomyMonitor.Wpf.MVVM.Generic;

/// <summary>
/// Тип выполнения задачи, позвращающей значение типа <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TResult">Тип возвращаемого задачей значения.</typeparam>
public interface ITaskCompletion<out TResult> : ITaskCompletion
{
    /// <summary>
    /// Возвращает результат выполнения задачи.
    /// </summary>
    TResult? Result { get; }
}
