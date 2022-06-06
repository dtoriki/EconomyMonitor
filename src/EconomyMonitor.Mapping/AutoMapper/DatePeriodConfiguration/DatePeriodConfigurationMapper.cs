using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using EconomyMonitor.Abstacts;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Mapping.AutoMapper.DatePeriodConfiguration;

/// <summary>
/// Сопоставление типов <see cref="IDatePeriodConfiguration"/>.
/// </summary>
/// <remarks>
/// <para>
/// От этого типа нельзя унаследоваться.
/// </para>
/// <para>
/// Наследует <see cref="AutoMapperBase"/>.
/// </para>
/// <para>
/// Реализует <see cref="IDatePeriodConfiguration"/>.
/// </para>
/// </remarks>
/// <exception cref="ArgumentNullException"/>
public sealed class DatePeriodConfigurationMapper : AutoMapperBase, IDatePeriodConfigurationMapper
{
    /// <summary>
    /// Создаёт экземпляр сопоставления типов <see cref="IDatePeriodConfigurationMapper"/>.
    /// </summary>
    /// <param name="configurationProvider">Объект конфигурации.</param>
    /// <exception cref="ArgumentNullException">
    /// Вызывается, когда <paramref name="configurationProvider"/> является <see langword="null"/> ссылкой.
    /// </exception>
    public DatePeriodConfigurationMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
    {
        _ = ThrowIfArgumentNull(configurationProvider);
    }

    /// <inheritdoc/>
    [return: NotNullIfNotNull("source")]
    public TDistanation? DatePeriodConfigurationMap<TSource, TDistanation>(TSource? source)
        where TSource : class, IDatePeriodConfiguration
        where TDistanation : class, IDatePeriodConfiguration
    {
        if (source is null)
        {
            return null;
        }

        return Mapper.Map<TSource, TDistanation>(source);
    }

    /// <inheritdoc/>
    public IQueryable<TDistanation> DatePeriodConfigurationMap<TSource, TDistanation>(IQueryable<TSource>? source)
        where TSource : class, IDatePeriodConfiguration
        where TDistanation : class, IDatePeriodConfiguration

    {
        if (source is null)
        {
            return Array.Empty<TDistanation>()
                .AsQueryable();
        }

        return Mapper.ProjectTo<TDistanation>(source);
    }
}
