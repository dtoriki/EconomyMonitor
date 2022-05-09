using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static EconomyMonitor.Helpers.ThrowHelper;
using static EconomyMonitor.Literals.ExceptionMessages;

namespace EconomyMonitor.Helpers;

/// <summary>
/// Helps set fields with values.
/// </summary>
public static class ArgsHelper
{
    private const string VALUE_ARGUMENT_NAME = "value";
    private const string SEQUENCE_ARGUMENT_NAME = "sequence";

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
        ThrowIfNull(value, argumentName);

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

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="value"/> is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T"><paramref name="value"/>'s type.</typeparam>
    /// <param name="value">Value.</param>
    /// <param name="argumentName">Argument name.</param>
    /// <returns>
    /// <see langword="false"/> if <paramref name="value"/> is not <see langword="null"/>,
    /// otherwise throws an <see cref="ArgumentNullException"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Throw when <paramref name="value"/> is <see langword="null"/>.</exception>
    public static bool ThrowIfNull<T>(
        [NotNullWhen(false)] T? value,
        [CallerArgumentExpression(VALUE_ARGUMENT_NAME)] string? argumentName = null)
    {
        if (value is null)
        {
            Throw<ArgumentNullException>(argumentName);
        }

        return false;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="value"/>
    /// is out of bounds.
    /// </summary>
    /// <param name="value">
    /// <see cref="int"/> value.
    /// </param>
    /// <param name="permissibleMinimum">
    /// The minimum that a <paramref name="value"/> can be.
    /// </param>
    /// <param name="permissibleMaximum">
    /// The maximum that a <paramref name="value"/> can be.
    /// </param>
    /// <param name="argumentName">Argument name.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Throws when <paramref name="value"/> is out of bounds.
    /// </exception>
    public static void ThrowIfIntArgumentOutOfRange(
        int value,
        int permissibleMinimum = 0,
        int permissibleMaximum = int.MaxValue,
        [CallerArgumentExpression(VALUE_ARGUMENT_NAME)] string? argumentName = null)
    {
        if (value < permissibleMinimum)
        {
            Throw<ArgumentOutOfRangeException>(
                argumentName,
                string.Format(ARGUMENT_OUT_OF_RANGE_CANT_BE_LOWER,
                              argumentName,
                              permissibleMinimum));
        }

        if (value > permissibleMaximum)
        {
            Throw<ArgumentOutOfRangeException>(
                argumentName,
                string.Format(ARGUMENT_OUT_OF_RANGE_CANT_BE_HIGHER,
                              argumentName,
                              permissibleMaximum));
        }
    }

    /// <summary>
    /// Throws <see cref="IndexOutOfRangeException"/> when <paramref name="value"/>
    /// is lower than zero.
    /// </summary>
    /// <param name="value">
    /// <see cref="int"/> index.
    /// </param>
    /// <param name="argumentName">Argument name.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Throws when <paramref name="value"/> is out of bounds.
    /// </exception>
    public static void ThrowIfIndexLowerZero(int index)
    {
        if (index < 0)
        {
            Throw<IndexOutOfRangeException>(INDEX_CANT_BE_LOWER_TAHN_ZERO);
        }
    }

    /// <summary>
    /// Invokes <paramref name="action"/> when <paramref name="value"/> is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T"><paramref name="value"/>'s type.</typeparam>
    /// <param name="value">Value.</param>
    /// <param name="action">Action who invokes when <paramref name="value"/> is <see langword="null"/>..</param>
    /// <exception cref="ArgumentNullException">Throw when <paramref name="action"/> is <see langword="null"/>.</exception>
    public static void DoIfNull<T>(T? value, Action action)
    {
        ThrowIfNull(action);
        if (value is null)
        {
            action();
        }
    }

    /// <summary>
    /// Throws <see cref="NullReferenceException"/> 
    /// if any item in sequence <paramref name="sequence"/> is <see langword="null"/>
    /// </summary>
    /// <param name="sequence">Sequence.</param>
    /// <param name="argumentName">Argument's name.</param>
    /// <returns>
    /// <see langword="false"/> if all items are not <see langword="null"/>,
    /// otherwise throws <see cref="NullReferenceException"/>.
    /// </returns>
    /// <exception cref="NullReferenceException"/>
    public static bool ThrowIfAnyItemIsNull(
        IEnumerable sequence,
        [CallerArgumentExpression(SEQUENCE_ARGUMENT_NAME)] string? argumentName = null)
    {
        foreach (object enumeable in sequence)
        {
            if (enumeable is null)
            {
                Throw<NullReferenceException>(string.Format(SEQUENCE_HAS_NULL_REFERENCE, argumentName));

                return true;
            }
        }

        return false;
    }
}