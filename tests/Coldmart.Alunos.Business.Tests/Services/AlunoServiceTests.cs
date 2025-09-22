using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Coldmart.Alunos.Business.Services;
using Coldmart.Alunos.Business.ViewModels;
using Coldmart.Alunos.Data.Contexts;
using Coldmart.Core.Tests.Attributes;
using Moq;

namespace Coldmart.Alunos.Business.Tests.Services;

public class AlunoServiceTests
{
    [Theory, AutoDomainData]
    internal async Task MatricularAoCursoAsync_Should_Add_Matricula_To_DbContext(
        [Frozen] Mock<IAlunosDbContext> dbContext,
        AlunoService service,
        MatriculaViewModel viewModel,
        CancellationToken cancellationToken)
    {
        //act
        await service.MatricularAoCursoAsync(viewModel, cancellationToken);

        //assert

        // ...
    }
}
