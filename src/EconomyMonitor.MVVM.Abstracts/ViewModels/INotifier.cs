using System.ComponentModel;

namespace EconomyMonitor.MVVM.Abstracts.ViewModels;

/// <summary>
/// Defines notifier for changing properties.
/// </summary>
public interface INotifier : INotifyPropertyChanged
{
    /// <summary>
    /// Rises event of property changing.
    /// </summary>
    /// <param name="propertyName">Property's name.</param>
    void OnPropertyChanged(string? propertyName = null);

    /// <summary>
    /// Sets a <paramref name="value"/> to <paramref name="field"/> and notifies about changing.
    /// </summary>
    /// <typeparam name="T">Field's type.</typeparam>
    /// <param name="field">Changing field.</param>
    /// <param name="value">New field's value.</param>
    /// <param name="propertyName">Notifiable property's name.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="field"/> has chanched, otherwise - <see langword="false"/>.
    /// </returns>
    bool Set<T>(ref T? field, T? value, string? propertyName = null);
}
