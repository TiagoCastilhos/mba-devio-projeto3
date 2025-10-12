using Coldmart.Alunos.Business.ViewModels;
using MediatR;

namespace Coldmart.Alunos.Business.Requests;

public class RealizarAulaRequest : IRequest
{
    public required RealizarAulaViewModel Aula { get; init; }
}