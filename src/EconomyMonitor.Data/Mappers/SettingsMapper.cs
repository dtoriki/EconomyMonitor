using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Mapping.AutoMapper;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data.Mappers;

internal sealed class SettingsMapper : AutoMapperBase, ISettingsMapper
{
    public SettingsMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
    }

    [return: NotNullIfNotNull("settings")]
    public TDestination? Map<TDestination>(ISettings? settings)
        where TDestination : class, ISettings
    {
        if (settings is null)
        {
            return null;
        }

        return Mapper.Map<TDestination>(settings);
    }

    [return: NotNullIfNotNull("pourFrom")]
    public TDestination? Pour<TDestination, TKey>(ISettings? pourFrom, TDestination pourTo, Func<ISettings, TKey> keySelector)
         where TDestination : class, ISettings
    {
        _ = ThrowIfArgumentNull(keySelector);
        _ = ThrowIfArgumentNull(pourTo);
        if (pourFrom is null)
        {
            return pourTo;
        }

        Type? selectorType = keySelector
            .Invoke(pourTo)?
            .GetType();
        if (selectorType is null)
        {
            Throw<InvalidOperationException>("Не удалось получить свойства для изменения.");
        }

        return Pour(pourFrom, pourTo, selectorType);
    }

    [return: NotNullIfNotNull("pourFrom")]
    public TDestination? Pour<TDestination>(ISettings? pourFrom, TDestination pourTo)
         where TDestination : class, ISettings
    {
        _ = ThrowIfArgumentNull(pourTo);
        if (pourFrom is null)
        {
            return pourTo;
        }

        Type pourToType = pourTo.GetType();
        
        return Pour(pourFrom, pourTo, pourToType);
    }

    [return: NotNullIfNotNull("pourFrom")]
    private static TDestination Pour<TDestination>(ISettings pourFrom, TDestination pourTo, Type type)
         where TDestination : class, ISettings
    {
        string[] interfacePropertyNames = typeof(ISettings)
            .GetProperties()
            .Select(x => x.Name)
            .ToArray();

        string[] propertyNames = type
                .GetProperties()
                .Where(prop => interfacePropertyNames.Any(name => name == prop.Name))
                .Select(prop => prop.Name)
                .ToArray();

        foreach (string propertyName in propertyNames)
        {
            PropertyInfo? pourToProperty = type.GetProperty(propertyName);

            if (pourToProperty?.CanWrite is null or false)
            {
                Throw<InvalidOperationException>($"Свойство {propertyName} только для чтения.");
            }

            PropertyInfo? pourFromProperty = pourFrom
                .GetType()
                .GetProperty(propertyName);

            if (pourFromProperty is null)
            {
                Throw<InvalidOperationException>($"Отсутствует свойство {propertyName}.");
            }

            object? valueFrom = pourFromProperty.GetValue(pourFrom);
            pourToProperty.SetValue(pourTo, valueFrom);
        }

        return pourTo;
    }
}
