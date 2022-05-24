using System.Windows;
using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands.Window;

/// <summary>
/// Presents minimalize window <see cref="ICommand"/>.
/// </summary>
public sealed class MinimalizeAppCommand : CommandBase
{
    /// <inheritdoc/>
    protected override bool CanExecute(object? parameter) => true;

    /// <inheritdoc/>
    protected override void Execute(object? parameter)
    {
        Application.Current.MainWindow.WindowState = WindowState.Minimized;
    }

    /// <summary>
    /// Creates minimalize window <see cref="ICommand"/> exemplar.
    /// </summary>
    public MinimalizeAppCommand()
    {

    }
}
