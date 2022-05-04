using System.Windows;
using System.ComponentModel;
using EconomyMonitor.MVVM.Abstracts.ViewModels.Internals;

namespace EconomyMonitor.MVVM.Abstracts.ViewModels;

/// <summary>
/// Base type of <see cref="DependencyObject"/> view model.
/// </summary>
public abstract class DependencyViewModelBase : DependencyObject, INotifyPropertyChanged
{
    protected readonly INotifier _notifier;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged
    {
        add => _notifier.PropertyChanged += value;
        remove => _notifier.PropertyChanged -= value;
    }

    /// <summary>
    /// Creates a <see cref="DependencyObject"/> view model exemplar.
    /// </summary>
    protected DependencyViewModelBase() => _notifier = new Notifier();
}
