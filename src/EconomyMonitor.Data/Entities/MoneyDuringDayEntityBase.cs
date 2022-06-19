using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Base;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;

namespace EconomyMonitor.Data.Entities;

#nullable disable

internal abstract class MoneyDuringDayEntityBase : EntityBase, IMoneyDuringDayEntity
{
    public decimal Amount { get; set; }

    public TimeOnly Time { get; set; }

    public MoneyForDayEntity MoneyFlowForDay { get; set; }

    public Guid MoneyForDayId { get; set; }

    IMoneyForDay IMoneyDuringDay.MoneyFlowForDay => MoneyFlowForDay;

    protected MoneyDuringDayEntityBase()
    {
        
    }
}

#nullable restore
