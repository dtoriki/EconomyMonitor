using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Entities;
using DatePeriodDto = EconomyMonitor.Domain.DatePeriod;

namespace EconomyMonitor.Mapping.AutoMapper.DatePeriod;

/// <summary>
/// Профиль сопоставления экземпляров типов, реализующих <see cref="IDatePeriod"/>, между собой.
/// </summary>
/// <remarks>
/// <para>
/// Наследует <see cref="Profile"/>.
/// </para>
/// <para>
/// От этого типа нельзя унаследоваться.
/// </para>
/// </remarks>
public sealed class DatePeriodMapProfile : Profile
{
    /// <summary>
    /// Создаёт профиль сопоставления экземпляров типов, реализующих <see cref="IDatePeriod"/>, между собой.
    /// </summary>
    public DatePeriodMapProfile() : base() => SetDatePeriodMap();

    private void SetDatePeriodMap()
    {
        CreateMap<IDatePeriod, DatePeriodDto>();
        CreateMap<IDatePeriod, DatePeriodEntity>();
    }
}
