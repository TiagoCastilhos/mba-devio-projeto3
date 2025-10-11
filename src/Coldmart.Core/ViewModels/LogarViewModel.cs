using System.ComponentModel.DataAnnotations;

namespace Coldmart.Core.ViewModels;

public class LogarViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?=.{6,})(?:(.)(?!.*\1)){6,}$")]
    public string Senha { get; set; }
}
