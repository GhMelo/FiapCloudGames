namespace Core.Entity
{
    public class Jogo : EntityBase
    {
        public string Titulo { get; set; } = null!;
        public string Produtora { get; set; } = null!;
        public int UsuarioCadastroId { get; set; }
        public Usuario UsuarioCadastro { get; set; } = null!;
        public ICollection<UsuarioJogoAdquirido> UsuariosQueAdquiriram { get; set; } = new List<UsuarioJogoAdquirido>();
    }
}
