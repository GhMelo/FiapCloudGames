using Core.DTOs;
using Core.Entity;

namespace Core.Repository
{
    public interface IUsuarioJogoAdquiridoRepository : IRepository<UsuarioJogoAdquirido>
    {
        IEnumerable<UsuarioJogoAdquiridoDto> ObterUsuarioJogosAdquiridosUltimos60DiasDapper();
    }
}
