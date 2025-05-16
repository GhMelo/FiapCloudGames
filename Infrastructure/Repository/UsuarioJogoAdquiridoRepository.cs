using Core.Entity;
using Core.Repository;

namespace Infrastructure.Repository
{
    public class UsuarioJogoAdquiridoRepository : EFRepository<UsuarioJogoAdquirido>, IUsuarioJogoAdquiridoRepository
    {
        public UsuarioJogoAdquiridoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
