namespace EconomyMonitor.Helpers;

/// <summary>
/// Содержит методы, высвобождающие объекты.
/// </summary>
public static class DisposeHelper
{
    /// <summary>
    /// Высвобождает объект <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">Высвобождаемый объект.</param>
    /// <returns><see langword="true"/>, если объект был высвобожден, иначе - <see langword="false"/>.</returns>
    public static bool Dispose(object? obj)
    {
        if (obj is IDisposable disposable)
        {
            disposable.Dispose();

            return true;
        }

        return false;
    }

    /// <summary>
    /// Асинхронно высвобождает объект <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">Высвобождаемый объект.</param>
    /// <returns><see langword="true"/>, если объект был высвобожден, иначе - <see langword="false"/>.</returns>
    public static async ValueTask<bool> DisposeAsync(object? obj)
    {
        if (obj is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync().ConfigureAwait(false);

            return true;
        }

        return Dispose(obj);
    }
}
