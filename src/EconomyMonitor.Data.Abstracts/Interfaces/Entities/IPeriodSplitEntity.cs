using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.Refers;

namespace EconomyMonitor.Data.Abstracts.Interfaces.Entities;

/// <summary>
/// Тип сущности, которая содержит информацию о разделении периода на части.
/// </summary>
/// <remarks>
/// Наследует <see cref="IEntity"/>, <see cref="IMonthDay"/>, <see cref="IDatePeriodConfigurationRefered"/>.
/// </remarks>
public interface IPeriodSplitEntity : IEntity, IMonthDay, IDatePeriodConfigurationRefered
{

}
