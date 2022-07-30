using System.Globalization;
using System.Windows.Data;
using static EconomyMonitor.Helpers.ThrowHelper;
using static EconomyMonitor.Literals.ExceptionMessages;

namespace EconomyMonitor.Wpf.MVVM.Converters;

internal sealed class DateTimeToDateOnlyConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not DateOnly dateOnly)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(DateTime),
                value.GetType().Name));

            return null;
        }

        return dateOnly.ToDateTime(TimeOnly.MinValue);
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not DateTime dateTime)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(DateTime),
                value.GetType().Name));

            return null;
        }

        return DateOnly.FromDateTime(dateTime);
    }
}
