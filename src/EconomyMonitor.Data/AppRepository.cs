using EconomyMonitor.Data.Abstracts.Base.Repositories;
using EconomyMonitor.Data.Abstracts.Interfaces;
using EconomyMonitor.Data.Abstracts.Interfaces.EfSets;
using EconomyMonitor.Data.Abstracts.Interfaces.Entities;
using EconomyMonitor.Data.Entities;
using EconomyMonitor.Data.FluentConfigurations;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static EconomyMonitor.Helpers.ThrowHelper;

namespace EconomyMonitor.Data;

internal sealed class AppRepository : 
    EfRepository,
    IMoneyForDaySet<MoneyForDayEntity>,
    IMoneyDuringDaySet<MoneyDuringDayEntityBase>,
    ISettingsSet<SettingsEntity>,
    IDataProtectionKeyContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    public DbSet<MoneyForDayEntity> MoneyForDays { get; set; }
    public DbSet<MoneyDuringDayEntityBase> MoneyDuringDays { get; set; }
    public DbSet<SettingsEntity> Settings { get; set; }

    IQueryable<IMoneyForDayEntity> IRepositorySet<IMoneyForDayEntity>.EntitySet => MoneyForDays;
    IQueryable<IMoneyDuringDayEntity> IRepositorySet<IMoneyDuringDayEntity>.EntitySet => MoneyDuringDays;
    IQueryable<ISettingsEntity> IRepositorySet<ISettingsEntity>.EntitySet => Settings;

    public AppRepository() : base()
    {

    }

    public AppRepository(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        _ = ThrowIfArgumentNull(dbContextOptions);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new ConfigureEarnings())
            .ApplyConfiguration(new ConfigureExpenses())
            .ApplyConfiguration(new ConfigureMoneyForDays())
            .ApplyConfiguration(new ConfigureSettings());
    }
}
