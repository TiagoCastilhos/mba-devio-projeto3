using Coldmart.Core.Domain;

namespace Coldmart.Pagamentos.Domain;

public class Pagamento : Entity, IAggregateRoot
{
    public Guid CartaoId { get; protected set; }
    public DadosCartao Cartao { get; protected set; }
    public StatusPagamento Status { get; protected set; }
    public DateTimeOffset DataAtualizacao { get; protected set; }

    protected Pagamento() { }

    public Pagamento(DadosCartao cartao)
    {
        ArgumentNullException.ThrowIfNull(cartao, nameof(cartao));

        Cartao = cartao;
        Status = StatusPagamento.Pendente;
        DataAtualizacao = DateTimeOffset.UtcNow;
    }

    public void Aprovar()
    {
        ValidarPagamentoPendente();

        Status = StatusPagamento.Aprovado;
        DataAtualizacao = DateTimeOffset.UtcNow;
    }

    public void Cancelar()
    {
        ValidarPagamentoPendente();

        Status = StatusPagamento.Cancelado;
        DataAtualizacao = DateTimeOffset.UtcNow;
    }

    public void Recusar()
    {
        ValidarPagamentoPendente();

        Status = StatusPagamento.Recusado;
        DataAtualizacao = DateTimeOffset.UtcNow;
    }

    private void ValidarPagamentoPendente()
    {
        if (Status != StatusPagamento.Pendente)
            throw new InvalidOperationException("Pagamento precisa estar pendente");
    }
}
