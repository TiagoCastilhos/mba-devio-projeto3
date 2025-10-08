using AutoFixture.Xunit2;
using Coldmart.Core.Eventos;
using Coldmart.Core.Notificacao;
using Coldmart.Core.Tests.Attributes;
using Coldmart.Core.Tests.Extensions;
using Coldmart.Pagamentos.Business.Services;
using Coldmart.Pagamentos.Business.ViewModels;
using Coldmart.Pagamentos.Data.Contexts;
using Coldmart.Pagamentos.Domain;
using MediatR;
using Moq;

namespace Coldmart.Pagamentos.Business.Tests.Services;

public class PagamentosServiceTests
{
    [Theory, AutoDomainData]
    public async Task CriarPagamento_FornecidoDadosPagamento_DeveCriar(
        [Frozen] Mock<IPagamentosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        Matricula matricula, List<Pagamento> pagamentos,
        PagamentosService pagamentosService, 
        PagamentoViewModel pagamentoViewModel,
        CancellationToken cancellationToken)
    {
        //arrange
        pagamentoViewModel.MatriculaId = matricula.Id;
        dbContext.Setup(db => db.Matriculas).Returns(DbSetHelper.CreateMockedDbSet([matricula]).Object);

        var pagamentosDbSet = DbSetHelper.CreateMockedDbSet(pagamentos);
        dbContext.Setup(db => db.Pagamentos).Returns(pagamentosDbSet.Object);

        //act
        await pagamentosService.CriarPagamentoAsync(pagamentoViewModel, cancellationToken);

        //assert
        dbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        pagamentosDbSet.Verify(m => m.AddAsync(It.Is<Pagamento>(
            p => p.MatriculaId == pagamentoViewModel.MatriculaId && p.Valor == pagamentoViewModel.Valor), It.IsAny<CancellationToken>())
        , Times.Once);
        notificador.Verify(n => n.AdicionarErro(It.IsAny<string>()), Times.Never);

    }

    [Theory, AutoDomainData]
    public async Task CriarPagamento_FornecidoDadosPagamento_MatriculaNaoExiste_DeveAdicionarErro(
        [Frozen] Mock<IPagamentosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        List<Matricula> matriculas, List<Pagamento> pagamentos,
        PagamentosService pagamentosService,
        PagamentoViewModel pagamentoViewModel,
        CancellationToken cancellationToken)
    {
        //arrange
        dbContext.Setup(db => db.Matriculas).Returns(DbSetHelper.CreateMockedDbSet(matriculas).Object);

        var pagamentosDbSet = DbSetHelper.CreateMockedDbSet(pagamentos);
        dbContext.Setup(db => db.Pagamentos).Returns(pagamentosDbSet.Object);

        //act
        await pagamentosService.CriarPagamentoAsync(pagamentoViewModel, cancellationToken);

        //assert
        dbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        pagamentosDbSet.Verify(m => m.AddAsync(It.IsAny<Pagamento>(), It.IsAny<CancellationToken>()), Times.Never);
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(pagamentoViewModel.MatriculaId.ToString()))), Times.Once);
    }

    [Theory, AutoDomainData]
    public async Task AprovarPagamentoAsync_FornecidoPagamento_DeveAtualizar(
        [Frozen] Mock<IPagamentosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        [Frozen] Mock<IMediator> mediator,
        Pagamento pagamento,
        PagamentosService pagamentosService,
        AlterarStatusPagamentoViewModel viewModel,
        CancellationToken cancellationToken)
    {
        //arrange
        viewModel.PagamentoId = pagamento.Id;
        dbContext.Setup(db => db.Pagamentos).Returns(DbSetHelper.CreateMockedDbSet([pagamento]).Object);

        //act
        await pagamentosService.AprovarPagamentoAsync(viewModel, cancellationToken);

        //assert
        dbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        notificador.Verify(n => n.AdicionarErro(It.IsAny<string>()), Times.Never);
        Assert.Equal(StatusPagamento.Aprovado, pagamento.Status);
        mediator.Verify(m => m.Publish(It.IsAny<PagamentoRealizadoEvento>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory, AutoDomainData]
    public async Task AprovarPagamentoAsync_PagamentoNaoEncontrado_DeveAdicionarErro(
        [Frozen] Mock<IPagamentosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        [Frozen] Mock<IMediator> mediator,
        Pagamento pagamento,
        PagamentosService pagamentosService,
        AlterarStatusPagamentoViewModel viewModel,
        CancellationToken cancellationToken)
    {
        //arrange
        dbContext.Setup(db => db.Pagamentos).Returns(DbSetHelper.CreateMockedDbSet([pagamento]).Object);

        //act
        await pagamentosService.AprovarPagamentoAsync(viewModel, cancellationToken);

        //assert
        dbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(viewModel.PagamentoId.ToString()))), Times.Once);
        mediator.Verify(m => m.Publish(It.IsAny<PagamentoRealizadoEvento>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory, AutoDomainData]
    public async Task CancelarPagamentoAsync_FornecidoPagamento_DeveAtualizar(
        [Frozen] Mock<IPagamentosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        [Frozen] Mock<IMediator> mediator,
        Pagamento pagamento,
        PagamentosService pagamentosService,
        AlterarStatusPagamentoViewModel viewModel,
        CancellationToken cancellationToken)
    {
        //arrange
        viewModel.PagamentoId = pagamento.Id;
        dbContext.Setup(db => db.Pagamentos).Returns(DbSetHelper.CreateMockedDbSet([pagamento]).Object);

        //act
        await pagamentosService.CancelarPagamentoAsync(viewModel, cancellationToken);

        //assert
        dbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        notificador.Verify(n => n.AdicionarErro(It.IsAny<string>()), Times.Never);
        Assert.Equal(StatusPagamento.Cancelado, pagamento.Status);
        mediator.Verify(m => m.Publish(It.IsAny<PagamentoCanceladoEvento>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory, AutoDomainData]
    public async Task CancelarPagamentoAsync_PagamentoNaoEncontrado_DeveAdicionarErro(
        [Frozen] Mock<IPagamentosDbContext> dbContext,
        [Frozen] Mock<INotificador> notificador,
        [Frozen] Mock<IMediator> mediator,
        Pagamento pagamento,
        PagamentosService pagamentosService,
        AlterarStatusPagamentoViewModel viewModel,
        CancellationToken cancellationToken)
    {
        //arrange
        dbContext.Setup(db => db.Pagamentos).Returns(DbSetHelper.CreateMockedDbSet([pagamento]).Object);

        //act
        await pagamentosService.CancelarPagamentoAsync(viewModel, cancellationToken);

        //assert
        dbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        notificador.Verify(n => n.AdicionarErro(It.Is<string>(s => s.Contains(viewModel.PagamentoId.ToString()))), Times.Once);
        mediator.Verify(m => m.Publish(It.IsAny<PagamentoCanceladoEvento>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
