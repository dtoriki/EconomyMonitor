namespace EconomyMonitor.Literals;

/// <summary>
/// Содержит литералы регулярных выражений.
/// </summary>
public static class RegularExpressions
{
    /// <summary>
    /// Возвращает выражение, определяющее строку числа с плавающей точкой.
    /// </summary>
    public static string FLOATING_POINT_NUMBER_REGEX => @"^\-?[0-9]+(\.|\,)?[0-9]*$";
}
