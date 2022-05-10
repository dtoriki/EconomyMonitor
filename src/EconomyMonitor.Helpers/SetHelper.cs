using System.Runtime.CompilerServices;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Helpers;

/// <summary>
/// Helps set fields with values.
/// </summary>
public static class SetHelper
{
    private const string VALUE_ARGUMENT_NAME = "value";

    /// <summary>
    /// Check <paramref name="value"/> for <see langword="null"/> reference and sets <paramref name="field"/> with it when it is not <see langword="null"/>. 
    /// </summary>
    /// <typeparam name="T">Field's type.</typeparam>
    /// <param name="field">Changing field.</param>
    /// <param name="value">New field's value.</param>
    /// <param name="argumentName">Field's argument name.</param>
    /// <param name="callback">Invoke after setting <paramref name="value"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="field"/> has chanched, otherwise - <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">Throws <see cref="ArgumentNullException"/> when <paramref name="field"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// This method recursive safety.
    /// </item>
    /// <item>
    /// Calls <paramref name="callback"/> after setting when it is not <see langword="null"/>.
    /// </item>
    /// <item>
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="value"/> is <see langword="null"/>.
    /// </item>
    /// </list>
    /// </remarks>
    public static bool NullCheckSet<T>
        (ref T? field,
        T value,
        [CallerArgumentExpression(VALUE_ARGUMENT_NAME)] string? argumentName = null,
        Action? callback = null)
    {
        ThrowIfArgumentNull(value, argumentName);

        return Set(ref field, value, callback);
    }

    /// <summary>
    /// Sets <paramref name="field"/> with <paramref name="value"/>. 
    /// </summary>
    /// <typeparam name="T">Field's type.</typeparam>
    /// <param name="field">Changing field.</param>
    /// <param name="value">New field's value.</param>
    /// <param name="callback">Invoke after setting <paramref name="value"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="field"/> has changed, otherwise - <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// This method recursive safety.
    /// </item>
    /// <item>
    /// Calls <paramref name="callback"/> after setting when it is not <see langword="null"/>.
    /// </item>
    /// </list>
    /// </remarks>
    public static bool Set<T>(ref T? field, T? value, Action? callback = null)
    {
        if (field?.Equals(value) ?? value is null)
        {
            return false;
        }

        field = value;
        callback?.Invoke();

        return true;
    }
}
