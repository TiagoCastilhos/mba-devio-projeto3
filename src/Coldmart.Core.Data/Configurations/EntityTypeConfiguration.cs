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
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValue(() => Guid.NewGuid());

        builder
            .Property(e => e.DataCriacao)
            .ValueGeneratedOnAdd()
            .HasDefaultValue(() => DateTimeOffset.UtcNow);

        builder
            .Property(e => e.Deletado)
            .HasDefaultValue(false);

        builder
            .HasQueryFilter(e => !e.Deletado);

        ConfigureEntity(builder);
    }
}
