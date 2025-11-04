using Coldmart.Alunos.Business.Requests;
using Coldmart.Alunos.Business.ViewModels;
using Coldmart.Core.Notificacao;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coldmart.API.Controllers;

[ApiController, Authorize]
[Route("api/[controller]")]
public class AlunosController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public AlunosController(IMediator mediator, INotificador notificador)
        : base(notificador)
    {
        _mediator = mediator;
    }

    [HttpPost("cursos")]
    public async Task<IActionResult> MatricularAoCurso([FromBody] MatriculaViewModel viewModel)
    {
        await _mediator.Send(new MatricularAoCursoRequest
        {
            Matricula = viewModel
        }, HttpContext.RequestAborted);

        return CustomResponse();
    }

    [HttpPut("cursos/aulas")]
    public async Task<IActionResult> RealizarAula([FromBody] RealizarAulaViewModel viewModel)
    {
        await _mediator.Send(new RealizarAulaRequest
        {
            Aula = viewModel
        }, HttpContext.RequestAborted);

        return CustomResponse();
    }
}