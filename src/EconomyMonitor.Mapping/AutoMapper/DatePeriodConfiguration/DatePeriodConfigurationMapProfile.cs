using AutoMapper;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Entities;
using DatePeriodConfigurationDto = EconomyMonitor.Domain.DatePeriodConfiguration;

namespace EconomyMonitor.Mapping.AutoMapper.DatePeriodConfiguration;

/// <summary>
/// Профиль сопоставления экземпляров типов, реализующих <see cref="IDatePeriodConfiguration"/>, между собой.
/// </summary>
/// <remarks>
/// <para>
/// Наследует <see cref="Profile"/>.
/// </para>
/// <para>
/// От этого типа нельзя унаследоваться.
/// </para>
/// </remarks>
public sealed class DatePeriodConfigurationMapProfile : Profile
{
    /// <summary>
    /// Создаёт профиль сопоставления экземпляров типов, реализующих <see cref="IDatePeriodConfiguration"/>, между собой.
    /// </summary>
    public DatePeriodConfigurationMapProfile() : base() => CreateMaps();

    private void CreateMaps()
    {
        CreateMap<IDatePeriodConfiguration, DatePeriodConfigurationEntity>();
        CreateMap<IDatePeriodConfiguration, DatePeriodConfigurationDto>();
        CreateMap<ISpendingQuota, SpendingQuotaEntity>();
    }
}
