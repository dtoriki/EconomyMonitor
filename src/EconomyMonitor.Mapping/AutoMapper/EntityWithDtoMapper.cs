using AutoMapper;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Mapping.AutoMapper;

/// <summary>
/// Provides mapping for entities with dtos.
/// </summary>
/// <exception cref="ArgumentNullException"/>
public sealed class EntityWithDtoMapper : Mapper, IEntityWithDtoMapper
{
    /// <summary>
    /// Creates mapper for entities with dtos.
    /// </summary>
    /// <param name="configurationProvider">Configuration provider</param>
    /// <exception cref="ArgumentNullException"/>
    public EntityWithDtoMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
        _ = ThrowIfArgumentNull(configurationProvider);
    }
}
