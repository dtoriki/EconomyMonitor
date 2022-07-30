using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using static EconomyMonitor.Literals.InfoMessages;
using static EconomyMonitor.Literals.RegularExpressions;

namespace EconomyMonitor.Wpf.MVVM.ValidationRules;

internal class DecimalValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is not string str)
        {
            return new ValidationResult(false, string.Empty);
        }

        if (string.IsNullOrEmpty(str) || Regex.IsMatch(str, FLOATING_POINT_NUMBER_REGEX))
        {
            return new ValidationResult(true, string.Empty);
        }

        return new ValidationResult(false, WRONG_FORMAT);
    }
}
