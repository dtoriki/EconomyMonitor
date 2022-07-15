using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.Mappers;

internal interface ISettingsMapper : IMapper
{
    [return: NotNullIfNotNull("settings")]
    TDestanation? Map<TDestanation>(ISettings? settings) where TDestanation : class, ISettings;
}
