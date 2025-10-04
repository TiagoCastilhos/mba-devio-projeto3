using System.ComponentModel.DataAnnotations;

namespace Coldmart.Pagamentos.Business.ViewModels;

public class PagamentoViewModel
{
    [Required]
    public DadosCartaoViewModel Cartao { get; set; }
    //Adicionar matricula
}

public class DadosCartaoViewModel : IValidatableObject
{
    [Required]
    [Range(13, 19)]
    public string? NumeroCartao { get; set; }

    [Required]
    [Range(5, 60)]
    public string? NomeTitular { get; set; }

    [Required]
    public DateTimeOffset Validade { get; set; }

    [Required]
    [Range(3, 4)]
    public string? CodigoSeguranca { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Validade.Subtract(DateTimeOffset.UtcNow).TotalSeconds < 0)
        {
            yield return new ValidationResult("Cartao expirado.", ["Validade"]);
        }
    }
}