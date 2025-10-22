using Coldmart.Core.Domain;

namespace Coldmart.Pagamentos.Domain;

public class Pagamento : Entity, IAggregateRoot
{
    public Guid CartaoId { get; protected set; }
    public DadosCartao Cartao { get; protected set; }
    public StatusPagamento Status { get; protected set; }
    public DateTimeOffset DataAtualizacao { get; protected set; }
    public decimal Valor { get; protected set; }
    public Guid MatriculaId { get; protected set; }

    protected Pagamento() { }

    public Pagamento(DadosCartao cartao, decimal valor, Guid matriculaId)
        : base(Guid.NewGuid())
    {
        ArgumentNullException.ThrowIfNull(cartao, nameof(cartao));
        ArgumentOutOfRangeException.ThrowIfLessThan(valor, 0, nameof(valor));
        ArgumentOutOfRangeException.ThrowIfEqual(matriculaId, Guid.Empty, nameof(matriculaId));

        Cartao = cartao;
        Status = StatusPagamento.Pendente;
        DataAtualizacao = DateTimeOffset.UtcNow;
        Valor = valor;
        MatriculaId = matriculaId;
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

    private void ValidarPagamentoPendente()
    {
        if (Status != StatusPagamento.Pendente)
            throw new InvalidOperationException("Pagamento precisa estar pendente");
    }
}
