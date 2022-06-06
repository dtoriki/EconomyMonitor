using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Domain;

/// <summary>
/// Лимит трат.
/// </summary>
/// <remarks>
/// <para>
/// Этот тип нельзя унаследовать.
/// </para>
/// <para>
/// Реализует <see cref="ISpendingQuota"/>.
/// </para>
/// </remarks>
public sealed class SpendingQuota : ISpendingQuota
{
    /// <summary>
    /// Возвращает или задаёт значение лимита трат.
    /// </summary>
    public decimal Quota { get; set; }

    /// <summary>
    /// Возвращает или задаёт уникальный идентификатор сущности конфигурации периода дат, 
    /// хранящейся в хранилище данных.
    /// </summary>
    public Guid? DatePeriodOptionId { get; set; }

    /// <summary>
    /// Возвращает или задаёт сущность кофигурации периода дат.
    /// </summary>
    public DatePeriodConfiguration? DatePeriodOption { get; set; }

    /// <summary>
    /// Создаёт лимит трат.
    /// </summary>
    public SpendingQuota()
    {

    }
}
