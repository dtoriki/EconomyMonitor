using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Domain;

/// <summary>
/// Период дат.
/// </summary>
public sealed class DatePeriod : IDatePeriod, IUniqueRequired
{
    /// <summary>
    /// Возвращает или задаёт уникальный идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Возвращает или задаёт дату начала периода.
    /// </summary>
    public DateOnly StartingDate { get; set; }

    /// <summary>
    /// Возвращает или задаёт дату окончания периода.
    /// </summary>
    public DateOnly EndingDate { get; set; }

    /// <inheritdoc/>
    public decimal Income { get; set; }
}
