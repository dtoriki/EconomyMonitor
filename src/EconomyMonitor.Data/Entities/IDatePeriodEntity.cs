using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Interfaces;

namespace EconomyMonitor.Data.Entities;

/// <summary>
/// Представляет тип сущности периода дат в таблице базы данных.
/// </summary>
/// <remarks>Наследует: <see cref="IEntity"/>, <see cref="IDatePeriod"/>.</remarks>
public interface IDatePeriodEntity : IEntity, IDatePeriod
{

}
