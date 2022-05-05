using System.Collections.ObjectModel;
using System.Windows.Input;
using EconomyMonitor.Helpers;
using EconomyMonitor.MVVM.Abstracts.ViewModels;
using EconomyMonitor.Resources;
using EconomyMonitor.Wpf.MVVM.Abstracts;

namespace EconomyMonitor.Wpf.MVVM.ViewModels.Header;

/// <summary>
/// Presents header's menu view model.
/// </summary>
/// <remarks>Inherits <see cref="ViewModelBase"/>.</remarks>
public sealed class HeaderMenuViewModel : ViewModelBase
{
    private HeaderMenuItem? _selectedItem;

    /// <summary>
    /// Gets menu items' collection.
    /// </summary>
    public ObservableCollection<HeaderMenuItem> Items { get; }

    /// <summary>
    /// Gets or sets selected item.
    /// </summary>
    public HeaderMenuItem? SelectedItem
    {
        get => _selectedItem;
        set => _ = ArgsHelper.Set(ref _selectedItem, value);
    }

    /// <summary>
    /// Gets select command.
    /// </summary>
    public ICommand SelectCommand { get; }

    /// <summary>
    /// Creates header's menu view model.
    /// </summary>
    public HeaderMenuViewModel()
    {
        Items = new ObservableCollection<HeaderMenuItem>();
        SelectCommand = new RelayCommand(Select, CanSelect);
    }

    private bool CanSelect(object? parameter) => !ReferenceEquals(SelectedItem, parameter);

    private void Select(object? parameter)
    {
        if (ArgsHelper.ThrowIfNull(parameter, nameof(parameter)))
        {
            return;
        }

        if (parameter is not HeaderMenuItem selected)
        {
            ThrowHelper.Throw<InvalidCastException>(
                string.Format(
                    ExceptionMessages.WRONG_TYPE_RECEIVED,
                    nameof(HeaderMenuItem),
                    parameter.GetType().Name));

            return;
        }

        SelectedItem = selected;
    }
}
