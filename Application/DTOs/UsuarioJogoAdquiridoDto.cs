namespace Application.DTOs
{
    public class UsuarioJogoAdquiridoDto
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public int UsuarioId { get; set; }
        public virtual UsuarioDto Usuario { get; set; } = null!;
        public int JogoId { get; set; }
        public virtual JogoDto Jogo { get; set; } = null!;
    }
}
