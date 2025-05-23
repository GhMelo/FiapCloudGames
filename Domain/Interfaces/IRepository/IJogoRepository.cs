using Domain.Entity;

namespace Domain.Interfaces.IRepository
{
    public interface IJogoRepository : IRepository<Jogo>
    {
        Jogo obterPorTitulo(string titulo);
    }
}
