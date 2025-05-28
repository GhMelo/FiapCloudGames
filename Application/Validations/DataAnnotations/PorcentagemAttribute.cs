using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Validations.DataAnnotations
{
    public class PorcentagemAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var porcentagem = Convert.ToInt16(value);

            if (porcentagem < 0 || porcentagem > 100)
                return new ValidationResult($"A porcentagem deve ser entre 0 e 100");

            return ValidationResult.Success;
        }
    }
}
