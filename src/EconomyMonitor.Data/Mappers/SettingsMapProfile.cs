using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Domain;
using EconomyMonitor.Data.Entities;

namespace EconomyMonitor.Data.Mappers;

internal sealed class SettingsMapProfile : Profile
{
    public SettingsMapProfile()
    {
        CreateMap<ISettings, Settings>();
        CreateMap<ISettings, SettingsEntity>();
    }
}
