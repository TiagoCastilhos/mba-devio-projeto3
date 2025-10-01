using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Pagamentos.Domain.Tests;

public class DadosCartaoTests
{
    [Theory, AutoDomainData]
    public void CriarDadosCartao_FornecidosParametros_DeveCriar(string numero, string nome, DateTimeOffset validade, string codigoSeguranca)
    {
        //act
        var dadosCartao = new DadosCartao(numero, nome, validade, codigoSeguranca);

        //assert
        Assert.Equal(numero, dadosCartao.NumeroCartao);
        Assert.Equal(nome, dadosCartao.NomeTitular);
        Assert.Equal(validade, dadosCartao.DataValidade);
        Assert.Equal(codigoSeguranca, dadosCartao.CodigoSeguranca);
    }

    [Theory, AutoDomainData]
    public void CriarDadosCartao_NumeroNuloOuVazio_DeveLancarExcecao(string nome, DateTimeOffset validade, string codigoSeguranca)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new DadosCartao(null!, nome, validade, codigoSeguranca));
        Assert.Throws<ArgumentException>(() => new DadosCartao("", nome, validade, codigoSeguranca));
    }

    [Theory, AutoDomainData]
    public void CriarDadosCartao_NomeNuloOuVazio_DeveLancarExcecao(string numero, DateTimeOffset validade, string codigoSeguranca)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new DadosCartao(numero, null!, validade, codigoSeguranca));
        Assert.Throws<ArgumentException>(() => new DadosCartao(numero, "", validade, codigoSeguranca));
    }

    [Theory, AutoDomainData]
    public void CriarDadosCartao_DataValidadeInvalida_DeveLancarExcecao(string numero, string nome, string codigoSeguranca)
    {
        //act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new DadosCartao(numero, nome, DateTimeOffset.UtcNow.AddDays(-1), codigoSeguranca));
    }

    [Theory, AutoDomainData]
    public void CriarDadosCartao_CodigoSegurancaNuloOuVazio_DeveLancarExcecao(string numero, DateTimeOffset validade, string nome)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new DadosCartao(numero, nome, validade, null!));
        Assert.Throws<ArgumentException>(() => new DadosCartao(numero, nome, validade, ""));
    }
}
