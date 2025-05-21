namespace Domain.Entity
{
    public enum TipoUsuario
    {
        Padrao,
        Administrador
    }

    public class Usuario : EntityBase
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public TipoUsuario Tipo { get; set; }
        public virtual ICollection<Jogo> JogosCadastrados { get; set; } = new List<Jogo>();
        public virtual ICollection<UsuarioJogoAdquirido> JogosAdquiridos { get; set; } = new List<UsuarioJogoAdquirido>();
    }
}
