using AutoFixture.Xunit2;
using Coldmart.Alunos.Business.Services;
using Coldmart.Alunos.Business.ViewModels;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core.Contexts;
using Coldmart.Core.Notificacao;
using Coldmart.Core.Tests.Attributes;
using Coldmart.Core.Tests.Extensions;
using Moq;

namespace Coldmart.Alunos.Business.Tests.Services;

public class AlunoServiceTests
{
    [Theory, AutoDomainData]
    internal async Task MatricularAoCursoAsync_CursoEAlunoFornecidos_DeveAdicionar(
        [Frozen] Mock<IAlunosDbContext> dbContext,
        [Frozen] Mock<IUsuarioContext> usuarioContext,
        [Frozen] Mock<INotificador> notificador,
        Aluno aluno, Curso curso, List<Matricula> matriculas,
        AlunoService service,
        MatriculaViewModel viewModel,
        CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        viewModel.CursoId = curso.Id;

        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet([aluno]).Object);
        dbContext.Setup(db => db.Cursos).Returns(DbSetHelper.CreateMockedDbSet([curso]).Object);

        var matriculasDbSet = DbSetHelper.CreateMockedDbSet(matriculas);
        dbContext.Setup(db => db.Matriculas).Returns(matriculasDbSet.Object);

        //act
        await service.MatricularAoCursoAsync(viewModel, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.IsAny<string>()), Times.Never);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        matriculasDbSet.Verify(m => m.AddAsync(It.IsAny<Matricula>(), cancellationToken), Times.Once);
    }

    [Theory, AutoDomainData]
    internal async Task MatricularAoCursoAsync_CursoNaoEncontrado_DeveAdicionarErro(
        [Frozen] Mock<IAlunosDbContext> dbContext,
        [Frozen] Mock<IUsuarioContext> usuarioContext,
        [Frozen] Mock<INotificador> notificador,
        Aluno aluno, Curso curso, List<Curso> cursos,
        AlunoService service,
        MatriculaViewModel viewModel,
        CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        viewModel.CursoId = curso.Id;

        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet([aluno]).Object);
        dbContext.Setup(db => db.Cursos).Returns(DbSetHelper.CreateMockedDbSet(cursos).Object);

        //act
        await service.MatricularAoCursoAsync(viewModel, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(viewModel.CursoId.ToString()))), Times.Once);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Never);
    }

    [Theory, AutoDomainData]
    internal async Task MatricularAoCursoAsync_AlunoNaoEncontrado_DeveAdicionarErro(
       [Frozen] Mock<IAlunosDbContext> dbContext,
       [Frozen] Mock<IUsuarioContext> usuarioContext,
       [Frozen] Mock<INotificador> notificador,
       Aluno aluno, Curso curso, List<Aluno> alunos,
       AlunoService service,
       MatriculaViewModel viewModel,
       CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        viewModel.CursoId = curso.Id;

        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet(alunos).Object);

        //act
        await service.MatricularAoCursoAsync(viewModel, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(aluno.Id.ToString()))), Times.Once);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Never);
    }
}
