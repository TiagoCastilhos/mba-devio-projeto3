using Coldmart.Core.Eventos;
using Coldmart.Core.Notificacao;
using Coldmart.Pagamentos.Business.ViewModels;
using Coldmart.Pagamentos.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Pagamentos.Business.Services;

public class PagamentosService
{
    private readonly IPagamentosDbContext _dbContext;
    private readonly INotificador _notificador;
    private readonly IMediator _mediator;

    public PagamentosService(IPagamentosDbContext dbContext, INotificador notificador, IMediator mediator)
    {
        _dbContext = dbContext;
        _notificador = notificador;
        _mediator = mediator;
    }

    public async Task CriarPagamentoAsync(PagamentoViewModel viewModel, CancellationToken cancellationToken)
    {
        var matricula = await _dbContext.Matriculas.FirstOrDefaultAsync(m => m.Id == viewModel.MatriculaId, cancellationToken);
        if (matricula == null)
        {
            _notificador.AdicionarErro($"Matricula '{viewModel.MatriculaId}' não encontrado.");
            return;
        }

        var dadosCartao = new Domain.DadosCartao(
            viewModel.Cartao.NumeroCartao!,
            viewModel.Cartao.NomeTitular!,
            viewModel.Cartao.DataValidade,
            viewModel.Cartao.CodigoSeguranca!);

        var novoPagamento = new Domain.Pagamento(
            dadosCartao,
            viewModel.Valor,
            viewModel.MatriculaId);

        await _dbContext.Pagamentos.AddAsync(novoPagamento, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AprovarPagamentoAsync(AlterarStatusPagamentoViewModel viewModel, CancellationToken cancellationToken)
    {
        var pagamento = await _dbContext.Pagamentos.FirstOrDefaultAsync(p => p.Id == viewModel.PagamentoId, cancellationToken);
        if (pagamento == null)
        {
            _notificador.AdicionarErro($"Pagamento '{viewModel.PagamentoId}' não encontrado.");
            return;
        }

        pagamento.Aprovar();
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new PagamentoRealizadoEvento { MatriculaId = pagamento.MatriculaId }, cancellationToken);
    }

    public async Task CancelarPagamentoAsync(AlterarStatusPagamentoViewModel viewModel, CancellationToken cancellationToken)
    {
        var pagamento = await _dbContext.Pagamentos.FirstOrDefaultAsync(p => p.Id == viewModel.PagamentoId, cancellationToken);
        if (pagamento == null)
        {
            _notificador.AdicionarErro($"Pagamento '{viewModel.PagamentoId}' não encontrado.");
            return;
        }

        pagamento.Cancelar();
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new PagamentoCanceladoEvento { MatriculaId = pagamento.MatriculaId }, cancellationToken);
    }
}
