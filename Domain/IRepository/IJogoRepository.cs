using Domain.Entity;

namespace Domain.IRepository
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Jogo obterPorTitulo(string titulo);
    }
}
