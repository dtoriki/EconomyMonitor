using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Domain;

namespace EconomyMonitor.Mapping.AutoMapper;

/// <summary>
/// Economy monitor mapper profile for entities with dtos.
/// </summary>
public sealed class EntityWithDtoProfile : Profile
{
    /// <summary>
    /// Creates mapper profile for entities with dtos
    /// </summary>
    public EntityWithDtoProfile()
    {
        SetPeriodMap();
    }

    private void SetPeriodMap()
    {
        CreateMap<PeriodEntity, Period>();
        CreateMap<IPeriod, PeriodEntity>();
    }
}
