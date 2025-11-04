using Coldmart.Core.Constants;
using Coldmart.Core.Notificacao;
using Coldmart.Pagamentos.Business.Requests;
using Coldmart.Pagamentos.Business.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coldmart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagamentosController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public PagamentosController(IMediator mediator, INotificador notificador)
        : base(notificador)
    {
        _mediator = mediator;
    }

    [HttpPost("")]
    [Authorize(Roles = RolesConstants.Usuario)]
    public async Task<IActionResult> CriarPagamentoAsync([FromBody] PagamentoViewModel pagamento)
    {
        await _mediator.Send(new CriarPagamentoRequest()
        {
            Pagamento = pagamento
        }, HttpContext.RequestAborted);

        return CustomResponse();
    }

    [HttpPut("{id:guid}/aprovar")]
    [Authorize(Roles = RolesConstants.Admin)]
    public async Task<IActionResult> AprovarPagamentoAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new AprovarPagamentoRequest
        {
            Pagamento = new AlterarStatusPagamentoViewModel { PagamentoId = id }
        }, HttpContext.RequestAborted);

        return CustomResponse();
    }

    [HttpPut("{id:guid}/cancelar")]
    [Authorize(Roles = RolesConstants.Admin)]
    public async Task<IActionResult> CancelarPagamentoAsync([FromRoute] Guid id)
    {
        await _mediator.Send(new CancelarPagamentoRequest
        {
            Pagamento = new AlterarStatusPagamentoViewModel { PagamentoId = id }
        }, HttpContext.RequestAborted);

        return CustomResponse();
    }
}
