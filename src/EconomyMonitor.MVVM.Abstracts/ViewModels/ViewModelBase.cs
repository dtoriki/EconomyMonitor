using System.ComponentModel;
using EconomyMonitor.MVVM.Abstracts.ViewModels.Internals;

namespace EconomyMonitor.MVVM.Abstracts.ViewModels;

/// <summary>
/// Base type of view model.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    protected readonly INotifier _notifier;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged
    {
        add => _notifier.PropertyChanged += value;
        remove => _notifier.PropertyChanged -= value;
    }

    /// <summary>
    /// Creates a view model exemplar.
    /// </summary>
    protected ViewModelBase() => _notifier = new Notifier();
}
