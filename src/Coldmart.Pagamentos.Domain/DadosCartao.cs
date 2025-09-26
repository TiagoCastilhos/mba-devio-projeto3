using Coldmart.Core.Domain;

namespace Coldmart.Pagamentos.Domain;

public class DadosCartao : Entity
{
    public required string NumeroCartao { get; set; }
    public required string NomeTitular { get; set; }
    public required DateTime DataValidade { get; set; }
    public required string CodigoSeguranca { get; set; }
}
