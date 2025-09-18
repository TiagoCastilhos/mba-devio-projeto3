using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Alunos.Domain.Tests;

public class AlunoTests
{
    [Theory, AutoDomainData]
    public void CriarAluno_FornecidosNomeEEmail_DeveCriar(string nome, string email)
    {
        //act
        var aluno = new Aluno(nome, email);

        //assert
        Assert.Equal(nome, aluno.Nome);
        Assert.Equal(email, aluno.Email);
    }

    [Theory, AutoDomainData]
    public void CriarAluno_NomeVazioOuNulo_DeveLancarExcecao(string email)
    {
        //act & assert
        Assert.Throws<ArgumentException>(() => new Aluno("", email));
        Assert.Throws<ArgumentNullException>(() => new Aluno(null!, email));
    }

    [Theory, AutoDomainData]
    public void CriarAluno_EmailVazioOuNulo_DeveLancarExcecao(string nome)
    {
        //act & assert
        Assert.Throws<ArgumentException>(() => new Aluno(nome, ""));
        Assert.Throws<ArgumentNullException>(() => new Aluno(nome, null!));
    }

    [Theory, AutoDomainData]
    public void AdicionarMatricula_Fornecida_DeveAdicionar(Matricula matricula, Aluno aluno)
    {
        //act
        aluno.AdicionarMatricula(matricula);

        //assert
        Assert.Contains(matricula, aluno.Matriculas!);
    }

    [Theory, AutoDomainData]
    public void AdicionarMatricula_MatriculaNula_DeveLancarExcecao(Aluno aluno)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => aluno.AdicionarMatricula(null!));
    }

    [Theory, AutoDomainData]
    public void AdicionarCertificado_Fornecido_DeveAdicionar(Certificado certificado, Aluno aluno)
    {
        //act
        aluno.AdicionarCertificado(certificado);

        //assert
        Assert.Contains(certificado, aluno.Certificados!);
    }

    [Theory, AutoDomainData]
    public void AdicionarCertificado_CertificadoNulo_DeveLancarExcecao(Aluno aluno)
    {
        //act & assert
        Assert.Throws<ArgumentNullException>(() => aluno.AdicionarCertificado(null!));
    }
}
