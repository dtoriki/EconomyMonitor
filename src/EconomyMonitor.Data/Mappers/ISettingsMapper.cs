using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Data.Mappers;

/// <summary>
/// Конвертирует типы <see cref="ISettings"/>.
/// </summary>
public interface ISettingsMapper : IMapper
{
    /// <summary>
    /// Конвертирует экземпляр <paramref name="settings"/> в экземпляр типа <typeparamref name="TDestination"/>.
    /// </summary>
    /// <typeparam name="TDestination">Тип в который конвертируется <paramref name="settings"/>.</typeparam>
    /// <param name="settings">Экземпляр <see cref="ISettings"/>.</param>
    /// <returns>Сконвертированный экземпляр типа <typeparamref name="TDestination"/>.</returns>
    [return: NotNullIfNotNull("settings")]
    TDestination? Map<TDestination>(ISettings? settings) where TDestination : class, ISettings;

    /// <summary>
    /// Переносит значения свойств экземпляра <paramref name="pourFrom"/>
    /// в соответствующие свойства экземпляра <paramref name="pourTo"/>. 
    /// </summary>
    /// <typeparam name="TDestination">Тип перезаписываемого экземпляра.</typeparam>
    /// <param name="pourFrom">Экземпляр, значения свойств которого, переносятся. </param>
    /// <param name="pourTo">Экземпляр, в котороый переносятся значения свойств.</param>
    /// <param name="keySelector">Делегат выбора переносимых свойств.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("pourFrom")]
    TDestination? Pour<TDestination, TKey>(ISettings? pourFrom, TDestination pourTo, Func<ISettings, TKey> keySelector) where TDestination : class, ISettings;

    /// <summary>
    /// Переносит значения свойств экземпляра <paramref name="pourFrom"/>
    /// в соответствующие свойства экземпляра <paramref name="pourTo"/>. 
    /// </summary>
    /// <typeparam name="TDestination">Тип перезаписываемого экземпляра.</typeparam>
    /// <param name="pourFrom">Экземпляр, значения свойств которого, переносятся. </param>
    /// <param name="pourTo">Экземпляр, в котороый переносятся значения свойств.</param>
    /// <returns></returns>
    [return: NotNullIfNotNull("pourFrom")]
    TDestination? Pour<TDestination>(ISettings? pourFrom, TDestination pourTo) where TDestination : class, ISettings;
}
