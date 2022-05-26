using EconomyMonitor.Data.Abstracts.Interfaces;

namespace EconomyMonitor.Data.Abstracts.Base;

/// <summary>
/// Представляет базовую сущность, хранимую в хранилище данных.
/// </summary>
/// <remarks>
/// Реализует <see cref="IEntity"/>.
/// </remarks>
public abstract class EntityBase : IEntity
{
    /// <summary>
    /// Возвращает или задаёт уникальный идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Возвращает или устанавливает дату создания сущности.
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Возвращает или задаёт дату модификации сущности.
    /// </summary>
    public DateTime DateModified { get; set; }
}
