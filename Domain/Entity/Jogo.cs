namespace Domain.Entity
{
    public class Jogo : EntityBase
    {
        public string Titulo { get; set; } = null!;
        public string Produtora { get; set; } = null!;
        public int UsuarioCadastroId { get; set; }
        public virtual Usuario UsuarioCadastro { get; set; } = null!;
        public virtual ICollection<UsuarioJogoAdquirido> UsuariosQueAdquiriram { get; set; } = new List<UsuarioJogoAdquirido>();
        public virtual ICollection<Promocao> PromocoesAderidas { get; set; } = new List<Promocao>();
    }
}
