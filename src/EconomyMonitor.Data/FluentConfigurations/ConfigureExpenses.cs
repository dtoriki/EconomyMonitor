using EconomyMonitor.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EconomyMonitor.Data.FluentConfigurations;

internal class ConfigureExpenses : BaseFluentConfiguration<ExpenseEntity>
{
    public override void Configure(EntityTypeBuilder<ExpenseEntity> builder)
    {
        ConfigureRelationships(builder);

        base.Configure(builder);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<ExpenseEntity> builder)
    {
        builder
            .HasOne(x => x.MoneyFlowForDay)
            .WithMany(x => x.Expenses)
            .HasForeignKey(x => x.MoneyForDayId)
            .IsRequired(required: true);
    }
}
