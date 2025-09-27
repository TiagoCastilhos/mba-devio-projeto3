using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Cursos.Domain.Tests;

public class CursoTests
{
    [Theory, AutoDomainData]
    public void CriarCurso_FornecidoNome_DeveCriar(string nome)
    {
        //act
        var curso = new Curso(nome);

        //assert
        Assert.Equal(nome, curso.Nome);
        Assert.Equal(TimeSpan.Zero, curso.DuracaoTotal);
    }

    [Fact]
    public void CriarCurso_NomeVazioOuNulo_DeveLancarExcecao()
    {
        //act & assert
        Assert.Throws<ArgumentException>(() => new Curso(""));
        Assert.Throws<ArgumentNullException>(() => new Curso(null!));
    }

    [Theory, AutoDomainData]
    public void AdicionarConteudoProgramatico_Fornecido_DeveAdicionar(ConteudoProgramatico conteudoProgramatico, Curso curso)
    {
        //act
        curso.AdicionarConteudoProgramatico(conteudoProgramatico);

        //assert
        Assert.Contains(conteudoProgramatico, curso.ConteudosProgramaticos!);
    }

    [Theory, AutoDomainData]
    public void AdicionarConteudoProgramatico_ConteudoNulo_DeveLancarExcecao(Curso curso)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => curso.AdicionarConteudoProgramatico(null!));
    }

    [Theory, AutoDomainData]
    public void AdicionarAula_Fornecida_DeveAdicionarEAumentarDuracaoTotal(Aula aula, Curso curso)
    {
        //act
        var duracaoAntes = curso.DuracaoTotal;
        curso.AdicionarAula(aula);

        //assert
        Assert.Contains(aula, curso.Aulas!);
        Assert.Equal(duracaoAntes + aula.Duracao, curso.DuracaoTotal);
    }

    [Theory, AutoDomainData]
    public void AdicionarAula_AulaNula_DeveLancarExcecao(Curso curso)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => curso.AdicionarAula(null!));
    }
}
