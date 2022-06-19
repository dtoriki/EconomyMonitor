using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.Abstracts.Interfaces.Entities;

/// <summary>
/// Тип сущности денежного потока, произошедшего в течении дня.
/// </summary>
public interface IMoneyDuringDayEntity : IEntity, IMoneyDuringDay
{
    /// <summary>
    /// Возвращает уникальный идентификатор денежного потока за день, в рамках которого создан текущий поток.
    /// </summary>
    Guid MoneyForDayId { get; }
}
