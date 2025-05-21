namespace Application.DTOs
{
    public class JogoDto
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Titulo { get; set; } = null!;
        public string Produtora { get; set; } = null!;
        public  UsuarioDto UsuarioCadastro { get; set; } = null!;
        public  ICollection<UsuarioJogoAdquiridoDto>? UsuariosQueAdquiriram { get; set; } = new List<UsuarioJogoAdquiridoDto>();
    }
}
