using Core.Entity;

namespace Core.Repository
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Jogo obterPorTitulo(string titulo);
    }
}
