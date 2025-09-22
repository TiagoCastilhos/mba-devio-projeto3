using System.ComponentModel.DataAnnotations;

namespace Coldmart.Alunos.Business.ViewModels;

public class MatriculaViewModel
{
    [Required]
    public Guid CursoId { get; set; }
}
