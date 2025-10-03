using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Cursos.Domain.Tests;

public class AulaTests
{
    [Theory, AutoDomainData]
    public void CriarAula_FornecidosCursoTituloDuracao_DeveCriar(Curso curso, string titulo, TimeSpan duracao)
    {
        //act
        var aula = new Aula(curso, titulo, duracao);

        //assert
        Assert.Equal(curso.Id, aula.CursoId);
        Assert.Equal(titulo, aula.Titulo);
        Assert.Equal(duracao, aula.Duracao);
    }

    [Theory, AutoDomainData]
    public void CriarAula_CursoNulo_DeveLancarArgumentNullException(string titulo, TimeSpan duracao)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new Aula(null!, titulo, duracao));
    }

    [Theory, AutoDomainData]
    public void CriarAula_TituloNuloOuVazio_DeveLancarArgumentException(Curso curso, TimeSpan duracao)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new Aula(curso, null!, duracao));
        Assert.Throws<ArgumentException>(() => new Aula(curso, "", duracao));
    }

    [Theory, AutoDomainData]
    public void CriarAula_DuracaoNegativoOuZero_DeveLancarArgumentOutOfRangeException(Curso curso, string titulo)
    {
        //act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Aula(curso, titulo, TimeSpan.Zero));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Aula(curso, titulo, TimeSpan.FromSeconds(-10)));
    }
}
