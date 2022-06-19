using System.ComponentModel.DataAnnotations.Schema;
using EconomyMonitor.Abstacts;
using EconomyMonitor.Data.Abstracts.Base;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Primitives.Collections;

namespace EconomyMonitor.Data.Entities;

internal sealed class MoneyForDayEntity : 
    EntityBase, 
    IMoneyForDayEntity
{
    public DateOnly Date { get; set; }

    public ICollection<EarningEntity> Earnings { get; set; }

    public ICollection<ExpenseEntity> Expenses { get; set; }

    [NotMapped]
    public decimal Amount => Earnings.Sum(x => x.Amount) - Expenses.Sum(x => x.Amount);

    [NotMapped]
    IMoneyFlow<IMoneyDuringDay> IMoneyForDay.Earnings => new MoneyFlow<IMoneyDuringDay>(Earnings);

    [NotMapped]
    IMoneyFlow<IMoneyDuringDay> IMoneyForDay.Expenses => new MoneyFlow<IMoneyDuringDay>(Expenses);

    [NotMapped]
    ICollection<IMoneyDuringDayEntity> IMoneyForDayEntity.Earnings => new MoneyFlow<IMoneyDuringDayEntity>(Earnings);

    [NotMapped]
    ICollection<IMoneyDuringDayEntity> IMoneyForDayEntity.Expenses => new MoneyFlow<IMoneyDuringDayEntity>(Expenses);

    public MoneyForDayEntity()
    {
        Earnings = new List<EarningEntity>();
        Expenses = new List<ExpenseEntity>();
    }
}
