using Domain.Entity;

namespace Domain.IRepository
{
    public interface IUsuarioJogoAdquiridoRepository : IRepository<UsuarioJogoAdquirido>
    {
        IEnumerable<UsuarioJogoAdquirido> ObterUsuarioJogosAdquiridosUltimos60DiasDapper();
    }
}
