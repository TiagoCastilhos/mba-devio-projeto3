using AutoFixture.Xunit2;
using Coldmart.Alunos.Business.Requests;
using Coldmart.Alunos.Business.Services;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core.Contexts;
using Coldmart.Core.Eventos;
using Coldmart.Core.Notificacao;
using Coldmart.Core.Tests.Attributes;
using Coldmart.Core.Tests.Extensions;
using MediatR;
using Moq;

namespace Coldmart.Alunos.Business.Tests.Services;

public class AlunosServiceTests
{
    [Theory, AutoDomainData]
    public async Task HandleMatricularAoCurso_CursoEAlunoFornecidos_DeveAdicionar(
        [Frozen] Mock<IAlunosDbContext> dbContext,
        [Frozen] Mock<IUsuarioContext> usuarioContext,
        [Frozen] Mock<INotificador> notificador,
        [Frozen] Mock<IMediator> mediator,
        Aluno aluno, Curso curso, List<Matricula> matriculas,
        AlunosService service,
        MatricularAoCursoRequest request,
        CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        request.Matricula.CursoId = curso.Id;

        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet([aluno]).Object);
        dbContext.Setup(db => db.Cursos).Returns(DbSetHelper.CreateMockedDbSet([curso]).Object);

        var matriculasDbSet = DbSetHelper.CreateMockedDbSet(matriculas);
        dbContext.Setup(db => db.Matriculas).Returns(matriculasDbSet.Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.IsAny<string>()), Times.Never);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        matriculasDbSet.Verify(m => m.AddAsync(It.IsAny<Matricula>(), cancellationToken), Times.Once);
        mediator.Verify(m => m.Publish(It.IsAny<MatriculaRealizadaEvento>(), cancellationToken), Times.Once);
    }

    [Theory, AutoDomainData]
    public async Task HandleMatricularAoCurso_CursoNaoEncontrado_DeveAdicionarErro(
        [Frozen] Mock<IAlunosDbContext> dbContext,
        [Frozen] Mock<IUsuarioContext> usuarioContext,
        [Frozen] Mock<INotificador> notificador,
        [Frozen] Mock<IMediator> mediator,
        Aluno aluno, Curso curso, List<Curso> cursos,
        AlunosService service,
        MatricularAoCursoRequest request,
        CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        request.Matricula.CursoId = curso.Id;

        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet([aluno]).Object);
        dbContext.Setup(db => db.Cursos).Returns(DbSetHelper.CreateMockedDbSet(cursos).Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(request.Matricula.CursoId.ToString()))), Times.Once);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Never);
        mediator.Verify(m => m.Publish(It.IsAny<MatriculaRealizadaEvento>(), cancellationToken), Times.Never);
    }

    [Theory, AutoDomainData]
    public async Task HandleMatricularAoCurso_AlunoNaoEncontrado_DeveAdicionarErro(
       [Frozen] Mock<IAlunosDbContext> dbContext,
       [Frozen] Mock<IUsuarioContext> usuarioContext,
       [Frozen] Mock<INotificador> notificador,
       [Frozen] Mock<IMediator> mediator,
       Aluno aluno, Curso curso, List<Aluno> alunos,
       AlunosService service,
       MatricularAoCursoRequest request,
       CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        request.Matricula.CursoId = curso.Id;

        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet(alunos).Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(aluno.Id.ToString()))), Times.Once);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Never);
        mediator.Verify(m => m.Publish(It.IsAny<MatriculaRealizadaEvento>(), cancellationToken), Times.Never);
    }

    [Theory, AutoDomainData]
    public async Task HandleRealizarAula_AulaEAlunoFornecidos_DeveAdicionar(
        [Frozen] Mock<IAlunosDbContext> dbContext,
        [Frozen] Mock<IUsuarioContext> usuarioContext,
        [Frozen] Mock<INotificador> notificador,
        [Frozen] Mock<IMediator> mediator,
        Aluno aluno, Aula aula, List<HistoricoAluno> historicosAlunos,
        AlunosService service,
        RealizarAulaRequest request,
        CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        request.Aula.AulaId = aula.Id;
        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet([aluno]).Object);
        dbContext.Setup(db => db.Aulas).Returns(DbSetHelper.CreateMockedDbSet([aula]).Object);

        var historicosAlunosDbSet = DbSetHelper.CreateMockedDbSet(historicosAlunos);
        dbContext.Setup(db => db.HistoricosAlunos).Returns(historicosAlunosDbSet.Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.IsAny<string>()), Times.Never);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        historicosAlunosDbSet.Verify(m => m.AddAsync(It.IsAny<HistoricoAluno>(), cancellationToken), Times.Once);
        mediator.Verify(m => m.Publish(It.IsAny<AulaRealizadaEvento>(), cancellationToken), Times.Once);
    }

    [Theory, AutoDomainData]
    public async Task HandleRealizarAula_AulaNaoEncontrada_DeveAdicionarErro(
       [Frozen] Mock<IAlunosDbContext> dbContext,
       [Frozen] Mock<IUsuarioContext> usuarioContext,
       [Frozen] Mock<INotificador> notificador,
       [Frozen] Mock<IMediator> mediator,
       Aluno aluno, Aula aula, List<Aula> aulas,
       AlunosService service,
       RealizarAulaRequest request,
       CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        request.Aula.AulaId = aula.Id;
        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet([aluno]).Object);
        dbContext.Setup(db => db.Aulas).Returns(DbSetHelper.CreateMockedDbSet(aulas).Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(request.Aula.AulaId.ToString()))), Times.Once);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Never);
        mediator.Verify(m => m.Publish(It.IsAny<AulaRealizadaEvento>(), cancellationToken), Times.Never);
    }

    [Theory, AutoDomainData]
    public async Task HandleRealizarAula_AlunoNaoEncontrado_DeveAdicionarErro(
       [Frozen] Mock<IAlunosDbContext> dbContext,
       [Frozen] Mock<IUsuarioContext> usuarioContext,
       [Frozen] Mock<INotificador> notificador,
       [Frozen] Mock<IMediator> mediator,
       Aluno aluno, Aula aula, List<Aluno> alunos,
       AlunosService service,
       RealizarAulaRequest request,
       CancellationToken cancellationToken)
    {
        //arrange
        usuarioContext.Setup(uc => uc.ObterIdUsuario()).Returns(aluno.Id);
        request.Aula.AulaId = aula.Id;
        dbContext.Setup(db => db.Aulas).Returns(DbSetHelper.CreateMockedDbSet([aula]).Object);
        dbContext.Setup(db => db.Alunos).Returns(DbSetHelper.CreateMockedDbSet(alunos).Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(aluno.Id.ToString()))), Times.Once);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Never);
        mediator.Verify(m => m.Publish(It.IsAny<AulaRealizadaEvento>(), cancellationToken), Times.Never);
    }
}
