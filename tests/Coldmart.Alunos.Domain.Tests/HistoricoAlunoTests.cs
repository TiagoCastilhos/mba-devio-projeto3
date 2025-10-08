using Coldmart.Core.Tests.Attributes;

namespace Coldmart.Alunos.Domain.Tests;

public class HistoricoAlunoTests
{
    [Theory, AutoDomainData]
    public void CriarHistoricoAluno_FornecidosIdsValidos_DeveCriar(Guid alunoId, Guid aulaId, Guid cursoId)
    {
        //act
        var historico = new HistoricoAluno(alunoId, aulaId, cursoId);
        //assert
        Assert.Equal(alunoId, historico.AlunoId);
        Assert.Equal(aulaId, historico.AulaId);
        Assert.Equal(cursoId, historico.CursoId);
    }

    [Theory, AutoDomainData]
    public void CriarHistoricoAluno_AlunoIdInvalido_DeveLancarExcecao(Guid aulaId, Guid cursoId)
    {
        //act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new HistoricoAluno(Guid.Empty, aulaId, cursoId));
    }

    [Theory, AutoDomainData]
    public void CriarHistoricoAluno_AulaIdInvalido_DeveLancarExcecao(Guid alunoId, Guid cursoId)
    {
        //act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new HistoricoAluno(alunoId, Guid.Empty, cursoId));
    }

    [Theory, AutoDomainData]
    public void CriarHistoricoAluno_CursoIdInvalido_DeveLancarExcecao(Guid alunoId, Guid aulaId)
    {
        //act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new HistoricoAluno(alunoId, aulaId, Guid.Empty));
    }
}