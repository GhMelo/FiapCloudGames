using Core.Entity;

namespace Core.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario obterPorNome(string nome);
    }
}
