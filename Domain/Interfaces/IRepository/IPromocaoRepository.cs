using Domain.Entity;

namespace Domain.Interfaces.IRepository
{
    public interface IPromocaoRepository : IRepository<Promocao>
    {
        Promocao obterPorNomePromocao(string nomePromocao);
    }
}
