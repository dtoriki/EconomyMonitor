using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Helpers;

/// <summary>
/// Содержит методы для работы с <see cref="Enum"/>.
/// </summary>
/// <exception cref="InvalidOperationException"/>
public static class EnumHelper
{
    /// <summary>
    /// Возвращает именованную константу <see cref="Enum"/>,
    /// по её описанию <see cref="DisplayAttribute.Description"/>.
    /// </summary>
    /// <param name="targetType">Тип именованной константы.</param>
    /// <param name="displayDescription">
    /// Описание именованной константы <see cref="DisplayAttribute.Description"/>.
    /// </param>
    /// <returns>Именованная константа.</returns>
    /// <exception cref="InvalidOperationException">
    /// Возникает когда в типе именованных констант, 
    /// есть больше чем одна именованная константа 
    /// с одним описанием <see cref="DisplayAttribute.Description"/>.
    /// </exception>
    public static Enum? GetEnumValueByDisplayDescription(Type targetType, string displayDescription)
    {
        _ = ThrowIfArgumentNull(displayDescription);

        MemberInfo? memberInfo = targetType
           .GetMembers()
           .SingleOrDefault(m => m.GetCustomAttribute<DisplayAttribute>()?.Description == displayDescription);

        if (memberInfo is null)
        {
            return null;
        }

        Array values = Enum.GetValues(targetType);

        foreach (object value in values)
        {
            if (value.ToString() == memberInfo.Name)
            {
                return value as Enum;
            }    
        }

        return null;
    }
}
