using System.Diagnostics.CodeAnalysis;
using Coldmart.Alunos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Alunos.Data.Configurations;

[ExcludeFromCodeCoverage]
internal sealed class AulaConfiguration : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("Aula", a => a.ExcludeFromMigrations());
    }
}
