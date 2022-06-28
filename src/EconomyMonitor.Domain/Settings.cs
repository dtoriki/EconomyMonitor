using EconomyMonitor.Abstacts;

namespace EconomyMonitor.Domain;

/// <summary>
/// Настройки приложения.
/// </summary>
/// <remarks>
/// Реализует <see cref="ISettings"/>, <see cref="IUniqueRequired"/>.
/// </remarks>
public class Settings : ISettings, IUniqueRequired
{
    public decimal StartingBudget { get; set; }

    public Guid Id { get; set; }

    public Settings()
    {

    }
}
