using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EconomyMonitor.Data.FluentConfigurations;

internal class DatePeriodConfigurationsConfiguration : IEntityTypeConfiguration<DatePeriodConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<DatePeriodConfigurationEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasIndex(i => i.Id)
            .IsUnique(unique: true);

        builder
            .HasMany(m => m.PeriodSplits)
            .WithOne(o => o.DatePeriodOption)
            .HasForeignKey(k => k.DatePeriodOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(m => m.SpendingQuotas)
            .WithOne(o => o.DatePeriodOption)
            .HasForeignKey(k => k.DatePeriodOptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
