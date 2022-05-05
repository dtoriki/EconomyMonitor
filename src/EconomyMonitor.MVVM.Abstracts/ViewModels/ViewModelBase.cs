using System.ComponentModel;
using System.Runtime.CompilerServices;
using EconomyMonitor.Helpers;

namespace EconomyMonitor.MVVM.Abstracts.ViewModels;

/// <summary>
/// Base type of view model.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Creates a view model exemplar.
    /// </summary>
    protected ViewModelBase() { }

    /// <summary>
    /// Rises event of property changing.
    /// </summary>
    /// <param name="propertyName">Property's name.</param>
    /// <exception cref="ArgumentNullException">Throws when <paramref name="propertyName"/> is <see langword="null"/>.</exception>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
    /// <remarks>
    /// This method recursive safety. 
    /// After the <paramref name="field"/> has changed, 
    /// method rises <see cref="PropertyChanged"/> event if <paramref name="propertyName"/> not empty. 
    /// </remarks>
    protected virtual bool SetPropertyNotifiable<T>(ref T? field, T? value, [CallerMemberName] string? propertyName = null)
    {
        void Notify()
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                OnPropertyChanged(propertyName);
            }
        }

        return ArgsHelper.Set(ref field, value, Notify);
    }
}
