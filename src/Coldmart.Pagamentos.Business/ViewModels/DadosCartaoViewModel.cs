using System.ComponentModel.DataAnnotations;

namespace Coldmart.Pagamentos.Business.ViewModels;

public class DadosCartaoViewModel : IValidatableObject
{
    [Required]
    [MinLength(13), MaxLength(19)]
    public string NumeroCartao { get; set; }

    [Required]
    [MinLength(5), MaxLength(60)]
    public string NomeTitular { get; set; }

    [Required]
    public DateTimeOffset DataValidade { get; set; }

    [Required]
    [MinLength(3), MaxLength(4)]
    public string CodigoSeguranca { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DataValidade.Subtract(DateTimeOffset.UtcNow).TotalSeconds < 0)
        {
            yield return new ValidationResult("Cartao expirado.", ["Validade"]);
        }
    }
}