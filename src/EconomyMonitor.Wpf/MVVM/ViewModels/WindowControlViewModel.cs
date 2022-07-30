using System.Windows;
using System.Windows.Input;
using EconomyMonitor.Wpf.MVVM.Commands;

namespace EconomyMonitor.Wpf.MVVM.ViewModels;

internal class WindowControlViewModel : NotifyPropertyChangedBase
{
    public ICommand CloseWindowCommand { get; }
    public ICommand MinimalizeWindowCommand { get; }

    public WindowControlViewModel()
    {
        CloseWindowCommand = new RelayCommand(ExecuteCloseWindowCommand);
        MinimalizeWindowCommand = new RelayCommand(ExecuteMinimalizeWindowCommand);
    }

    private void ExecuteCloseWindowCommand(object? parameter) => Application.Current.Shutdown();
    private void ExecuteMinimalizeWindowCommand(object? parameter) => Application.Current.MainWindow.WindowState = WindowState.Minimized;
}
