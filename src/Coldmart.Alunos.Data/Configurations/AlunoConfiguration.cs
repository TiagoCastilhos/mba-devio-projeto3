using Coldmart.Alunos.Domain;
using Coldmart.Core.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Alunos.Data.Configurations;

internal sealed class AlunoConfiguration : EntityTypeConfiguration<Aluno>
{
    public override string TableName => "Aluno";

    public override void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder
            .Property(a => a.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(100);
    }
}
