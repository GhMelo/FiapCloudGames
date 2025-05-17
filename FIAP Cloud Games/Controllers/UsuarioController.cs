using Core.DTOs;
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
                var todosUsuarios = _usuarioRepository.ObterTodos();
                var usuariosDto = new List<UsuarioDto>();
                usuariosDto = todosUsuarios.Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    DataCriacao = u.DataCriacao,
                    Tipo = u.Tipo,
                    Nome = u.Nome,
                    Email = u.Email,
                    JogosCadastrados = u.JogosCadastrados.Select(j => new JogoDto
                    {
                        Id = j.Id,
                        DataCriacao = j.DataCriacao,
                        Titulo = j.Titulo,
                        Produtora = j.Produtora
                    }).ToList(),

                    JogosAdquiridos = u.JogosAdquiridos.Select(uj => new UsuarioJogoAdquiridoDto()
                    {
                        Id = uj.Id,
                        DataCriacao = uj.DataCriacao,
                        UsuarioId = uj.UsuarioId,
                        JogoId = uj.JogoId,
                        Jogo = new JogoDto()
                        {
                            DataCriacao = uj.Jogo.DataCriacao,
                            Id = uj.Jogo.Id,
                            Produtora = uj.Jogo.Produtora,
                            Titulo = uj.Jogo.Titulo,
                            UsuarioCadastro = new UsuarioDto()
                            {
                                DataCriacao = uj.Usuario.DataCriacao,
                                Id = uj.Usuario.Id,
                                Nome = uj.Usuario.Nome,
                                Email = uj.Usuario.Email,
                                Tipo = uj.Usuario.Tipo,
                            }
                        }
                    }).ToList()
                }
                ).ToList();

                return Ok(usuariosDto);
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
                var usuarioDto = new UsuarioDto();

                usuarioDto.Id = usuario.Id;
                usuarioDto.DataCriacao = usuario.DataCriacao;
                usuarioDto.Tipo = usuario.Tipo;
                usuarioDto.Nome = usuario.Nome;
                usuarioDto.Email = usuario.Email;
                usuarioDto.JogosCadastrados = usuario.JogosCadastrados.Select(j => new JogoDto{
                        Id = j.Id,
                        DataCriacao = j.DataCriacao,
                        Titulo = j.Titulo,
                        Produtora = j.Produtora
                    }).ToList();

                usuarioDto.JogosAdquiridos = usuario.JogosAdquiridos.Select(uj => new UsuarioJogoAdquiridoDto()
                {
                    Id = uj.Id,
                    DataCriacao = uj.DataCriacao,
                    UsuarioId = uj.UsuarioId,
                    JogoId = uj.JogoId,
                    Jogo = new JogoDto()
                    {
                        DataCriacao = uj.Jogo.DataCriacao,
                        Id = uj.Jogo.Id,
                        Produtora = uj.Jogo.Produtora,
                        Titulo = uj.Jogo.Titulo,
                        UsuarioCadastro = new UsuarioDto()
                        {
                            DataCriacao = uj.Usuario.DataCriacao,
                            Id = uj.Usuario.Id,
                            Nome = uj.Usuario.Nome,
                            Email = uj.Usuario.Email,
                            Tipo = uj.Usuario.Tipo,
                        }
                    }
                }).ToList();

                return Ok(usuarioDto);
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
