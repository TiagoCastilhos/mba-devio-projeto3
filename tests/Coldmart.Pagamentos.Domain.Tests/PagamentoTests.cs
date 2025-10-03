using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Pagamentos.Domain.Tests;

public class PagamentoTests
{
    [Theory, AutoDomainData]
    public void CriarPagamento_FornecidosParametros_DeveCriar(DadosCartao dadosCartao)
    {
        //act
        var pagamento = new Pagamento(dadosCartao);

        //assert
        Assert.Equal(StatusPagamento.Pendente, pagamento.Status);
        Assert.Equal(dadosCartao, pagamento.Cartao);
        Assert.NotEqual(DateTimeOffset.MinValue, pagamento.DataAtualizacao);
    }

    [Fact]
    public void CriarPagamento_DadosCartaoNulo_DeveLancarExcecao()
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new Pagamento(null!));
    }
}