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

    [Theory, AutoDomainData]
    public void AprovarPagamento_PagamentoPendente_DeveAprovar(DadosCartao dadosCartao)
    {
        //arrange
        var pagamento = new Pagamento(dadosCartao);

        //act
        pagamento.Aprovar();

        //assert
        Assert.Equal(StatusPagamento.Aprovado, pagamento.Status);
        Assert.NotEqual(DateTimeOffset.MinValue, pagamento.DataAtualizacao);
    }

    [Theory, AutoDomainData]
    public void CancelarPagamento_PagamentoPendente_DeveCancelar(DadosCartao dadosCartao)
    {
        //arrange
        var pagamento = new Pagamento(dadosCartao);

        //act
        pagamento.Cancelar();

        //assert
        Assert.Equal(StatusPagamento.Cancelado, pagamento.Status);
        Assert.NotEqual(DateTimeOffset.MinValue, pagamento.DataAtualizacao);
    }

    [Theory, AutoDomainData]
    public void RecusarPagamento_PagamentoPendente_DeveRecusar(DadosCartao dadosCartao)
    {
        //arrange
        var pagamento = new Pagamento(dadosCartao);

        //act
        pagamento.Recusar();

        //assert
        Assert.Equal(StatusPagamento.Recusado, pagamento.Status);
        Assert.NotEqual(DateTimeOffset.MinValue, pagamento.DataAtualizacao);
    }

    [Theory, AutoDomainData]
    public void AprovarPagamento_PagamentoNaoPendente_DeveLancarExcecao(DadosCartao dadosCartao)
    {
        //arrange
        var pagamento = new Pagamento(dadosCartao);
        pagamento.Recusar();

        //act & assert
        Assert.Throws<InvalidOperationException>(pagamento.Aprovar);
    }
}