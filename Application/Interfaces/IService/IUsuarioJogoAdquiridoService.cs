using Application.Input.UsuarioJogoInput;
using Domain.Entity;

namespace Application.Interfaces.IService
{
    public interface IUsuarioJogoAdquiridoService
    {
        IEnumerable<UsuarioJogoAdquirido> ObterUsuarioJogosAdquiridosUltimos60Dias();
        void CadastrarJogoAdquirido(UsuarioJogoAdquiridoCadastroInput usuarioJogoAdquiridoCadastroInput);
    }
}
