using Coldmart.Alunos.Business.ViewModels;
using MediatR;

namespace Coldmart.Alunos.Business.Requests;

public class RealizarAulaRequest : IRequest
{
    public required AulaViewModel Aula { get; init; }
}