using System.ComponentModel.DataAnnotations;

namespace EconomyMonitor.Primitives.Enums;

/// <summary>
/// Классификация дней месяца.
/// </summary>
public enum MonthDayType
{
    /// <summary>
    /// Первый день мксяца.
    /// </summary>
    [Display(Name = "Первый день месяца")]
    FirstDayOfTheMonth = 1,

    /// <summary>
    /// Последний день месяца.
    /// </summary>
    [Display(Name = "Последний день месяца")]
    LastDayOfTheMonth = 2,

    /// <summary>
    /// Середина месяца (15-е число).
    /// </summary>
    [Display(Name = "Середина месяца (15-е число)")]
    MiddleOfTheMonth = 3,

    /// <summary>
    /// Другое.
    /// </summary>
    [Display(Name = "Другое")]
    Other = 4,

    /// <summary>
    /// Неизвестно.
    /// </summary>
    [Display(Name = "Неизвестно")]
    Unknown = 0
}
