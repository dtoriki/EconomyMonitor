namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип денежного потока за день.
/// </summary>
/// <remarks>
/// Наследует <see cref="IMoney"/>, <see cref="IForDay"/>.
/// </remarks>
public interface IMoneyForDay : IMoney, IForDay
{
    /// <summary>
    /// Возвращает коллекцию доходов в течении дня.
    /// </summary>
    IMoneyFlow<IMoneyDuringDay> Earnings { get; }

    /// <summary>
    /// Возвращает коллекцию рассходов в течении дня.
    /// </summary>
    IMoneyFlow<IMoneyDuringDay> Expenses { get; }
}
