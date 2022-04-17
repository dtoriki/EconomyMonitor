using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EconomyMonitor.Wpf.MVVM.Views;

/// <summary>
/// Window state control.
/// </summary>
public partial class WindowStateView : UserControl
{
    /// <summary>
    /// Creates window state control instance.
    /// </summary>
    public WindowStateView() => InitializeComponent();

    private void OnMouseDown(object sender, MouseButtonEventArgs e) => Application.Current.MainWindow.DragMove();
}
