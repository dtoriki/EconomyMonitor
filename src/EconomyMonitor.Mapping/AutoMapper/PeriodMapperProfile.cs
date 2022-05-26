using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Domain;

namespace EconomyMonitor.Mapping.AutoMapper;

/// <summary>
/// Профиль сопоставления сущностей с объектами передачи данных.
/// </summary>
public sealed class EntityWithDtoProfile : Profile
{
    /// <summary>
    /// Creates mapper profile for entities with dtos
    /// </summary>
    public EntityWithDtoProfile() => SetDatePeriodMap();

    private void SetDatePeriodMap()
    {
        CreateMap<DatePeriodEntity, DatePeriod>();
        CreateMap<IDatePeriod, DatePeriodEntity>();
    }
}
