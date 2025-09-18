using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Alunos.Domain.Tests;

public class CertificadoTests
{
    [Theory, AutoDomainData]
    public void CriarCertificado_FornecidoCursoEAluno_DeveCriar(Curso curso, Aluno aluno)
    {
        //act
        var certificado = new Certificado(curso, aluno);

        //assert
        Assert.Equal(curso, certificado.Curso);
        Assert.Equal(aluno, certificado.Aluno);
    }

    [Theory, AutoDomainData]
    public void CriarCertificado_CursoNulo_DeveLancarExcecao(Aluno aluno)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new Certificado(null!, aluno));
    }

    [Theory, AutoDomainData]
    public void CriarCertificado_AlunoNulo_DeveLancarExcecao(Curso curso)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => new Certificado(curso, null!));
    }
}