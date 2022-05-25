using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;

namespace EconomyMonitor.Data.Entities;

/// <summary>
/// Defines period entity interface.
/// </summary>
/// <remarks>Inherits <see cref="IEntity"/>, <see cref="IDatePeriod"/>.</remarks>
public interface IDatePeriodEntity : IEntity, IDatePeriod
{

}
