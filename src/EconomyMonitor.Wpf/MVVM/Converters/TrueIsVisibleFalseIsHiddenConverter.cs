using System.Globalization;
using System.Windows;
using System.Windows.Data;
using static EconomyMonitor.Helpers.ThrowHelper;
using static EconomyMonitor.Literals.ExceptionMessages;

namespace EconomyMonitor.Wpf.MVVM.Converters;

internal sealed class TrueIsVisibleFalseIsHiddenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool isVisible)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(Boolean),
                value.GetType().Name));

            return null;
        }

        return isVisible 
            ? Visibility.Visible
            : Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Visibility visibility)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(Visibility),
                value.GetType().Name));

            return null;
        }

        return visibility == Visibility.Visible;
    }
}

internal sealed class TrueIsVisibleFalseIsCollapseConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool isVisible)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(Boolean),
                value.GetType().Name));

            return null;
        }

        return isVisible
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Visibility visibility)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(Visibility),
                value.GetType().Name));

            return null;
        }

        return visibility == Visibility.Visible;
    }
}

internal sealed class TrueIsCollapseFalseIsVisibleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool isInvisible)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(Boolean),
                value.GetType().Name));

            return null;
        }

        return isInvisible
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Visibility visibility)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(Visibility),
                value.GetType().Name));

            return null;
        }

        return visibility == Visibility.Collapsed;
    }
}
