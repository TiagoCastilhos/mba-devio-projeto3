using Coldmart.Alunos.Domain;
using Coldmart.Core.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Alunos.Data.Configurations;

internal sealed class HistoricoAlunoConfiguration : EntityTypeConfiguration<HistoricoAluno>
{
    public override string TableName => "HistoricoAluno";

    public override void ConfigureEntity(EntityTypeBuilder<HistoricoAluno> builder)
    {
        builder
            .HasOne<Aluno>()
            .WithMany(a => a.Historicos)
            .HasForeignKey(ha => ha.AlunoId);

        builder
            .HasOne<Curso>()
            .WithMany()
            .HasForeignKey(ha => ha.CursoId);

        builder
            .HasOne<Aula>()
            .WithMany()
            .HasForeignKey(ha => ha.AulaId);
    }
}