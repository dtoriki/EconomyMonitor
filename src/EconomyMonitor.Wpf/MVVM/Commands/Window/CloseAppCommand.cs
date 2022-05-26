using System.Windows;

namespace EconomyMonitor.Wpf.MVVM.Commands.Window;

internal sealed class CloseAppCommand : CommandBase
{
    protected override bool CanExecute(object? parameter) => true;

    protected override void Execute(object? parameter) => Application.Current.Shutdown();

    public CloseAppCommand()
    {

    }
}
