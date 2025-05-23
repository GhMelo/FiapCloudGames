using System.Security.Claims;
using Application.Input.JogoInput;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogoController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }
            

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var todosJogosDto = _jogoService.ObterTodosJogosDto();
                return Ok(todosJogosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/JogoPorId/{id:int}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult GetJogoPorId([FromRoute] int id)
        {
            try
            {
                var jogoDto = _jogoService.ObterJogoDtoPorId(id);
                return Ok(jogoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/JogoPorTitulo/{titulo}")]
        public IActionResult GetJogoPorTitulo([FromRoute] string titulo)
        {
            try
            {
                var jogoDto = _jogoService.ObterJogoDtoPorTitulo(titulo);
                return Ok(jogoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "Administrador")]
        public IActionResult Post([FromBody] JogoCadastroInput input)
        {
            try
            {
                var nomeUsuarioLogado = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _jogoService.CadastrarJogo(input, nomeUsuarioLogado);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Policy = "Administrador")]
        public IActionResult Put([FromBody] JogoAlteracaoInput input)
        {
            try
            {
                _jogoService.AlterarJogo(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _jogoService.DeletarJogo(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
