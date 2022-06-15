using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EconomyMonitor.Data.FluentConfigurations;

internal class DatePeriodConfigurationsConfiguration : IEntityTypeConfiguration<DatePeriodConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<DatePeriodConfigurationEntity> builder)
    {
       ConfigureIndexes(builder);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<DatePeriodConfigurationEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasIndex(config => config.Id)
            .IsUnique(unique: true);

        builder
            .HasIndex(config => config.StartPeriodDateInclusive)
            .IsUnique(unique: true);

        builder
            .HasIndex(config => config.EndPeriodDateExclusive)
            .IsUnique(unique: true);

        builder
            .HasIndex(config => new
            {
                config.StartPeriodDateInclusive,
                config.EndPeriodDateExclusive
            })
            .IsUnique(unique: true);
    }
}
