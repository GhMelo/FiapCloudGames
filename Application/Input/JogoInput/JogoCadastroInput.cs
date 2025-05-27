using System.ComponentModel.DataAnnotations;

namespace Application.Input.JogoInput
{
    public class JogoCadastroInput
    {
        [Required(ErrorMessage = "Titulo é obrigatório.")]
        public string Titulo { get; set; } = null!;
        [Required(ErrorMessage = "Produtora é obrigatório.")]
        public string Produtora { get; set; } = null!;
    }
}
