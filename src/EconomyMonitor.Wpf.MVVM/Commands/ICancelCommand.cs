using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Тип команды отмены операции.
/// </summary>
/// <remarks>
/// Наследует <see cref="ICommand"/>.
/// </remarks>
public interface ICancelCommand : ICommand
{
    /// <summary>
    /// Возвращает индикацию выполнения команды.
    /// </summary>
    /// <value>
    /// <see langword="true"/>, если команда выполняется, иначе - <see langword="false"/>.
    /// </value>
    bool IsInProgress { get; }

    /// <summary>
    /// Возвращает токен отмены операции.
    /// </summary>
    CancellationToken CancellationToken { get; }

    /// <summary>
    /// Уведомляет о начале выполнения команды.
    /// </summary>
    void NotifyCommandStarting();

    /// <summary>
    /// Уведомляет об окончании выполнения команды.
    /// </summary>
    void NotifyCommandFinished();
}