using System.Diagnostics.CodeAnalysis;
using Coldmart.Alunos.Domain;
using Coldmart.Core.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Alunos.Data.Configurations;

[ExcludeFromCodeCoverage]
internal sealed class CertificadoConfiguration : EntityTypeConfiguration<Certificado>
{
    public override string TableName => "Certificado";

    public override void ConfigureEntity(EntityTypeBuilder<Certificado> builder)
    {
        builder
            .HasOne(a => a.Curso)
            .WithMany()
            .HasForeignKey(k => k.CursoId);
    }
}
