using Coldmart.Alunos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Alunos.Data.Configurations;

internal sealed class AulaConfiguration : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("Aula");
    }
}
