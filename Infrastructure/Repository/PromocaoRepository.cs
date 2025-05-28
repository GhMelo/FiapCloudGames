using Domain.Entity;
using Domain.Interfaces.IRepository;

namespace Infrastructure.Repository
{
    public class PromocaoRepository : EFRepository<Promocao>, IPromocaoRepository
    {
        public PromocaoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Promocao obterPorNomePromocao(string nomePromocao)
            => _dbSet.FirstOrDefault(entity => entity.NomePromocao == nomePromocao);
    }
}
