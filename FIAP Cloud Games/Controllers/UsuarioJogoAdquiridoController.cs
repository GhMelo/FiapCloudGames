using Application.Input.UsuarioJogoInput;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UsuarioJogoAdquiridoController : ControllerBase
    {
        private readonly IUsuarioJogoAdquiridoService _usuarioJogoAdquiridoService;
        public UsuarioJogoAdquiridoController(IUsuarioJogoAdquiridoService usuarioJogoAdquiridoService)
        {
            _usuarioJogoAdquiridoService = usuarioJogoAdquiridoService;
        }


        [HttpGet("/JogosAdquiridosUltimos60dias/")]
        [Authorize(Policy = "Administrador")]
        public IActionResult GetUsuarioJogosAdquiridosUltimos60DiasDapper()
        {
            try
            {
                var usuarioJogosAdquiridosUltimos60Dias = _usuarioJogoAdquiridoService.ObterUsuarioJogosAdquiridosUltimos60Dias();
                return Ok(usuarioJogosAdquiridosUltimos60Dias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "Administrador")]
        public IActionResult Post([FromBody] UsuarioJogoAdquiridoCadastroInput input)
        {
            try
            {
                _usuarioJogoAdquiridoService.CadastrarJogoAdquirido(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
