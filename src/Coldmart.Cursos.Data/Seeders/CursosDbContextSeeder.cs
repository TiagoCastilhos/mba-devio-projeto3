using Coldmart.Core.Data.Seeders;
using Coldmart.Cursos.Data.Contexts;
using Coldmart.Cursos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coldmart.Cursos.Data.Seeders;

public class CursosDbContextSeeder : IDbContextSeeder
{
    private readonly ICursosDbContext _cursosDbContext;

    public CursosDbContextSeeder(ICursosDbContext cursosDbContext)
    {
        _cursosDbContext = cursosDbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        if (await _cursosDbContext.Cursos.AnyAsync(cancellationToken))
            return;

        var curso = new Curso("Introdução ao C#");
        await _cursosDbContext.Cursos.AddAsync(curso, cancellationToken);

        var conteudoProgramatico = new ConteudoProgramatico(curso, "Variáveis e Tipos de Dados", "Aprenderemos a declarar variáveis de diversos tipos e a forma de alocação de memória para cada tipo.");
        curso.AdicionarConteudoProgramatico(conteudoProgramatico);
        await _cursosDbContext.ConteudosProgramaticos.AddAsync(conteudoProgramatico, cancellationToken);

        var aula = new Aula(curso, "Aula 1 - Variáveis", TimeSpan.FromMinutes(30));
        curso.AdicionarAula(aula);
        await _cursosDbContext.Aulas.AddAsync(aula, cancellationToken);

        await _cursosDbContext.SaveChangesAsync(cancellationToken);
    }
}
