using System.Globalization;
using System.Windows.Data;
using static EconomyMonitor.Helpers.ThrowHelper;
using static EconomyMonitor.Literals.ExceptionMessages;

namespace EconomyMonitor.Wpf.MVVM.Converters;

internal sealed class DecimalToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return parameter;
        }

        if (value is not string str)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(Decimal),
                value.GetType().Name));

            return parameter;
        }

        return str.Replace(",", ".");
    }

    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null || value == parameter)
        {
            return null;
        }

        if (value is not string str)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(String),
                value.GetType().Name));

            return parameter;
        }

        if (string.IsNullOrEmpty(str))
        {
            return null;
        }

        return str.Replace(",", ".");
    }
}
