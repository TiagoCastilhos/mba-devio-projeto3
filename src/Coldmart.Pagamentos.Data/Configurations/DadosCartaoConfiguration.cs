using Coldmart.Core.Data.Configurations;
using Coldmart.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Pagamentos.Data.Configurations;

internal sealed class DadosCartaoConfiguration : EntityTypeConfiguration<DadosCartao>
{
    public override string TableName => "DadosCartao";

    public override void ConfigureEntity(EntityTypeBuilder<DadosCartao> builder)
    {
        builder
            .Property(d => d.NumeroCartao)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .Property(d => d.NomeTitular)
            .IsRequired()
            .HasMaxLength(60);

        builder
            .Property(d => d.DataValidade)
            .IsRequired();

        builder
            .Property(d => d.CodigoSeguranca)
            .IsRequired()
            .HasMaxLength(10);
    }
}