using Application.Input.UsuarioJogoInput;
using Application.Interfaces.IService;
using Domain.Entity;
using Domain.Interfaces.IRepository;

namespace Application.Services
{
    public class UsuarioJogoAdquiridoService : IUsuarioJogoAdquiridoService
    {
        private readonly IUsuarioJogoAdquiridoRepository _usuarioJogoAdquiridoRepository;
        private readonly IJogoRepository _jogoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioJogoAdquiridoService(IUsuarioJogoAdquiridoRepository usuarioJogoAdquiridoRepository,
            IJogoRepository jogoRepository,
            IUsuarioRepository usuarioRepository)
        {
            _usuarioJogoAdquiridoRepository = usuarioJogoAdquiridoRepository;
            _jogoRepository = jogoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public void CadastrarJogoAdquirido(UsuarioJogoAdquiridoCadastroInput usuarioJogoAdquiridoCadastroInput)
        {
            var usuarioJogoAdquirido = new UsuarioJogoAdquirido()
            {
                UsuarioId = _jogoRepository.obterPorTitulo(usuarioJogoAdquiridoCadastroInput.TituloJogo).Id,
                JogoId = _usuarioRepository.obterPorNome(usuarioJogoAdquiridoCadastroInput.NomeUsuario).Id
            };
            _usuarioJogoAdquiridoRepository.Cadastrar(usuarioJogoAdquirido);
        }

        public IEnumerable<UsuarioJogoAdquirido> ObterUsuarioJogosAdquiridosUltimos60Dias()
        {
            return _usuarioJogoAdquiridoRepository.ObterUsuarioJogosAdquiridosUltimos60DiasDapper();
        }
    }
}
