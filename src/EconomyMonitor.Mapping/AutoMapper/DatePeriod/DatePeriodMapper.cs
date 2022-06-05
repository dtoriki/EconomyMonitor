using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using EconomyMonitor.Abstacts;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Mapping.AutoMapper.DatePeriod;

/// <summary>
/// Сопоставление типов <see cref="IDatePeriod"/>.
/// </summary>
/// <remarks>
/// <para>
/// От этого типа нельзя унаследоваться.
/// </para>
/// <para>
/// Наследует <see cref="AutoMapperBase"/>.
/// </para>
/// <para>
/// Реализует <see cref="IDatePeriodMapper"/>.
/// </para>
/// </remarks>
/// <exception cref="ArgumentNullException"/>
public sealed class DatePeriodMapper : AutoMapperBase, IDatePeriodMapper
{
    /// <summary>
    /// Создаёт экземпляр сопоставления типов <see cref="IDatePeriod"/>.
    /// </summary>
    /// <param name="configurationProvider">Объект конфигурации.</param>
    /// <exception cref="ArgumentNullException">
    /// Вызывается, когда <paramref name="configurationProvider"/> является <see langword="null"/> ссылкой.
    /// </exception>
    public DatePeriodMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
        _ = ThrowIfArgumentNull(configurationProvider);
    }

    /// <inheritdoc/>
    [return: NotNullIfNotNull("source")]
    public TDistanation? DatePeriodMap<TSource, TDistanation>(TSource? source)
        where TSource : class, IDatePeriod
        where TDistanation : class, IDatePeriod
    {
        if (source is null)
        {
            return null;
        }

        return Mapper.Map<TSource, TDistanation>(source);
    }

    /// <inheritdoc/>
    public IQueryable<TDistanation> DatePeriodMap<TSource, TDistanation>(IQueryable<TSource>? source)
        where TSource : class, IDatePeriod
        where TDistanation : class, IDatePeriod

    {
        if (source is null)
        {
            return Array.Empty<TDistanation>()
                .AsQueryable();
        }

        return Mapper.ProjectTo<TDistanation>(source);
    }
}
