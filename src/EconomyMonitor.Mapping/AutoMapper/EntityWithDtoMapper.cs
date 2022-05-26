using AutoMapper;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Mapping.AutoMapper;

/// <summary>
/// Тип сопоставления сущностей с объектами передачи данных.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public sealed class EntityWithDtoMapper : Mapper, IEntityWithDtoMapper
{
    /// <summary>
    /// Creates экземпляр типа сопоставления сущностей с объектами передачи данных.
    /// </summary>
    /// <param name="configurationProvider">Объект конфигурации.</param>
    /// <exception cref="ArgumentNullException">
    /// Вызывается, когда <paramref name="configurationProvider"/> является <see langword="null"/> ссылкой.
    /// </exception>
    public EntityWithDtoMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
        _ = ThrowIfArgumentNull(configurationProvider);
    }
}
