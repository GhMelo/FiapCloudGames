using System.ComponentModel.DataAnnotations;

namespace Domain.Input.UsuarioJogoInput
{
    public class UsuarioJogoAdquiridoCadastroInput
    {
        [Required(ErrorMessage = "NomeUsuario é obrigatório.")]
        public required string NomeUsuario { get; set; }
        [Required(ErrorMessage = "TituloJogo é obrigatório.")]
        public required string TituloJogo { get; set; }
    }
}
