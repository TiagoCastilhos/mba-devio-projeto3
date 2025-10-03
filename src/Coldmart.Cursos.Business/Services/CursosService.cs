using Coldmart.Core.Notificacao;
using Coldmart.Cursos.Business.ViewModels;
using Coldmart.Cursos.Data.Contexts;
using Coldmart.Cursos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Cursos.Business.Services;

public class CursosService
{
    private readonly ICursosDbContext _cursosDbContext;
    private readonly INotificador _notificador;

    public CursosService(ICursosDbContext cursosDbContext, INotificador notificador)
    {
        _cursosDbContext = cursosDbContext;
        _notificador = notificador;
    }

    public async Task CriarCursoAsync(CursoViewModel cursoViewModel, CancellationToken cancellationToken)
    {
        var curso = new Curso(cursoViewModel.Nome!);

        foreach (var conteudoProgramaticoViewModel in cursoViewModel.ConteudosProgramaticos!)
        {
            var conteudoProgramatico = new ConteudoProgramatico(curso, conteudoProgramaticoViewModel.Titulo!, conteudoProgramaticoViewModel.Descricao!);
            curso.AdicionarConteudoProgramatico(conteudoProgramatico);

            await _cursosDbContext.ConteudosProgramaticos.AddAsync(conteudoProgramatico, cancellationToken);
        }

        await _cursosDbContext.Cursos.AddAsync(curso, cancellationToken);
        await _cursosDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AdicionarAulaAsync(AulaViewModel aulaViewModel, CancellationToken cancellationToken)
    {
        var curso = await _cursosDbContext.Cursos.FirstOrDefaultAsync(c => c.Id == aulaViewModel.CursoId, cancellationToken);
        if (curso == null)
        {
            _notificador.AdicionarErro($"Curso '{aulaViewModel.CursoId}' não encontrado.");
            return;
        }

        //ToDo: Adicionar suporte aos materiais da aula
        var aula = new Aula(curso, aulaViewModel.Titulo!, TimeSpan.FromSeconds(aulaViewModel.DuracaoSegundos));
        await _cursosDbContext.Aulas.AddAsync(aula, cancellationToken);

        curso.AdicionarAula(aula);
        await _cursosDbContext.SaveChangesAsync(cancellationToken);
    }
}
