using EconomyMonitor.Data.Abstracts.Base.Repositories;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data;

internal sealed class EconomyMonitorRepository : EfRepository, IEconomyMonitorRepository
{
    public DbSet<PeriodEntity> Periods { get; set; }

    IQueryable<IPeriodEntity> IRepositorySet<IPeriodEntity>.EntitySet => Periods;

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public EconomyMonitorRepository() : base()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    {
        Database.EnsureCreated();
    }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public EconomyMonitorRepository(DbContextOptions dbContextOptions) : base(dbContextOptions)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    {
        _ = ThrowIfArgumentNull(dbContextOptions);
    }
}
