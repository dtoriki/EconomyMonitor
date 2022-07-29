using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using EconomyMonitor.Abstacts;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Primitives.Comparers;

/// <summary>
/// Предоставляет методы сравнения экземпляров типа <see cref="ISettings"/>.
/// </summary>
/// <remarks>
/// Реализует <see cref="IEqualityComparer{T}"/>.
/// </remarks>
/// <exception cref="ArgumentNullException"/>
public class SettingsEqualityComparer : IEqualityComparer<ISettings>
{
    /// <summary>
    /// Создаёт экземпляр, предоставляющий методы сравнения экземпляров типа <see cref="ISettings"/>.
    /// </summary>
    public SettingsEqualityComparer()
    {

    }

    /// <summary>
    /// Определяет равенство двух экземпляров типа <see cref="ISettings"/>.
    /// </summary>
    /// <param name="x">Первый экземпляр типа <see cref="ISettings"/>.</param>
    /// <param name="y">Второй экземпляр типа <see cref="ISettings"/>.</param>
    /// <returns><see langword="true"/>, если экземпляры равны, иначе - <see langword="false"/>.</returns>
    public bool Equals(ISettings? x, ISettings? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.StartingBudget == y.StartingBudget;
    }

    /// <summary>
    /// Считает и возвращает хэш-код экземляра <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">Экземпляр типа <see cref="ISettings"/>.</param>
    /// <returns>Хэш-код экземпляра <paramref name="obj"/>.</returns>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="obj"/> оказался <see langword="null"/>.
    /// </exception>
    public int GetHashCode([DisallowNull] ISettings obj)
    {
        _ = ThrowIfArgumentNull(obj);

        return HashCode.Combine(obj.StartingBudget);
    }

    /// <summary>
    /// Определяет равенство двух экземпляров типа <see cref="ISettings"/>.
    /// </summary>
    /// <param name="x">Первый экземпляр типа <see cref="ISettings"/>.</param>
    /// <param name="y">Второй экземпляр типа <see cref="ISettings"/>.</param>
    /// <returns><see langword="true"/>, если экземпляры равны, иначе - <see langword="false"/>.</returns>
    public static bool IsEquals(ISettings? x, ISettings? y)
    {
        return new SettingsEqualityComparer().Equals(x, y);
    }
}
