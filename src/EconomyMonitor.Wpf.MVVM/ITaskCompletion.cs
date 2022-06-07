namespace EconomyMonitor.Wpf.MVVM;

/// <summary>
/// Тип, выполнения задачи.
/// </summary>
public interface ITaskCompletion
{
    /// <summary>
    /// Возвращает сообщение об ошибке, с которой завершилось выполнение задачи.
    /// </summary>
    string? ErrorMessage { get; }

    /// <summary>
    /// Возвращает исключение, с которым завершилась задача.
    /// </summary>
    Exception? Exception { get; }

    /// <summary>
    /// Возвращает <see langword="true"/>, если выполнение задачи было отменено, 
    /// иначе - <see langword="false"/>.
    /// </summary>
    bool IsCanceled { get; }

    /// <summary>
    /// Возвращает <see langword="true"/>, если выполнение задачи завершилось, 
    /// иначе - <see langword="false"/>.
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// Возвращает <see langword="true"/>, если выполнение задачи вызвало ошибку, 
    /// иначе - <see langword="false"/>.
    /// </summary>
    bool IsFaulted { get; }

    /// <summary>
    /// Возвращает <see langword="true"/>, если выполнение задачи ещё не завершилось, 
    /// иначе - <see langword="false"/>.
    /// </summary>
    bool IsNotCompleted { get; }

    /// <summary>
    /// Возвращает <see langword="true"/>, если выполнение задачи завершилось успешно, 
    /// иначе - <see langword="false"/>.
    /// </summary>
    bool IsSuccessfullyCompleted { get; }

    /// <summary>
    /// Возвращает статус выполнения задачи.
    /// </summary>
    TaskStatus Status { get; }

    /// <summary>
    /// Возращает выполняемую задачу.
    /// </summary>
    /// <returns>Выполняемая задача.</returns>
    Task TaskCompletionAsync();
}
