namespace Coldmart.Alunos.Domain;

public class Aula
{
    public Guid Id { get; set; }
    public Guid CursoId { get; set; }
    public Curso Curso { get; set; }

    public Aula() { }
}