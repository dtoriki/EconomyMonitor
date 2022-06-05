using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.Refers;

namespace EconomyMonitor.Data.Abstracts.Interfaces.Entities;

/// <summary>
/// Тип сущности лимита трат.
/// </summary>
/// <remarks>
/// Наследует <see cref="IEntity"/>, <see cref="ISpendingQuota"/>, <see cref="IDatePeriodConfigurationRefered"/>.
/// </remarks>
public interface ISpendingQuotaEntity : IEntity, ISpendingQuota, IDatePeriodConfigurationRefered
{

}
