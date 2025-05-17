using Core.Entity;
using Core.Repository;

namespace Infrastructure.Repository
{
    public class JogoRepository : EFRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Jogo obterPorTitulo(string titulo)
            => _dbSet.FirstOrDefault(entity => entity.Titulo == titulo);
    }
}
