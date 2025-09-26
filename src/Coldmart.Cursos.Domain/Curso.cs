using Coldmart.Core.Domain;

namespace Coldmart.Cursos.Domain;

public class Curso : Entity, IAggregateRoot
{
    public required string Nome { get; set; }
    public TimeSpan DuracaoTotal { get; set; }
    public List<Aula>? Aulas { get; set; }
}
