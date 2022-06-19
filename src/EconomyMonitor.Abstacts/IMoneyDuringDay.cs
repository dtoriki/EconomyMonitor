namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип денежного потока, произошедшего в течении дня.
/// </summary>
/// <remarks>
/// Наследует <see cref="IMoney"/>, <see cref="IDuringDay"/>.
/// </remarks>
public interface IMoneyDuringDay : IMoney, IDuringDay
{
    /// <summary>
    /// Возвращает денежный поток за день, в рамках которого был создан текщий денежный поток. 
    /// </summary>
    IMoneyForDay MoneyFlowForDay { get; }
}
