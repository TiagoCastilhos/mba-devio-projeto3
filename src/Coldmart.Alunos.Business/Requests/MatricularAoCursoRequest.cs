using Coldmart.Alunos.Business.ViewModels;
using MediatR;

namespace Coldmart.Alunos.Business.Requests;

public class MatricularAoCursoRequest : IRequest
{
    public required MatriculaViewModel Matricula { get; init; }
}
