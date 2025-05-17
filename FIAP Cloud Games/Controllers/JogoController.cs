using System.Collections.Generic;
using Core.DTOs;
using Core.Entity;
using Core.Input.JogoInput;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_Cloud_Games.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoController(IJogoRepository jogoRepository) 
            => _jogoRepository = jogoRepository;

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var todosJogos = _jogoRepository.ObterTodos();
                var jogoDto = new List<JogoDto>();
                foreach (var jogo in todosJogos)
                {
                    jogoDto.Add(new JogoDto()
                    {
                        Id = jogo.Id,
                        Titulo = jogo.Titulo,
                        Produtora = jogo.Produtora,
                        DataCriacao = jogo.DataCriacao,
                        UsuarioCadastro = new UsuarioDto()
                        {
                            Id = jogo.UsuarioCadastro.Id,
                            DataCriacao = jogo.UsuarioCadastro.DataCriacao,
                            Nome = jogo.UsuarioCadastro.Nome,
                            Email = jogo.UsuarioCadastro.Email,
                            Tipo = jogo.UsuarioCadastro.Tipo,
                        },
                        UsuariosQueAdquiriram = jogo.UsuariosQueAdquiriram.Select(u => new UsuarioJogoAdquiridoDto
                        {
                            Id = u.Id,
                            DataCriacao = u.DataCriacao,
                            UsuarioId = u.UsuarioId,
                            JogoId = u.JogoId,
                            Usuario = new UsuarioDto()
                            {
                                Id = u.Usuario.Id,
                                DataCriacao = u.Usuario.DataCriacao,
                                Nome = u.Usuario.Nome,
                                Email = u.Usuario.Email,
                                Tipo = u.Usuario.Tipo
                            }
                        }).ToList(),
                    });
                }

                return Ok(jogoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var jogo = _jogoRepository.ObterPorId(id);
                var jogoDto = new JogoDto();
                jogoDto.Id = jogo.Id;
                jogoDto.Titulo = jogo.Titulo;
                jogoDto.Produtora = jogo.Produtora;
                jogoDto.DataCriacao = jogo.DataCriacao;
                foreach(var usuariosQueAdquiriram in jogo.UsuariosQueAdquiriram)
                {
                    jogoDto.UsuariosQueAdquiriram.Add(new UsuarioJogoAdquiridoDto()
                    {
                        Id = usuariosQueAdquiriram.Id,
                        DataCriacao = usuariosQueAdquiriram.DataCriacao,
                        UsuarioId = usuariosQueAdquiriram.UsuarioId,
                        JogoId = usuariosQueAdquiriram.JogoId,
                        Jogo = new JogoDto()
                        {
                            DataCriacao = usuariosQueAdquiriram.Jogo.DataCriacao,
                            Id = usuariosQueAdquiriram.Jogo.Id,
                            Produtora = usuariosQueAdquiriram.Jogo.Produtora,
                            Titulo = usuariosQueAdquiriram.Jogo.Titulo,
                            UsuarioCadastro = new UsuarioDto()
                            {
                                DataCriacao = usuariosQueAdquiriram.Usuario.DataCriacao,
                                Id = usuariosQueAdquiriram.Usuario.Id,
                                Nome = usuariosQueAdquiriram.Usuario.Nome,
                                Email = usuariosQueAdquiriram.Usuario.Email,
                                Tipo = usuariosQueAdquiriram.Usuario.Tipo,
                            }
                        },
                        Usuario = new UsuarioDto()
                        {
                            Id = usuariosQueAdquiriram.UsuarioId,
                            DataCriacao = usuariosQueAdquiriram.DataCriacao,
                            Nome = usuariosQueAdquiriram.Usuario.Nome,
                            Email = usuariosQueAdquiriram.Usuario.Email,
                            Tipo = usuariosQueAdquiriram.Usuario.Tipo,
                        }
                    });
                }
                jogoDto.UsuarioCadastro = new UsuarioDto()
                {
                    Id = jogo.UsuarioCadastro.Id,
                    DataCriacao = jogo.UsuarioCadastro.DataCriacao,
                    Nome = jogo.UsuarioCadastro.Nome,
                    Email = jogo.UsuarioCadastro.Email,
                    Tipo = jogo.UsuarioCadastro.Tipo,
                };

                return Ok(jogoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] JogoCadastroInput input)
        {
            try
            {
                var jogo = new Jogo()
                {
                    Titulo = input.Titulo,
                    Produtora = input.Produtora,
                    UsuarioCadastroId = input.UsuarioCadastroId
                };
                _jogoRepository.Cadastrar(jogo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody] JogoAlteracaoInput input)
        {
            try
            {
                var jogo = _jogoRepository.ObterPorId(input.Id);
                jogo.Titulo = input.Titulo;
                jogo.Produtora = input.Produtora;
                _jogoRepository.Alterar(jogo);
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
                _jogoRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
