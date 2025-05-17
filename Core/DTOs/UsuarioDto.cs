using Core.Entity;

namespace Core.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public TipoUsuario Tipo { get; set; }
        public  ICollection<JogoDto>? JogosCadastrados { get; set; } = new List<JogoDto>();
        public  ICollection<UsuarioJogoAdquiridoDto>? JogosAdquiridos { get; set; } = new List<UsuarioJogoAdquiridoDto>();
    }
}
