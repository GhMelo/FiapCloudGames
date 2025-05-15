namespace Core.Entity
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
        public TipoUsuario Tipo { get; set; }
        public ICollection<Jogo> JogosCadastrados { get; set; } = new List<Jogo>();
        public ICollection<UsuarioJogoAdquirido> JogosAdquiridos { get; set; } = new List<UsuarioJogoAdquirido>();

    }
}
