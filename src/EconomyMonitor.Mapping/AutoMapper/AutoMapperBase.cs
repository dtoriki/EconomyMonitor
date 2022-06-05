using System.Linq.Expressions;
using AutoMapper;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Mapping.AutoMapper;

/// <summary>
/// Базовая реализация автосопоставления типов.
/// </summary>
/// <remarks>
/// <para>
/// Оборачивает тип автосопоставления <see cref="IMapper"/> 
/// и предоставляет экземпляр этого типа наследникам.
/// При этом скрывает реализацию этого типа.
/// </para>
/// <para>
/// Реализует <see cref="IMapper"/>.
/// </para>
/// </remarks>
public abstract class AutoMapperBase : IMapper
{
    /// <summary>
    /// Возвращает экземпляр типа сопоставления.
    /// </summary>
    protected IMapper Mapper { get; }

    /// <summary>
    /// Создаёт экземпляр автосопоставления типов.
    /// </summary>
    /// <param name="configurationProvider">Объект конфигурации.</param>
    /// <exception cref="ArgumentNullException">
    /// Вызывается, когда <paramref name="configurationProvider"/> является <see langword="null"/> ссылкой.
    /// </exception>
    protected AutoMapperBase(IConfigurationProvider configurationProvider)
    {
        _ = ThrowIfArgumentNull(configurationProvider);

        Mapper = new Mapper(configurationProvider);
    }

    IConfigurationProvider IMapper.ConfigurationProvider => Mapper.ConfigurationProvider;

    TDestination IMapper.Map<TDestination>(
        object source, 
        Action<IMappingOperationOptions<object, TDestination>> opts) => Mapper.Map(source, opts);

    TDestination IMapper.Map<TSource, TDestination>(
        TSource source, 
        Action<IMappingOperationOptions<TSource, TDestination>> opts) => Mapper.Map(source, opts);

    TDestination IMapper.Map<TSource, TDestination>(
        TSource source, 
        TDestination destination, 
        Action<IMappingOperationOptions<TSource, TDestination>> opts) => Mapper.Map(source, destination, opts);

    object IMapper.Map(
        object source, 
        Type sourceType, 
        Type destinationType, 
        Action<IMappingOperationOptions<object, object>> opts) => Mapper.Map(source, sourceType, destinationType, opts);

    object IMapper.Map(
        object source, 
        object destination, 
        Type sourceType, 
        Type destinationType, 
        Action<IMappingOperationOptions<object, object>> opts) => Mapper.Map(source, destination, sourceType, destinationType, opts);

    TDestination IMapperBase.Map<TDestination>(object source) => Mapper.Map<TDestination>(source);

    TDestination IMapperBase.Map<TSource, TDestination>(TSource source) => Mapper.Map<TSource, TDestination>(source);

    TDestination IMapperBase.Map<TSource, TDestination>(
        TSource source, 
        TDestination destination) => Mapper.Map(source, destination);

    object IMapperBase.Map(
        object source, 
        Type sourceType, 
        Type destinationType) => Mapper.Map(source, sourceType, destinationType);

    object IMapperBase.Map(
        object source, 
        object destination, 
        Type sourceType, 
        Type destinationType) => Mapper.Map(source, destination, sourceType, destinationType);

    IQueryable<TDestination> IMapper.ProjectTo<TDestination>(
        IQueryable source,
        object? parameters,
        params Expression<Func<TDestination, object>>[] membersToExpand) => Mapper.ProjectTo(source, parameters, membersToExpand);

    IQueryable<TDestination> IMapper.ProjectTo<TDestination>(
        IQueryable source, 
        IDictionary<string, object> parameters, 
        params string[] membersToExpand) => Mapper.ProjectTo<TDestination>(source, parameters, membersToExpand);

    IQueryable IMapper.ProjectTo(
        IQueryable source, 
        Type destinationType,
        IDictionary<string, object>? parameters,
        params string[] membersToExpand) => Mapper.ProjectTo(source, destinationType, parameters, membersToExpand);
}
