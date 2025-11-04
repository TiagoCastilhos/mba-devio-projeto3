using Coldmart.Core.Constants;
using Coldmart.Core.Notificacao;
using Coldmart.Cursos.Business.Requests;
using Coldmart.Cursos.Business.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coldmart.API.Controllers;

[ApiController, Authorize(Roles = RolesConstants.Admin)]
[Route("api/[controller]")]
public class CursosController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public CursosController(IMediator mediator, INotificador notificador) 
        : base(notificador)
    {
        _mediator = mediator;
    }

    [HttpPost("")]
    public async Task<IActionResult> CriarCursoAsync([FromBody] CursoViewModel viewModel)
    {
        await _mediator.Send(new CriarCursoRequest
        {
            Curso = viewModel
        }, HttpContext.RequestAborted);

        return CustomResponse();
    }

    [HttpPost("{id:guid}/aulas")]
    public async Task<IActionResult> AdicionarAulaAsync([FromRoute] Guid id, [FromBody] AulaViewModel viewModel)
    {
        await _mediator.Send(new AdicionarAulaRequest
        {
            Aula = viewModel
        }, HttpContext.RequestAborted);
        return CustomResponse();
    }
}