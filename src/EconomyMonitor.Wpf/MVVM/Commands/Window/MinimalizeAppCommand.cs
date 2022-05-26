using System.Windows;

namespace EconomyMonitor.Wpf.MVVM.Commands.Window;

internal sealed class MinimalizeAppCommand : CommandBase
{
    protected override bool CanExecute(object? parameter) => true;

    protected override void Execute(object? parameter)
    {
        Application.Current.MainWindow.WindowState = WindowState.Minimized;
    }

    public MinimalizeAppCommand()
    {

    }
}
