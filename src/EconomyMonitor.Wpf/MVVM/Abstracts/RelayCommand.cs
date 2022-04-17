using EconomyMonitor.Wpf.Helpers;

namespace EconomyMonitor.Wpf.MVVM.Abstracts;

/// <summary>
/// Relay <see cref="CommandBase"/>'s implementation.
/// </summary>
/// <remarks>
/// <inheritdoc/>
/// </remarks>
public sealed class RelayCommand : CommandBase
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool> _canExecute;

    /// <summary>
    /// Creates relay command exemplar.
    /// </summary>
    /// <param name="execute">Execute method. <see cref="CommandBase.Execute(object?)"/></param>
    /// <param name="canExecute">CanExecute method. <see cref="CommandBase.CanExecute(object?)"/></param>
    /// <exception cref="ArgumentNullException">Throws when <paramref name="execute"/> is <see langword="null"/>.</exception>
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        ArgsHelper.ThrowIfNull(execute);

        _execute = execute;
        _canExecute = canExecute ?? AlwaysCan;
    }

    /// <inheritdoc/>
    public override bool CanExecute(object? parameter) => _canExecute(parameter);

    /// <inheritdoc/>
    public override void Execute(object? parameter) => _execute(parameter);

    private static bool AlwaysCan(object? _) => true;
}
