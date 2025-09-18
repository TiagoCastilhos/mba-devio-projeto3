using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Alunos.Domain.Tests;

public class CursoTests
{
    [Theory, AutoDomainData]
    public void CriarCurso_FornecidoIdENome_DeveCriar(Guid id, string nome)
    {
        //act
        var curso = new Curso(id, nome);
        
        //assert
        Assert.Equal(id, curso.Id);
        Assert.Equal(nome, curso.Nome);
    }

    [Theory, AutoDomainData]
    public void CriarCurso_IdVazio_DeveLancarExcecao(string nome)
    {
        //act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Curso(Guid.Empty, nome));
    }

    [Theory, AutoDomainData]
    public void CriarCurso_NomeVazioOuNulo_DeveLancarExcecao(Guid id)
    {
        //act & assert
        Assert.Throws<ArgumentException>(() => new Curso(id, ""));
        Assert.Throws<ArgumentNullException>(() => new Curso(id, null!));
    }
}