using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Mapping.AutoMapper;

namespace EconomyMonitor.Data.Mappers;

internal sealed class SettingsMapper : AutoMapperBase, ISettingsMapper
{
    public SettingsMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
    }

    public TDestanation? Map<TDestanation>(ISettings? settings)
        where TDestanation : class, ISettings
    {
        if (settings is null)
        {
            return null;
        }

        return Mapper.Map<TDestanation>(settings);
    }
}
