namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип, имеющий свойство конфигурации периода дат.
/// </summary>
public interface IDatePeriodConfigured
{
    /// <summary>
    /// Возвращает конфигурацию периода дат.
    /// </summary>
    IDatePeriodConfiguration? DatePeriodOption { get; }
}
