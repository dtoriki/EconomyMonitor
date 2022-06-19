using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EconomyMonitor.Data.FluentConfigurations;

internal class ConfigureMoneyForDays : BaseFluentConfiguration<MoneyForDayEntity>
{
    public override void Configure(EntityTypeBuilder<MoneyForDayEntity> builder)
    {
        ConfigureRelationships(builder);
        ConfigureIndexes(builder);

        base.Configure(builder);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<MoneyForDayEntity> builder)
    {
        builder
            .HasMany(x => x.Earnings)
            .WithOne(x => x.MoneyFlowForDay)
            .HasForeignKey(x => x.MoneyForDayId)
            .IsRequired(required: true);

        builder
            .HasMany(x => x.Expenses)
            .WithOne(x => x.MoneyFlowForDay)
            .HasForeignKey(x => x.MoneyForDayId)
            .IsRequired(required: true);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<MoneyForDayEntity> builder)
    {
        builder.HasIndex(x => x.Date)
            .IsUnique(unique: true);
    }
}
