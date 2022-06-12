using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EconomyMonitor.Extensions;

/// <summary>
/// Методы расширения именованных констант <see cref="Enum"/>.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Возвращает описание <see cref="DisplayAttribute.Description"/>
    /// именованной константы <paramref name="enum"/>.
    /// </summary>
    /// <typeparam name="TEnum">Тип именованной константы.</typeparam>
    /// <param name="enum">Именованная константа.</param>
    /// <returns>Описание именованной константы.</returns>
    public static string? GetDisplayDescription<TEnum>(this TEnum @enum)
        where TEnum : Enum
    {
        return @enum.GetType()
            .GetMember(@enum.ToString())
            .Single()
            .GetCustomAttribute<DisplayAttribute>()?
            .Description;
    }
}
