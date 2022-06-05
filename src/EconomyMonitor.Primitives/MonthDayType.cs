using System.ComponentModel.DataAnnotations;

namespace EconomyMonitor.Primitives;

/// <summary>
/// Классификация дней месяца.
/// </summary>
public enum MonthDayType
{
    /// <summary>
    /// Первый день мксяца.
    /// </summary>
    [Display(Description = "Первый день месяца")]
    FirstDayOfTheMonth = 1,

    /// <summary>
    /// Последний день месяца.
    /// </summary>
    [Display(Description = "Последний день месяца")]
    LastDayOfTheMonth = 2,

    /// <summary>
    /// Середина месяца (15-е число).
    /// </summary>
    [Display(Description = "Середина месяца (15-е число)")]
    MiddleOfTheMonth = 3,

    /// <summary>
    /// Другое.
    /// </summary>
    [Display(Description = "Другое")]
    Other = 4,

    /// <summary>
    /// Неизвестно.
    /// </summary>
    [Display(Description = "Неизвестно")]
    Unknown = 0
}
