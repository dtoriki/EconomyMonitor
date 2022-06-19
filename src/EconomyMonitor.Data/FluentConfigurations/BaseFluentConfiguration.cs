using EconomyMonitor.Data.Abstracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EconomyMonitor.Data.FluentConfigurations;
internal abstract class BaseFluentConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasIndex(x => x.Id)
            .IsUnique(unique: true);

        builder.HasIndex(x => x.DateCreated)
            .IsUnique(unique: false);

        builder.HasIndex(x => x.DateModified)
            .IsUnique(unique: false);
    }
}
