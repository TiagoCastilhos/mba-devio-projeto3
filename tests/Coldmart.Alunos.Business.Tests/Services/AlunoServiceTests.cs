using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Coldmart.Alunos.Business.Services;
using Coldmart.Alunos.Business.ViewModels;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Alunos.Domain;
using Coldmart.Core;
using Coldmart.Core.Tests.Attributes;
using Coldmart.Core.Tests.Extensions;
using Moq;

namespace Coldmart.Alunos.Business.Tests.Services;

public class AlunoServiceTests
{
    [Theory, AutoDomainData]
    internal async Task MatricularAoCursoAsync_Should_Add_Matricula_To_DbContext(
        [Frozen] Mock<IAlunosDbContext> dbContext,
        [Frozen] Mock<IUsuarioContext> usuarioContext,
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
        dbContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        matriculasDbSet.Verify(m => m.AddAsync(It.IsAny<Matricula>(), cancellationToken), Times.Once);
    }
}
