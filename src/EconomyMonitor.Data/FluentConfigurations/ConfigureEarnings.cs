using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EconomyMonitor.Data.FluentConfigurations;

internal class ConfigureEarnings : BaseFluentConfiguration<EarningEntity>
{
    public override void Configure(EntityTypeBuilder<EarningEntity> builder)
    {
        ConfigureRelationships(builder);

        base.Configure(builder);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<EarningEntity> builder)
    {
        builder
            .HasOne(x => x.MoneyFlowForDay)
            .WithMany(x => x.Earnings)
            .HasForeignKey(x => x.MoneyForDayId)
            .IsRequired(required: true);
    }
}
