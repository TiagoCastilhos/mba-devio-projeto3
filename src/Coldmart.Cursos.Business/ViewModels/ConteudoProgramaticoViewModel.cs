using System.ComponentModel.DataAnnotations;

namespace Coldmart.Cursos.Business.ViewModels;

public sealed class ConteudoProgramaticoViewModel
{
    [Required]
    [Length(50, 100)]
    public string? Titulo { get; set; }

    [Required]
    [Length(50, 100)]
    public string? Descricao { get; set; }
}