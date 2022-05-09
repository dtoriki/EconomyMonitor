using System.Collections;
using System.Runtime.CompilerServices;
using EconomyMonitor.Helpers;

namespace EconomyMonitor.Extensions;

/// <summary>
/// Contains extensions for <see cref="IEnumerable"/> and <see cref="IEnumerable{T}"/>
/// </summary>
public static class EnumerableExtensions
{
    private const string SEQUENCE_ARGUMENT_NAME = "enumerable";

    /// <summary>
    /// Throws <see cref="NullReferenceException"/> 
    /// if any item in sequence <paramref name="sequence"/> is <see langword="null"/>
    /// </summary>
    /// <param name="enumerable">Sequence.</param>
    /// <param name="argumentName">Argument's name.</param>
    /// <returns>
    /// <see langword="false"/> if all items are not <see langword="null"/>,
    /// otherwise throws <see cref="NullReferenceException"/>.
    /// </returns>
    /// <exception cref="NullReferenceException"/>
    public static bool ThrowIfAnyItemIsNull(
        this IEnumerable enumerable,
        [CallerArgumentExpression(SEQUENCE_ARGUMENT_NAME)] string? argumentName = null)
    {
        return ArgsHelper.ThrowIfAnyItemIsNull(enumerable, argumentName);
    }
}
