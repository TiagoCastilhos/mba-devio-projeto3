using AutoFixture.Xunit2;
using Coldmart.Core.Notificacao;
using Coldmart.Core.Tests.Attributes;
using Coldmart.Core.Tests.Extensions;
using Coldmart.Cursos.Business.Requests;
using Coldmart.Cursos.Business.Services;
using Coldmart.Cursos.Data.Contexts;
using Coldmart.Cursos.Domain;
using Moq;

namespace Coldmart.Cursos.Business.Tests.Services;

public class CursosServiceTests
{
    [Theory, AutoDomainData]
    internal async Task HandleCriarCurso_FornecidosCursoEConteudo_DeveAdicionar(
        [Frozen] Mock<ICursosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        CursosService service,
        List<Curso> cursos, List<ConteudoProgramatico> conteudosProgramaticos,
        CriarCursoRequest request,
        CancellationToken cancellationToken)
    {
        //arrange
        var cursosDbSet = DbSetHelper.CreateMockedDbSet(cursos);
        dbContext.Setup(db => db.Cursos).Returns(cursosDbSet.Object);

        var conteudosProgramaticosDbSet = DbSetHelper.CreateMockedDbSet(conteudosProgramaticos);
        dbContext.Setup(db => db.ConteudosProgramaticos).Returns(conteudosProgramaticosDbSet.Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.IsAny<string>()), Times.Never);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        cursosDbSet.Verify(m => m.AddAsync(It.Is<Curso>(c => c.Nome == request.Curso.Nome), cancellationToken), Times.Once);

        Assert.All(request.Curso.ConteudosProgramaticos!, cp =>
            conteudosProgramaticosDbSet.Verify(m => m.AddAsync(It.Is<ConteudoProgramatico>(c =>
                c.Titulo == cp.Titulo && c.Descricao == cp.Descricao), cancellationToken), Times.Once));
    }

    [Theory, AutoDomainData]
    internal async Task HandleAdicionarAula_FornecidaAula_DeveAdicionar(
        [Frozen] Mock<ICursosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        CursosService service,
        Curso curso, List<Aula> aulas,
        AdicionarAulaRequest request,
        CancellationToken cancellationToken)
    {
        //arrange
        request.Aula.CursoId = curso.Id;
        var cursosDbSet = DbSetHelper.CreateMockedDbSet([curso]);
        dbContext.Setup(db => db.Cursos).Returns(cursosDbSet.Object);

        var aulasDbSet = DbSetHelper.CreateMockedDbSet(aulas);
        dbContext.Setup(db => db.Aulas).Returns(aulasDbSet.Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.IsAny<string>()), Times.Never);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);

        aulasDbSet.Verify(m => m.AddAsync(
            It.Is<Aula>(c => c.Titulo == request.Aula.Titulo && c.Duracao.TotalSeconds == request.Aula.DuracaoSegundos), cancellationToken)
        , Times.Once);
    }

    [Theory, AutoDomainData]
    internal async Task HandleAdicionarAula_CursoNaoExiste_DeveAdicionarErro(
        [Frozen] Mock<ICursosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        CursosService service,
        Curso curso, List<Aula> aulas,
        AdicionarAulaRequest request,
        CancellationToken cancellationToken)
    {
        //arrange
        var cursosDbSet = DbSetHelper.CreateMockedDbSet([curso]);
        dbContext.Setup(db => db.Cursos).Returns(cursosDbSet.Object);

        var aulasDbSet = DbSetHelper.CreateMockedDbSet(aulas);
        dbContext.Setup(db => db.Aulas).Returns(aulasDbSet.Object);

        //act
        await service.Handle(request, cancellationToken);

        //assert
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(request.Aula.CursoId.ToString()))), Times.Once);
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Never);
        aulasDbSet.Verify(m => m.AddAsync(It.IsAny<Aula>(), cancellationToken), Times.Never);
    }
}
