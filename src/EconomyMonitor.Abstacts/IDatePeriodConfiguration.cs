namespace EconomyMonitor.Abstacts;

/// <summary>
/// Тип конфигурации периода дат.
/// </summary>
/// <remarks>
/// Наследует <see cref="IMonthDay"/>.
/// </remarks>
public interface IDatePeriodConfiguration
{
    /// <summary>
    /// Возвращает дату начала периода включительно.
    /// </summary>
    DateOnly StartPeriodDateInclusive { get; }

    /// <summary>
    /// Возвращает дату окончания периода исключительно.
    /// </summary>
    DateOnly EndPeriodDateExclusive { get; }


    ///// <summary>
    ///// Возвращает <see langword="true"/>, если текущий вариант конфигурации
    ///// является конфигурацией по-умолчанию, иначе - <see langword="false"/>.
    ///// </summary>
    //bool IsDefault { get; }

    ///// <summary>
    ///// Возвращает перечисление дней, разделяющих период.
    ///// </summary>
    //IEnumerable<IMonthDay> PeriodSplits { get; }

    ///// <summary>
    ///// Возвращает перечисление лимитов трат.
    ///// </summary>
    //IEnumerable<ISpendingQuota> SpendingQuotas { get; }
}
