using Core.DTOs;
using Core.Entity;
using Core.Input.UsuarioInput;
using Core.Input.UsuarioJogoInput;
using Core.Repository;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UsuarioJogoAdquiridoController : ControllerBase
    {
        private readonly IUsuarioJogoAdquiridoRepository _usuarioJogoAdquiridoRepository;
        private readonly IJogoRepository _jogoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioJogoAdquiridoController(IUsuarioJogoAdquiridoRepository usuarioJogoAdquiridoRepository,
            IJogoRepository jogoRepository,
            IUsuarioRepository usuarioRepository)
        {
            _usuarioJogoAdquiridoRepository = usuarioJogoAdquiridoRepository;
            _jogoRepository = jogoRepository;
            _usuarioRepository = usuarioRepository;
        }


        [HttpGet("/JogosAdquiridosUltimos60dias/")]
        public IActionResult GetUsuarioJogosAdquiridosUltimos60DiasDapper()
        {
            try
            {
                return Ok(_usuarioJogoAdquiridoRepository.ObterUsuarioJogosAdquiridosUltimos60DiasDapper());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UsuarioJogoAdquiridoCadastroInput input)
        {
            try
            {
                var usuarioJogoAdquirido = new UsuarioJogoAdquirido()
                {
                    UsuarioId = _jogoRepository.obterPorTitulo(input.TituloJogo).Id,
                    JogoId = _usuarioRepository.obterPorNome(input.NomeUsuario).Id
                };
                _usuarioJogoAdquiridoRepository.Cadastrar(usuarioJogoAdquirido);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
