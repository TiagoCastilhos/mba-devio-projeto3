using AutoFixture.Xunit2;
using Coldmart.Alunos.Business.Services;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core.Eventos;
using Coldmart.Core.Tests.Attributes;
using Coldmart.Core.Tests.Extensions;
using Moq;

namespace Coldmart.Alunos.Business.Tests.Services;

public class AlunosEventosServiceTests
{
    [Theory, AutoDomainData]
    public async Task Handle_AulaRealizadaEvento_DeveGerarCertificado(
        [Frozen] Mock<IAlunosDbContext> dbContext,
        AlunosEventosService service,
        AulaRealizadaEvento evento,
        Aluno aluno,
        Curso curso,
        List<Aula> aulasCurso,
        List<HistoricoAluno> historicosAlunos,
        CancellationToken cancellationToken)
    {
        //arrange
        evento.AlunoId = aluno.Id;
        evento.CursoId = curso.Id;

        aulasCurso =
        [
            new Aula { Id = Guid.NewGuid(), CursoId = curso.Id },
            new Aula { Id = Guid.NewGuid(), CursoId = curso.Id }
        ];

        historicosAlunos =
        [
            new HistoricoAluno(aluno.Id, aulasCurso[0].Id, curso.Id),
            new HistoricoAluno(aluno.Id, aulasCurso[1].Id, curso.Id)
        ];

        dbContext.Setup(db => db.Aulas)
            .Returns(DbSetHelper.CreateMockedDbSet(aulasCurso).Object);

        dbContext.Setup(db => db.HistoricosAlunos)
            .Returns(DbSetHelper.CreateMockedDbSet(historicosAlunos).Object);

        dbContext.Setup(db => db.Cursos)
            .Returns(DbSetHelper.CreateMockedDbSet([curso]).Object);

        dbContext.Setup(db => db.Alunos)
            .Returns(DbSetHelper.CreateMockedDbSet([aluno]).Object);

        var certificadosDbSet = DbSetHelper.CreateMockedDbSet(new List<Certificado>());
        dbContext.Setup(db => db.Certificados).Returns(certificadosDbSet.Object);

        //act
        await service.Handle(evento, cancellationToken);

        //assert
        certificadosDbSet.Verify(c => c.AddAsync(It.IsAny<Certificado>(), cancellationToken), Times.Once);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
    }

    [Theory, AutoDomainData]
    public async Task Handle_AulaRealizadaEvento_QuandoFaltamAulasASeremRealizadas_NaoDeveGerarCertificado(
       [Frozen] Mock<IAlunosDbContext> dbContext,
       AlunosEventosService service,
       AulaRealizadaEvento evento,
       Aluno aluno,
       Curso curso,
       List<Aula> aulasCurso,
       List<HistoricoAluno> historicosAlunos,
       CancellationToken cancellationToken)
    {
        //arrange
        evento.AlunoId = aluno.Id;
        evento.CursoId = curso.Id;

        aulasCurso =
        [
            new Aula { Id = Guid.NewGuid(), CursoId = curso.Id },
            new Aula { Id = Guid.NewGuid(), CursoId = curso.Id }
        ];

        historicosAlunos =
        [
            new HistoricoAluno(aluno.Id, aulasCurso[0].Id, curso.Id)
        ];

        dbContext.Setup(db => db.Aulas)
            .Returns(DbSetHelper.CreateMockedDbSet(aulasCurso).Object);

        dbContext.Setup(db => db.HistoricosAlunos)
            .Returns(DbSetHelper.CreateMockedDbSet(historicosAlunos).Object);

        dbContext.Setup(db => db.Cursos)
            .Returns(DbSetHelper.CreateMockedDbSet([curso]).Object);

        dbContext.Setup(db => db.Alunos)
            .Returns(DbSetHelper.CreateMockedDbSet([aluno]).Object);

        var certificadosDbSet = DbSetHelper.CreateMockedDbSet(new List<Certificado>());
        dbContext.Setup(db => db.Certificados).Returns(certificadosDbSet.Object);

        //act
        await service.Handle(evento, cancellationToken);

        //assert
        certificadosDbSet.Verify(c => c.AddAsync(It.IsAny<Certificado>(), cancellationToken), Times.Never);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Never);
    }
}
