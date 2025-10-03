using Coldmart.Core.Data.Configurations;
using Coldmart.Pagamentos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coldmart.Pagamentos.Data.Configurations;

internal sealed class PagamentoConfiguration : EntityTypeConfiguration<Pagamento>
{
    public override string TableName => "Pagamento";

    public override void ConfigureEntity(EntityTypeBuilder<Pagamento> builder)
    {
        builder
            .Property(p => p.Status)
            .IsRequired()
            .HasConversion(p => p.ToString(), s => Enum.Parse<StatusPagamento>(s))
            .HasMaxLength(20);

        builder
            .Property(p => p.DataAtualizacao)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValue(() => DateTimeOffset.UtcNow);

        builder
            .HasOne(p => p.Cartao)
            .WithMany()
            .HasForeignKey(p => p.CartaoId);
    }
}
