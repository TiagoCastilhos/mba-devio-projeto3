using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Cursos.Domain.Tests;

public class ConteudoProgramaticoTests
{
    [Theory, AutoDomainData]
    public void CriarConteudoProgramatico_FornecidosCursoTituloDescricao_DeveCriar(Curso curso, string titulo, string descricao)
    {
        //act
        var conteudoProgramatico = new ConteudoProgramatico(curso, titulo, descricao);

        //assert
        Assert.Equal(curso.Id, conteudoProgramatico.CursoId);
        Assert.Equal(titulo, conteudoProgramatico.Titulo);
        Assert.Equal(descricao, conteudoProgramatico.Descricao);
    }

    [Theory, AutoDomainData]
    public void CriarConteudoProgramatico_CursoNulo_DeveLancarExcecao(string titulo, string descricao)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new ConteudoProgramatico(null!, titulo, descricao));
    }

    [Theory, AutoDomainData]
    public void CriarConteudoProgramatico_TituloVazioOuNulo_DeveLancarExcecao(Curso curso, string descricao)
    {
        //act & assert
        Assert.Throws<ArgumentException>(() => new ConteudoProgramatico(curso, "", descricao));
        Assert.Throws<ArgumentNullException>(() => new ConteudoProgramatico(curso, null!, descricao));
    }

    [Theory, AutoDomainData]
    public void CriarConteudoProgramatico_DescricaoVaziaOuNulo_DeveLancarExcecao(Curso curso, string titulo)
    {
        //act & assert
        Assert.Throws<ArgumentException>(() => new ConteudoProgramatico(curso, titulo, ""));
        Assert.Throws<ArgumentNullException>(() => new ConteudoProgramatico(curso, titulo, null!));
    }
}
