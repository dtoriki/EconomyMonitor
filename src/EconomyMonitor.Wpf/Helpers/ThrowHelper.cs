using System.Diagnostics.CodeAnalysis;
using static EconomyMonitor.Wpf.Helpers.Internal.ExceptionMessages;

namespace EconomyMonitor.Wpf.Helpers;

public static class ThrowHelper
{
    /// <summary>
    /// Throws exception.
    /// </summary>
    /// <typeparam name="T">
    /// Exception's type.
    /// </typeparam>
    /// <param name="args">
    /// Arguments' array.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Throws when <paramref name="args"/> are empty. Or when exception is obscure.
    /// </exception>
    /// <remarks>
    /// <paramref name="args"/> should include at least one argument as message.
    /// </remarks>
    [DoesNotReturn]
    public static void Throw<T>(params object?[] args)
        where T : Exception
    {
        ArgsHelper.ThrowIfNull(args, nameof(args));

        if (args.Length < 1)
        {
            Throw<InvalidOperationException>(NEED_AT_LEAST_A_MESSAGE);
        }

        throw (T)(Activator.CreateInstance(typeof(T), args)
            ?? throw new InvalidOperationException(OBSCURE_EXCEPTION_THROWN));
    }
}
