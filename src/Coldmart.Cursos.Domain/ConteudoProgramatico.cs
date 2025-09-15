using Coldmart.Core;

namespace Coldmart.Cursos.Domain;

public class ConteudoProgramatico : Entity
{
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
}