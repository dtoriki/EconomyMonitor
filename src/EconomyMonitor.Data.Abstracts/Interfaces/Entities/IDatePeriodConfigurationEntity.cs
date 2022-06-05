using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;

namespace EconomyMonitor.Data.Abstracts.Interfaces.Entities;

/// <summary>
/// Тип сущности конфигурации периода дат.
/// </summary>
/// <remarks>
/// Наследует <see cref="IEntity"/>, <see cref="IDatePeriodConfiguration"/>.
/// </remarks>
public interface IDatePeriodConfigurationEntity : IEntity, IDatePeriodConfiguration
{

}
