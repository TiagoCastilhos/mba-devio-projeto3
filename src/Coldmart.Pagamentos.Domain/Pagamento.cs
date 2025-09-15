using Coldmart.Core;

namespace Coldmart.Pagamentos.Domain;

public class Pagamento : Entity, IAggregateRoot
{
    public required DadosCartao Cartao { get; set; }
    public required StatusPagamento Status { get; set; }
    public DateTimeOffset DataAtualizacao { get; set; }
}
