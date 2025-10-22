using Coldmart.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Core.Data.Configurations;

public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public abstract string TableName { get; }
    public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);

    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder
            .ToTable(TableName);

        builder
            .HasKey(e => e.Id);

        builder
            .HasQueryFilter(e => !e.Deletado);

        ConfigureEntity(builder);
    }
}
