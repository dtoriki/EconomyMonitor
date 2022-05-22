using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Services.Exceptions;

/// <summary>
/// Presents an <see cref="Exception"/> of missing DI service in <see cref="IServiceCollection"/>.
/// </summary>
/// <remarks>
/// Has default message: "The "<see cref="Type.FullName"/>" type service not found.".
/// </remarks>
[Serializable]
public class ServiceNotFoundException : Exception
{
    private static string MessagePattern => "The \"{0}\" type service not found.";

    /// <summary>
    /// Creates an Exception.
    /// </summary>
    /// <param name="serviceType">Type of service.</param>
    public ServiceNotFoundException(Type serviceType) : base(GetMessage(serviceType))
    {
        
    }

    /// <summary>
    /// Creates an Exception.
    /// </summary>
    /// <param name="serviceType">Type of service.</param>
    /// <param name="message">Additional message.</param>
    public ServiceNotFoundException(Type serviceType, string? message) : base(GetMessage(serviceType, message))
    {

    }

    /// <summary>
    /// Creates an Exception.
    /// </summary>
    /// <param name="serviceType">Type of service.</param>
    /// <param name="message">Additional message.</param>
    /// <param name="innerException">Inner <see cref="Exception"/>.</param>
    public ServiceNotFoundException(
        Type serviceType, 
        string? message, 
        Exception? innerException) : base(GetMessage(serviceType, message), innerException)
    {

    }

    protected ServiceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }

    private static string GetMessage(Type serviceType, string? outerMessage = null)
    {
        _ = ThrowIfArgumentNull(serviceType);

        string message = string.Format(MessagePattern, serviceType.FullName);
        if (!string.IsNullOrWhiteSpace(outerMessage))
        {
            message = outerMessage.Trim() + " " + message;
        }

        return message;
    }
}
