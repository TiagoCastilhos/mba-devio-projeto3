using System.ComponentModel.DataAnnotations;

namespace Coldmart.Alunos.Business.ViewModels;

public class AulaViewModel
{
    [Required]
    public Guid AulaId { get; set; }
}
