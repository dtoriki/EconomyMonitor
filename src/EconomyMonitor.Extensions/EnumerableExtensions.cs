using System.Collections;
using System.Runtime.CompilerServices;
using EconomyMonitor.Helpers;

namespace EconomyMonitor.Extensions;

/// <summary>
/// Содержит методы расширения для последовательностей элементов <see cref="IEnumerable"/> и <see cref="IEnumerable{T}"/>
/// </summary>
/// <exception cref="NullReferenceException"/>
public static class EnumerableExtensions
{
    private const string SEQUENCE_ARGUMENT_NAME = "enumerable";

    /// <summary>
    /// Вызывает исключение <see cref="NullReferenceException"/>, 
    /// если последовательность элементов <paramref name="sequence"/> содержит <see langword="null"/> ссылки
    /// </summary>
    /// <param name="enumerable">Последовательность элементов.</param>
    /// <param name="argumentName">Наименование аргумента.</param>
    /// <returns>
    /// <see langword="false"/> если в последовательности <paramref name="enumerable"/> нет <see langword="null"/> ссылок,
    /// иначе вызывает <see cref="NullReferenceException"/>.
    /// </returns>
    /// <exception cref="NullReferenceException">
    /// Вызывается, если в последовательности элементов <paramref name="enumerable"/> есть <see langword="null"/> ссылки.
    /// </exception>
    public static bool ThrowIfAnyItemIsNull(
        this IEnumerable enumerable,
        [CallerArgumentExpression(SEQUENCE_ARGUMENT_NAME)] string? argumentName = null)
    {
        return ThrowHelper.ThrowIfAnyItemIsNull(enumerable, argumentName);
    }
}
