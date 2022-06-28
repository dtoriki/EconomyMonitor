using AutoMapper;
using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.Mappers;

internal interface ISettingsMapper : IMapper
{
    TDestanation? Map<TDestanation>(ISettings? settings) where TDestanation : class, ISettings;
}
