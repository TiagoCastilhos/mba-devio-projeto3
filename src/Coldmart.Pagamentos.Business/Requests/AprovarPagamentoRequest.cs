using Coldmart.Pagamentos.Business.ViewModels;
using MediatR;

namespace Coldmart.Pagamentos.Business.Requests;

public class AprovarPagamentoRequest : IRequest
{
    public required AlterarStatusPagamentoViewModel Pagamento { get; init; }
}
