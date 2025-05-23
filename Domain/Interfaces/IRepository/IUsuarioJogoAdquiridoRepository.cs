using Domain.Entity;

namespace Domain.Interfaces.IRepository
{
    public interface IUsuarioJogoAdquiridoRepository : IRepository<UsuarioJogoAdquirido>
    {
        IEnumerable<UsuarioJogoAdquirido> ObterUsuarioJogosAdquiridosUltimos60DiasDapper();
    }
}
