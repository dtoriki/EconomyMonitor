using System.Windows;
using System.Windows.Input;
using EconomyMonitor.Wpf.MVVM.Abstracts;

namespace EconomyMonitor.Wpf.MVVM.Commands.Window;

/// <summary>
/// Presents minimalize window <see cref="ICommand"/>.
/// </summary>
public sealed class MinimalizeAppCommand : CommandBase
{
    /// <inheritdoc/>
    public override bool CanExecute(object? parameter) => true;

    /// <inheritdoc/>
    public override void Execute(object? parameter)
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
