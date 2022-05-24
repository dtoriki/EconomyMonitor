using System.Windows;
using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Commands.Window;

/// <summary>
/// Presents close application <see cref="ICommand"/>.
/// </summary>
public sealed class CloseAppCommand : CommandBase
{
    /// <inheritdoc/>
    protected override bool CanExecute(object? parameter) => true;

    /// <inheritdoc/>
    protected override void Execute(object? parameter) => Application.Current.Shutdown();

    /// <summary>
    /// Creates close app <see cref="ICommand"/> exemplar.
    /// </summary>
    public CloseAppCommand()
    {

    }
}
