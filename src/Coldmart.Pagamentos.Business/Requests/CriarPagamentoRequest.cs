using Coldmart.Pagamentos.Business.ViewModels;
using MediatR;

namespace Coldmart.Pagamentos.Business.Requests;

public class CriarPagamentoRequest : IRequest
{
    public required PagamentoViewModel Pagamento { get; init; }
}
