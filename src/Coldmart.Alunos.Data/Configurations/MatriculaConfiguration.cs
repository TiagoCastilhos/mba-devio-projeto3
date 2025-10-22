using Coldmart.Alunos.Domain;
using Coldmart.Alunos.Domain.Enumerations;
using Coldmart.Core.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Alunos.Data.Configurations;

internal sealed class MatriculaConfiguration : EntityTypeConfiguration<Matricula>
{
    public override string TableName => "Matricula";

    public override void ConfigureEntity(EntityTypeBuilder<Matricula> builder)
    {
        builder
            .Property(a => a.Status)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion(s => s.ToString(), v => Enum.Parse<StatusMatricula>(v));

        builder
            .Property(a => a.DataAtualizacao)
            .IsRequired();

        builder
            .HasOne(a => a.Curso)
            .WithMany()
            .HasForeignKey(k => k.CursoId);
    }
}
