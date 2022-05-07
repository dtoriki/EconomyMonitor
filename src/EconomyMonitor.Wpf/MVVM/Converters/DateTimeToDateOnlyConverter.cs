using System.Globalization;
using System.Windows.Data;
using EconomyMonitor.Wpf.Helpers;
using EconomyMonitor.Wpf.Helpers.Internal;

namespace EconomyMonitor.Wpf.MVVM.Converters;

/// <summary>
/// Converts <see cref="DateOnly"/> in <see cref="DateTime"/> and back.
/// </summary>
/// <exception cref="InvalidCastException"/>
public class DateTimeToDateOnlyConverter : IValueConverter
{
    /// <inheritdoc/>
    /// <exception cref="InvalidCastException">
    /// Throws when value is not <see cref="DateTime"/>.
    /// </exception>
    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not DateTime dateTime)
        {
            ThrowHelper.Throw<InvalidCastException>(string.Format(
                ExceptionMessages.WRONG_TYPE_RECEIVED,
                nameof(DateTime),
                value.GetType().Name));

            return null;
        }

        return DateOnly.FromDateTime(dateTime);
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidCastException">
    /// Throws when value is not <see cref="DateOnly"/>.
    /// </exception>
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not DateOnly dateOnly)
        {
            ThrowHelper.Throw<InvalidCastException>(string.Format(
                ExceptionMessages.WRONG_TYPE_RECEIVED,
                nameof(DateTime),
                value.GetType().Name));

            return null;
        }

        return dateOnly.ToDateTime(TimeOnly.MinValue);
    }
}
