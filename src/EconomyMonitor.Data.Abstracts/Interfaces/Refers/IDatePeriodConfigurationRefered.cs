using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.Abstracts.Interfaces.Refers;

/// <summary>
/// Тип, ссылающийся на конфигурацию периода дат в хранилище данных.
/// </summary>
/// <remarks>
/// Наследует <see cref="IDatePeriodConfigured"/>.
/// </remarks>
public interface IDatePeriodConfigurationRefered : IDatePeriodConfigured
{
    /// <summary>
    /// Возвращает уникальный идентификатор сущности конфигурации периода дат, 
    /// хранящейся в хранилище данных.
    /// </summary>
    Guid? DatePeriodOptionId { get; }
}
