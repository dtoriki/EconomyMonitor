using System.ComponentModel;
using System.Runtime.CompilerServices;
using static EconomyMonitor.Helpers.SetHelper;

namespace EconomyMonitor.Wpf.MVVM;

/// <summary>
/// Базовая реализация типа, который может уведомлять об изменении своего состояния.
/// </summary>
public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
{
    /// <summary>
    /// Уведомляет когда текущий экземпляр изменил своё состояние.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Создаёт экземпляр, который может уведомлять об изменении своего состояния.
    /// </summary>
    protected NotifyPropertyChangedBase() { }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="propertyName">
    /// Имя свойства. По-умолчанию - имя свойства, которое вызвало этот метод.
    /// </param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Устанавливает полю <paramref name="field"/> значение <paramref name="value"/>. 
    /// Затем вызывает событие <see cref="PropertyChanged"/> с помощью метода <see cref="OnPropertyChanged(string?)"/>.
    /// </summary>
    /// <typeparam name="T">Тип поля.</typeparam>
    /// <param name="field">Изменяемое поле.</param>
    /// <param name="value">Новое значение поля.</param>
    /// <param name="propertyName">
    /// Имя свойства, которое изменилось.
    /// По-умолчанию, то свойство, из которого был вызван метод.
    /// </param>
    /// <returns>
    /// <see langword="true"/>, если поле <paramref name="field"/> изменило значение, иначе - <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// Этот метод не вызывает рекурсии, 
    /// в случае если два разных свойства ссылаются друг на друга 
    /// и пытаются изменить одно и тоже поле на одно и тоже значение.
    /// </item>
    /// <item>
    /// Вызывает <see cref="OnPropertyChanged(string?)"/> только если значение поля <paramref name="field"/> изменилось.
    /// </item>
    /// </list>
    /// </remarks>
    protected virtual bool SetPropertyNotifiable<T>(ref T? field, T? value, [CallerMemberName] string? propertyName = null)
    {
        void Notify()
        {
            OnPropertyChanged(propertyName);
        }

        return Set(ref field, value, Notify);
    }

    /// <summary>
    /// Очищает подписчиков события <see cref="PropertyChanged"/>.
    /// </summary>
    protected virtual void FlushPropertyChangedSubscribers() => PropertyChanged = null;
}
