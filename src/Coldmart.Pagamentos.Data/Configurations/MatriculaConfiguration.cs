using System.Diagnostics.CodeAnalysis;
using Coldmart.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Pagamentos.Data.Configurations;

[ExcludeFromCodeCoverage]
internal sealed class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder
            .ToTable("Matricula", a => a.ExcludeFromMigrations());
    }
}