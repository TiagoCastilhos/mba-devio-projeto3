using Coldmart.Core.Notificacao;
using Coldmart.Cursos.Business.Requests;
using Coldmart.Cursos.Data.Contexts;
using Coldmart.Cursos.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Cursos.Business.Services;

public class CursosService : IRequestHandler<CriarCursoRequest>
{
    private readonly ICursosDbContext _cursosDbContext;
    private readonly INotificador _notificador;

    public CursosService(ICursosDbContext cursosDbContext, INotificador notificador)
    {
        _cursosDbContext = cursosDbContext;
        _notificador = notificador;
    }

    public async Task Handle(CriarCursoRequest request, CancellationToken cancellationToken)
    {
        var curso = new Curso(request.Curso.Nome!);

        foreach (var conteudoProgramaticoViewModel in request.Curso.ConteudosProgramaticos!)
        {
            var conteudoProgramatico = new ConteudoProgramatico(curso, conteudoProgramaticoViewModel.Titulo!, conteudoProgramaticoViewModel.Descricao!);
            curso.AdicionarConteudoProgramatico(conteudoProgramatico);

            await _cursosDbContext.ConteudosProgramaticos.AddAsync(conteudoProgramatico, cancellationToken);
        }

        await _cursosDbContext.Cursos.AddAsync(curso, cancellationToken);
        await _cursosDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(AdicionarAulaRequest request, CancellationToken cancellationToken)
    {
        var curso = await _cursosDbContext.Cursos.FirstOrDefaultAsync(c => c.Id == request.Aula.CursoId, cancellationToken);
        if (curso == null)
        {
            _notificador.AdicionarErro($"Curso '{request.Aula.CursoId}' não encontrado.");
            return;
        }

        //ToDo: Adicionar suporte aos materiais da aula
        var aula = new Aula(curso, request.Aula.Titulo!, TimeSpan.FromSeconds(request.Aula.DuracaoSegundos));
        await _cursosDbContext.Aulas.AddAsync(aula, cancellationToken);

        curso.AdicionarAula(aula);
        await _cursosDbContext.SaveChangesAsync(cancellationToken);
    }
}
