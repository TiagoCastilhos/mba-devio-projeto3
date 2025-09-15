using Coldmart.Core;

namespace Coldmart.Cursos.Domain;

public class Curso : Entity, IAggregateRoot
{
    public TimeSpan DuracaoTotal { get; set; }
    public List<Aula>? Aulas { get; set; }
}
