using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Pagamentos.Domain.Tests;

public class PagamentoTests
{
    [Theory, AutoDomainData]
    public void CriarPagamento_FornecidosParametros_DeveCriar(DadosCartao dadosCartao, Guid matriculaId)
    {
        //act
        var pagamento = new Pagamento(dadosCartao, 2, matriculaId);

        //assert
        Assert.Equal(StatusPagamento.Pendente, pagamento.Status);
        Assert.Equal(dadosCartao, pagamento.Cartao);
        Assert.NotEqual(DateTimeOffset.MinValue, pagamento.DataAtualizacao);
    }

    [Theory, AutoDomainData]
    public void CriarPagamento_DadosCartaoNulo_DeveLancarExcecao(Guid matriculaId)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new Pagamento(null!, 2, matriculaId));
    }

    [Theory, AutoDomainData]
    public void CriarPagamento_ValorInvalido_DeveLancarExcecao(DadosCartao dadosCartao, Guid matriculaId)
    {
        //act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Pagamento(dadosCartao, -5, matriculaId));
    }

    [Theory, AutoDomainData]
    public void CriarPagamento_MatriculaIdInvalido_DeveLancarExcecao(DadosCartao dadosCartao)
    {
        //act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Pagamento(dadosCartao, 2, Guid.Empty));
    }

    [Theory, AutoDomainData]
    public void AprovarPagamento_PagamentoPendente_DeveAprovar(DadosCartao dadosCartao, Guid matriculaId)
    {
        //arrange
        var pagamento = new Pagamento(dadosCartao, 2, matriculaId);

        //act
        pagamento.Aprovar();

        //assert
        Assert.Equal(StatusPagamento.Aprovado, pagamento.Status);
        Assert.NotEqual(DateTimeOffset.MinValue, pagamento.DataAtualizacao);
    }

    [Theory, AutoDomainData]
    public void CancelarPagamento_PagamentoPendente_DeveCancelar(DadosCartao dadosCartao, Guid matriculaId)
    {
        //arrange
        var pagamento = new Pagamento(dadosCartao, 2, matriculaId);

        //act
        pagamento.Cancelar();

        //assert
        Assert.Equal(StatusPagamento.Cancelado, pagamento.Status);
        Assert.NotEqual(DateTimeOffset.MinValue, pagamento.DataAtualizacao);
    }

    [Theory, AutoDomainData]
    public void RecusarPagamento_PagamentoPendente_DeveRecusar(DadosCartao dadosCartao, Guid matriculaId)
    {
        //arrange
        var pagamento = new Pagamento(dadosCartao, 2, matriculaId);

        //act
        pagamento.Recusar();

        //assert
        Assert.Equal(StatusPagamento.Recusado, pagamento.Status);
        Assert.NotEqual(DateTimeOffset.MinValue, pagamento.DataAtualizacao);
    }

    [Theory, AutoDomainData]
    public void AprovarPagamento_PagamentoNaoPendente_DeveLancarExcecao(DadosCartao dadosCartao, Guid matriculaId)
    {
        //arrange
        var pagamento = new Pagamento(dadosCartao, 2, matriculaId);
        pagamento.Recusar();

        //act & assert
        Assert.Throws<InvalidOperationException>(pagamento.Aprovar);
    }
}