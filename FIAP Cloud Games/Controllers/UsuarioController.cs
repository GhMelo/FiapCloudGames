using Core.Entity;
using Core.Input.UsuarioInput;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
            => _usuarioRepository = usuarioRepository;
        

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usuarios = _usuarioRepository.ObterTodos();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute]int id)
        {
            try
            {
                var usuario = _usuarioRepository.ObterPorId(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UsuarioCadastroInput input)
        {
            try
            {
                var Usuario = new Usuario()
                {
                    Nome = input.Nome,
                    Email = input.Email,
                    Tipo = input.Tipo
                };
                _usuarioRepository.Cadastrar(Usuario);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] UsuarioAlteracaoInput input)
        {
            try
            {
                var usuario = _usuarioRepository.ObterPorId(input.Id);
                usuario.Nome = input.Nome;
                usuario.Tipo = input.Tipo;
                usuario.Email = input.Email;
                _usuarioRepository.Alterar(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
