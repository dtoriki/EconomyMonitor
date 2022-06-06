using System.Diagnostics.CodeAnalysis;
using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Mapping.AutoMapper.DatePeriodConfiguration;

/// <summary>
/// Тип сопоставления сущностей типа <see cref="IDatePeriodConfiguration"/>.
/// </summary>
public interface IDatePeriodConfigurationMapper
{
    /// <summary>
    /// Сопоставляет экземпляр типа <typeparamref name="TSource"/>,
    /// реализующий <see cref="IDatePeriodConfiguration"/>,
    /// с объектом <typeparamref name="TDistanation"/>,
    /// реализующий <see cref="IDatePeriodConfiguration"/>,
    /// записывая значения свойств экзмпляра <paramref name="source"/>,
    /// в аналогичные свойства нового экземпляра типа <typeparamref name="TDistanation"/>.
    /// </summary>
    /// <typeparam name="TSource">Тип источника.</typeparam>
    /// <typeparam name="TDistanation">Тип результирующего экземпляра.</typeparam>
    /// <param name="source">Источник.</param>
    /// <returns>Сопоставимый экземпляр типа <typeparamref name="TDistanation"/>.</returns>
    [return: NotNullIfNotNull("source")]
    TDistanation? DatePeriodConfigurationMap<TSource, TDistanation>(TSource? source)
        where TSource : class, IDatePeriodConfiguration
        where TDistanation : class, IDatePeriodConfiguration;

    /// <summary>
    /// Сопоставляет экземпляры типа <typeparamref name="TSource"/>,
    /// реализующие <see cref="IDatePeriodConfiguration"/>,
    /// с объектами <typeparamref name="TDistanation"/>,
    /// реализующими <see cref="IDatePeriodConfiguration"/>,
    /// записывая значения свойств экзмпляров <paramref name="source"/>,
    /// в аналогичные свойства новых экземпляров типа <typeparamref name="TDistanation"/>.
    /// </summary>
    /// <typeparam name="TSource">Тип источника.</typeparam>
    /// <typeparam name="TDistanation">Тип результирующего экземпляра.</typeparam>
    /// <param name="source">Источник.</param>
    /// <returns>Сопоставимые экземпляры типа <typeparamref name="TDistanation"/>.</returns>
    IQueryable<TDistanation> DatePeriodConfigurationMap<TSource, TDistanation>(IQueryable<TSource>? source)
        where TSource : class, IDatePeriodConfiguration
        where TDistanation : class, IDatePeriodConfiguration;
}
