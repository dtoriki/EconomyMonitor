using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using EconomyMonitor.Wpf.MVVM.Abstracts;
using static EconomyMonitor.Wpf.Helpers.ArgsHelper;
using static EconomyMonitor.Wpf.Helpers.ThrowHelper;
using static EconomyMonitor.Wpf.Helpers.Internal.ExceptionMessages;
using EconomyMonitor.Wpf.EventArguments;

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
        set => _ = SetPropertyNotifiable(ref _selectedItem, value);
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

        Items.CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action is not NotifyCollectionChangedAction.Add
            || e.NewItems is null)
        {
            return;
        }

        foreach(object? item in e.NewItems)
        {
            var menuItem = (HeaderMenuItem)item;

            if (menuItem.IsSelected)
            {
                if (SelectedItem is not null)
                {
                    Throw<InvalidOperationException>(SELECTED_ITEM_ALREADY_EXISTS);
                }

                SelectedItem = menuItem;
            }

            menuItem.SelectedChanged += OnIntemSelectedChanged;
        }
    }

    private void OnIntemSelectedChanged(object? sender, PropertyValueChangedEventArgs<bool> e)
    {
        if (sender is null)
        {
            Throw<ArgumentNullException>(nameof(sender));
        }

        if (e.NewValue)
        {
            SelectedItem = (HeaderMenuItem)sender;
        }
    }

    private bool CanSelect(object? parameter)
    {
        if (parameter is null)
        {
            return false;
        }

        if (parameter is not HeaderMenuItem menuItem)
        {
            Throw<InvalidCastException>(
                string.Format(
                    WRONG_TYPE_RECEIVED,
                    nameof(HeaderMenuItem),
                    parameter.GetType().Name));

            return false;
        }

        return !ReferenceEquals(parameter, SelectedItem);
    }

    private void Select(object? parameter)
    {
        if (ThrowIfNull(parameter, nameof(parameter)))
        {
            return;
        }

        if (parameter is not HeaderMenuItem selected)
        {
            Throw<InvalidCastException>(
                string.Format(
                    WRONG_TYPE_RECEIVED,
                    nameof(HeaderMenuItem),
                    parameter.GetType().Name));

            return;
        }

        if (SelectedItem is not null)
        {
            SelectedItem.IsSelected = false;
        }

        selected.IsSelected = true;
    }
}
