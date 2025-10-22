using Coldmart.Core.Domain;

namespace Coldmart.Pagamentos.Domain;

public class DadosCartao : Entity
{
    public string NumeroCartao { get; protected set; }
    public string NomeTitular { get; protected set; }
    public DateTimeOffset DataValidade { get; protected set; }
    public string CodigoSeguranca { get; protected set; }

    protected DadosCartao() { }

    public DadosCartao(string numeroCartao, string nomeTitular, DateTimeOffset dataValidade, string codigoSeguranca)
        : base(Guid.NewGuid())
    {
        ArgumentException.ThrowIfNullOrEmpty(numeroCartao, nameof(numeroCartao));
        ArgumentException.ThrowIfNullOrEmpty(nomeTitular, nameof(nomeTitular));
        ArgumentException.ThrowIfNullOrEmpty(codigoSeguranca, nameof(codigoSeguranca));
        ArgumentOutOfRangeException.ThrowIfLessThan(dataValidade, DateTimeOffset.UtcNow);

        NumeroCartao = numeroCartao;
        NomeTitular = nomeTitular;
        DataValidade = dataValidade;
        CodigoSeguranca = codigoSeguranca;
    }
}
