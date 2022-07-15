using EconomyMonitor.Data.Abstracts.Base;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;

namespace EconomyMonitor.Data.Entities;
internal class SettingsEntity : EntityBase, ISettingsEntity
{
    public decimal? StartingBudget { get; set; }
}
