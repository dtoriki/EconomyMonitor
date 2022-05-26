using System.Windows.Input;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Wpf.MVVM.Commands;

/// <summary>
/// Передаваемая команда <see cref="ICommand"/>.
/// </summary>
/// <remarks>
/// <inheritdoc/>
/// </remarks>
/// <exception cref="ArgumentNullException"/>
public sealed class RelayCommand : CommandBase
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool> _canExecute;

    /// <summary>
    /// Создаёт передаваемую команду.
    /// </summary>
    /// <param name="execute">Делегат выполнения команды.</param>
    /// <param name="canExecute">
    /// Делегат условия выполнения команды.
    /// По-умолчанию - <see langword="null"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Вызывается, если <paramref name="execute"/> является <see langword="null"/>.
    /// </exception>
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        ThrowIfArgumentNull(execute);

        _execute = execute;
        _canExecute = canExecute ?? AlwaysCan;
    }

    /// <inheritdoc/>
    protected override bool CanExecute(object? parameter) => _canExecute(parameter);

    /// <inheritdoc/>
    protected override void Execute(object? parameter) => _execute(parameter);

    private static bool AlwaysCan(object? _) => true;
}
