using System.ComponentModel.DataAnnotations;

namespace Coldmart.Cursos.Business.ViewModels;

public sealed class CursoViewModel
{
    [Required]
    [Length(2, 60)]
    public string? Nome { get; set; }

    [Required]
    [MinLength(1)]
    public List<ConteudoProgramaticoViewModel>? ConteudosProgramaticos { get; set; }
}
