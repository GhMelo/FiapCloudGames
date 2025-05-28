using System.ComponentModel.DataAnnotations;
using Application.Validations.DataAnnotations;

namespace Application.Input.PromocaoInput
{
    public class PromocaoCadastroInput
    {
        [Required(ErrorMessage = "JogoId é obrigatório.")]
        public required int JogoId { get; set; }

        [Required(ErrorMessage = "NomePromocao é obrigatório.")]
        public required string NomePromocao { get; set; }

        [Required(ErrorMessage = "Porcentagem é obrigatória.")]
        [PorcentagemAttribute]
        public required int Porcentagem { get; set; }

        [Required(ErrorMessage = "PromocaoAtiva é obrigatório.")]
        public required bool PromocaoAtiva { get; set; }
    }
}
