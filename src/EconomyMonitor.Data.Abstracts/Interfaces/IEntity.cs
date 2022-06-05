using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.Abstracts.Interfaces;

/// <summary>
/// Тип, хранящийся в хранилище данных.
/// </summary>
/// <remarks>Наследует <see cref="IUniqueRequired"/>.</remarks>
public interface IEntity : IUniqueRequired
{
    /// <summary>
    /// Возвращает дату создания сущности.
    /// </summary>
    DateTime DateCreated { get; }

    /// <summary>
    /// Возвращает дату модификации сущности.
    /// </summary>
    DateTime DateModified { get; }
}
