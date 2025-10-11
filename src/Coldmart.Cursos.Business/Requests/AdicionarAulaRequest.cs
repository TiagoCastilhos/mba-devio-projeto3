using Coldmart.Cursos.Business.ViewModels;
using MediatR;

namespace Coldmart.Cursos.Business.Requests;

public class AdicionarAulaRequest : IRequest
{
    public required AulaViewModel Aula { get; init; }
}