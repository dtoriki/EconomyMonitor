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
    /// Конвертирует экземпляр <paramref name="settings"/> в экземпляр типа <typeparamref name="TDestanation"/>.
    /// </summary>
    /// <typeparam name="TDestanation">Тип в который конвертируется <paramref name="settings"/>.</typeparam>
    /// <param name="settings">Экземпляр <see cref="ISettings"/>.</param>
    /// <returns>Сконвертированный экземпляр типа <typeparamref name="TDestanation"/>.</returns>
    [return: NotNullIfNotNull("settings")]
    TDestanation? Map<TDestanation>(ISettings? settings) where TDestanation : class, ISettings;
}
