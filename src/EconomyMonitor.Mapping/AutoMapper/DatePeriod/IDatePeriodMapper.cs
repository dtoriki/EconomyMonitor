using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Mapping.AutoMapper.DatePeriod;

/// <summary>
/// Тип сопоставления сущностей типа <see cref="IDatePeriod"/>.
/// </summary>
public interface IDatePeriodMapper
{
    /// <summary>
    /// Сопоставляет экземпляр типа <typeparamref name="TSource"/>,
    /// реализующий <see cref="IDatePeriod"/>,
    /// с объектом <typeparamref name="TDistanation"/>,
    /// реализующий <see cref="IDatePeriod"/>,
    /// записывая значения свойств экзмпляра <paramref name="source"/>,
    /// в аналогичные свойства нового экземпляра типа <typeparamref name="TDistanation"/>.
    /// </summary>
    /// <typeparam name="TSource">Тип источника.</typeparam>
    /// <typeparam name="TDistanation">Тип результирующего экземпляра.</typeparam>
    /// <param name="source">Источник.</param>
    /// <returns>Сопоставимый экземпляр типа <typeparamref name="TDistanation"/>.</returns>
    [return: NotNullIfNotNull("source")]
    TDistanation? DatePeriodMap<TSource, TDistanation>(TSource? source)
        where TSource : class, IDatePeriod
        where TDistanation : class, IDatePeriod;

    /// <summary>
    /// Сопоставляет экземпляры типа <typeparamref name="TSource"/>,
    /// реализующие <see cref="IDatePeriod"/>,
    /// с объектами <typeparamref name="TDistanation"/>,
    /// реализующими <see cref="IDatePeriod"/>,
    /// записывая значения свойств экзмпляров <paramref name="source"/>,
    /// в аналогичные свойства новых экземпляров типа <typeparamref name="TDistanation"/>.
    /// </summary>
    /// <typeparam name="TSource">Тип источника.</typeparam>
    /// <typeparam name="TDistanation">Тип результирующего экземпляра.</typeparam>
    /// <param name="source">Источник.</param>
    /// <returns>Сопоставимые экземпляры типа <typeparamref name="TDistanation"/>.</returns>
    IQueryable<TDistanation> DatePeriodMap<TSource, TDistanation>(IQueryable<TSource>? source)
        where TSource : class, IDatePeriod
        where TDistanation : class, IDatePeriod;
}
