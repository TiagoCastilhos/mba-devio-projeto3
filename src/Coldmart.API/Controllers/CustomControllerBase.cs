using Coldmart.Core.Notificacao;
using Microsoft.AspNetCore.Mvc;

namespace Coldmart.API.Controllers;

public abstract class CustomControllerBase : ControllerBase
{
    private readonly INotificador _notificador;

    public CustomControllerBase(INotificador notificador)
    {
        _notificador = notificador;
    }

    protected IActionResult CustomResponse(object? result = null)
    {
        if (_notificador.TemErro())
        {
            return BadRequest(new ProblemDetails
            {
                Detail = "Houveram erros na sua solicitação.",
                Title = "Erros de validação",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                {
                    { "erros", _notificador.ObterErros() }
                },
                Instance = HttpContext.Request.Path
            });
        }

        return Ok(result);
    }
}