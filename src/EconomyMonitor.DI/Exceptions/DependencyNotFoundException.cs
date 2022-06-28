using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.DI.Exceptions;

/// <summary>
/// Исключение an <see cref="Exception"/>, 
/// вызываемое при попытке получить нужную зависимость из коллекции сервисов <see cref="IServiceCollection"/>,
/// но она не зарегестрирован в ней.
/// </summary>
/// <remarks>
/// Имеет встроенное сообщение: "Зависимость типа "<see cref="Type.FullName"/>" не найдена.".
/// </remarks>
[Serializable]
public class DependencyNotFoundException : Exception
{
    private static string MessagePattern => "Зависимость типа \"{0}\" не найдена.";

    /// <summary>
    /// Создаёт исключение.
    /// </summary>
    /// <param name="dependencyType">Тип зависимости.</param>
    public DependencyNotFoundException(Type dependencyType) : base(GetMessage(dependencyType))
    {

    }

    /// <summary>
    /// Создаёт исключение.
    /// </summary>
    /// <param name="dependencyType">Тип зависимости.</param>
    /// <param name="message">Добавочное сообщение.</param>
    public DependencyNotFoundException(Type dependencyType, string? message) : base(GetMessage(dependencyType, message))
    {

    }

    /// <summary>
    /// Создаёт исключение.
    /// </summary>
    /// <param name="dependencyType">Тип зависимости.</param>
    /// <param name="message">Добавочное сообщение.</param>
    /// <param name="innerException">Внутреннее исключение <see cref="Exception"/>.</param>
    public DependencyNotFoundException(
        Type dependencyType,
        string? message,
        Exception? innerException) : base(GetMessage(dependencyType, message), innerException)
    {

    }

    /// <summary>
    /// Создаёт исключение.
    /// </summary>
    /// <param name="info">
    /// Хранилище всех данных, необходимых для сериализации или десериализации объекта.
    /// </param>
    /// <param name="context">
    /// Контекст, который описывает источник и место назначения данного сериализованного потока 
    /// и предоставляет дополнительный контекст, определяемый вызывающей стороной.
    /// </param>
    protected DependencyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
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
