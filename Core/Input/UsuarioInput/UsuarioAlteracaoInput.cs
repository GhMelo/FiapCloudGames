using Core.Entity;

namespace Core.Input.UsuarioInput
{
    public class UsuarioAlteracaoInput
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required TipoUsuario Tipo { get; set; }
    }
}
