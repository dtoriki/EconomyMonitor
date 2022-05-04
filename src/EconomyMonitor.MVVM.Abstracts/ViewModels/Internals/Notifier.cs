using System.ComponentModel;
using System.Runtime.CompilerServices;
using EconomyMonitor.Helpers;

namespace EconomyMonitor.MVVM.Abstracts.ViewModels.Internals;

/// <summary>
/// Presents notifier for changing properties.
/// </summary>
internal sealed class Notifier : INotifier
{
    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Creates notifier.
    /// </summary>
    public Notifier()
    {

    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// Throws when <paramref name="propertyName"/> is <see langword="null"/>.
    /// </exception>
    public void OnPropertyChanged([CallerMemberName]string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This method recursive safety. 
    /// After the <paramref name="field"/> has changed, 
    /// method rises <see cref="PropertyChanged"/> event if <paramref name="propertyName"/> not empty. 
    /// </remarks>
    public bool Set<T>(ref T? field, T? value, [CallerMemberName] string? propertyName = null)
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
