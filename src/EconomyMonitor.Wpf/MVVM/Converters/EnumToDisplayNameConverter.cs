using System.Collections;
using System.Globalization;
using System.Windows.Data;
using EconomyMonitor.Extensions;
using static EconomyMonitor.Helpers.EnumHelper;
using static EconomyMonitor.Helpers.ThrowHelper;
using static EconomyMonitor.Literals.ExceptionMessages;

namespace EconomyMonitor.Wpf.MVVM.Converters;

internal class EnumToDisplayNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }

        if (value is IEnumerable enums)
        {
            List<object> descriptions = new();

            foreach(object enumValue in enums)
            {
                if (enumValue is not Enum enumsEntry)
                {
                    Throw<InvalidCastException>(string.Format(
                        WRONG_TYPE_RECEIVED,
                        nameof(Enum),
                        value.GetType().Name));

                    return null;
                }

                string? description = enumsEntry.GetDisplayDescription();

                if (!string.IsNullOrWhiteSpace(description))
                {
                    descriptions.Add(description);
                }
            }

            return descriptions;
        }

        if (value is not Enum @enum)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(Enum),
                value.GetType().Name));

            return null;
        }

        return @enum.GetDisplayDescription();
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string description)
        {
            Throw<InvalidCastException>(string.Format(
                WRONG_TYPE_RECEIVED,
                nameof(String),
                value.GetType().Name));

            return null;
        }

        return GetEnumValueByDisplay(targetType, description);
    }
}
