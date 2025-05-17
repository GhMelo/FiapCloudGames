namespace Core.Entity 
{ 
    public class UsuarioJogoAdquirido : EntityBase
    {
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        public int JogoId { get; set; }
        public virtual Jogo Jogo { get; set; } = null!;
    }
}
