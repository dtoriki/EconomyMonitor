using System.Windows;
using System.Windows.Input;
using EconomyMonitor.Wpf.MVVM.Abstracts;

namespace EconomyMonitor.Wpf.MVVM.ViewModels;

/// <summary>
/// Window's state view model.
/// </summary>
public sealed class WindowStateViewModel : ViewModelBase
{
    private string? _title;
    private double _controlHeight;
    private double _buttonsHeight;
    private readonly RelayCommand _closeCommand;
    private readonly RelayCommand _minimalizeCommand;

    /// <summary>
    /// Gets or sets control's height.
    /// </summary>
    public double ControlHeight
    {
        get => _controlHeight;
        set => _ = SetPropertyNotifiable(ref _controlHeight, value);
    }

    /// <summary>
    /// Gets or sets buttons' square size.
    /// </summary>
    public double ButtonsSize
    {
        get => _buttonsHeight;
        set => _ = SetPropertyNotifiable(ref _buttonsHeight, value);
    }

    /// <summary>
    /// Gets or sets window's title.
    /// </summary>
    public string Title
    {
        get => _title ??= string.Empty;
        set => _ = SetPropertyNotifiable(ref _title, value);
    }

    /// <summary>
    /// Gets closing app command.
    /// </summary>
    public ICommand CloseCommand => _closeCommand;

    /// <summary>
    /// Gets minimalize app command.
    /// </summary>
    public ICommand MinimalizeCommand => _minimalizeCommand;

    /// <summary>
    /// Creates window state view model instance.
    /// </summary>
    public WindowStateViewModel()
    {
        _closeCommand = new RelayCommand(OnClose, OnCanClose);
        _minimalizeCommand = new RelayCommand(OnMinimalize, OnCanMinimalize);
    }

    private bool OnCanClose(object? parameter) => true;

    private void OnClose(object? parameter) => Application.Current.Shutdown();

    private bool OnCanMinimalize(object? parameter) => true;

    private void OnMinimalize(object? parameter) => Application.Current.MainWindow.WindowState = WindowState.Minimized;
}
