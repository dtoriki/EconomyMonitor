using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.Abstracts.Interfaces.Entities;

/// <summary>
/// Тип сущности денежного потока за день.
/// </summary>
public interface IMoneyForDayEntity : IEntity, IMoneyForDay
{
    /// <summary>
    /// Возвращает коллекцию доходов за день.
    /// </summary>
    new ICollection<IMoneyDuringDayEntity> Earnings { get; }

    /// <summary>
    /// Возвращает коллекцию расходов за день.
    /// </summary>
    new ICollection<IMoneyDuringDayEntity> Expenses { get; }
}
