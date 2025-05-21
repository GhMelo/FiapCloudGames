using Domain.Entity;

namespace Domain.IRepository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario obterPorNome(string nome);
    }
}
