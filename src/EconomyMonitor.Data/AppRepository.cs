using EconomyMonitor.Data.Abstracts.Base.Repositories;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Data.FluentConfigurations;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data;

internal sealed class AppRepository : EfRepository, IAppRepository
{
    public DbSet<DatePeriodEntity> DatePeriods { get; set; }
    public DbSet<DatePeriodConfigurationEntity> DatePeriodConfigurations { get; set; }
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    //public DbSet<SpendingQuotaEntity> SpendingQuotas { get; set; }
    //public DbSet<PeriodSplitEntity> PeriodSplits { get; set; }

    IQueryable<IDatePeriodEntity> IRepositorySet<IDatePeriodEntity>.EntitySet => DatePeriods;
    IQueryable<IDatePeriodConfigurationEntity> IRepositorySet<IDatePeriodConfigurationEntity>.EntitySet => DatePeriodConfigurations;
    //IQueryable<ISpendingQuotaEntity> IRepositorySet<ISpendingQuotaEntity>.EntitySet => SpendingQuotas;
    //IQueryable<IPeriodSplitEntity> IRepositorySet<IPeriodSplitEntity>.EntitySet => PeriodSplits;


#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public AppRepository() : base()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    {

    }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public AppRepository(DbContextOptions dbContextOptions) : base(dbContextOptions)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    {
        _ = ThrowIfArgumentNull(dbContextOptions);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DatePeriodConfigurationsConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
