using Coldmart.Core;

namespace Coldmart.Cursos.Domain;

public class Aula : Entity
{
    public required string Titulo { get; set; }
    public required TimeSpan Duracao { get; set; }
    public List<ConteudoProgramatico>? ConteudosProgramaticos { get; set; }
}
