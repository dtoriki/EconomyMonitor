using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Mapping.AutoMapper;

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
}
