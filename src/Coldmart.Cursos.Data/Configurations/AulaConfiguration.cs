using System.Diagnostics.CodeAnalysis;
using Coldmart.Core.Data.Configurations;
using Coldmart.Cursos.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Cursos.Data.Configurations;

[ExcludeFromCodeCoverage]
internal sealed class AulaConfiguration : EntityTypeConfiguration<Aula>
{
    public override string TableName => "Aula";

    public override void ConfigureEntity(EntityTypeBuilder<Aula> builder)
    {
        builder
            .Property(a => a.Titulo)
            .IsRequired()
            .HasMaxLength(60);

        builder
            .Property(a => a.Duracao)
            .HasConversion(
                v => v.TotalSeconds,
                v => TimeSpan.FromSeconds(v))
            .IsRequired();
    }
}