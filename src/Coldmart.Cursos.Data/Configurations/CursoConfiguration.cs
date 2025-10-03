using Coldmart.Core.Data.Configurations;
using Coldmart.Cursos.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Cursos.Data.Configurations;

internal sealed class CursoConfiguration : EntityTypeConfiguration<Curso>
{
    public override string TableName => "Curso";

    public override void ConfigureEntity(EntityTypeBuilder<Curso> builder)
    {
        builder
            .Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(60);

        builder
            .Property(c => c.DuracaoTotal)
            .HasConversion(
                v => v.TotalSeconds,
                v => TimeSpan.FromSeconds(v))
            .IsRequired();

        builder
            .HasMany(c => c.ConteudosProgramaticos)
            .WithOne()
            .HasForeignKey(c => c.CursoId);
    }
}
