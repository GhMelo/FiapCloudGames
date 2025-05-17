using System.Collections.Generic;
using Core.DTOs;
using Core.Entity;
using Core.Input.JogoInput;
using Core.Repository;
using Microsoft.AspNetCore.Authorization;
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
                jogoDto = todosJogos.Select(tj => new JogoDto()
                {
                    Id = tj.Id,
                    Titulo = tj.Titulo,
                    Produtora = tj.Produtora,
                    DataCriacao = tj.DataCriacao,
                    UsuarioCadastro = new UsuarioDto()
                    {
                        Id = tj.UsuarioCadastro.Id,
                        DataCriacao = tj.UsuarioCadastro.DataCriacao,
                        Nome = tj.UsuarioCadastro.Nome,
                        Email = tj.UsuarioCadastro.Email,
                        Tipo = tj.UsuarioCadastro.Tipo,
                    },
                    UsuariosQueAdquiriram = tj.UsuariosQueAdquiriram.Select(u => new UsuarioJogoAdquiridoDto
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
                }).ToList();

                return Ok(jogoDto);
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
                var jogo = _jogoRepository.ObterPorId(id);
                var jogoDto = new JogoDto();
                jogoDto.Id = jogo.Id;
                jogoDto.Titulo = jogo.Titulo;
                jogoDto.Produtora = jogo.Produtora;
                jogoDto.DataCriacao = jogo.DataCriacao;
                jogoDto.UsuariosQueAdquiriram = jogo.UsuariosQueAdquiriram.Select(ua => new UsuarioJogoAdquiridoDto()
                {
                    Id = ua.Id,
                    DataCriacao = ua.DataCriacao,
                    UsuarioId = ua.UsuarioId,
                    JogoId = ua.JogoId,
                    Jogo = new JogoDto()
                    {
                        DataCriacao = ua.Jogo.DataCriacao,
                        Id = ua.Jogo.Id,
                        Produtora = ua.Jogo.Produtora,
                        Titulo = ua.Jogo.Titulo,
                        UsuarioCadastro = new UsuarioDto()
                        {
                            DataCriacao = ua.Usuario.DataCriacao,
                            Id = ua.Usuario.Id,
                            Nome = ua.Usuario.Nome,
                            Email = ua.Usuario.Email,
                            Tipo = ua.Usuario.Tipo,
                        }
                    },
                    Usuario = new UsuarioDto()
                    {
                        Id = ua.UsuarioId,
                        DataCriacao = ua.DataCriacao,
                        Nome = ua.Usuario.Nome,
                        Email = ua.Usuario.Email,
                        Tipo = ua.Usuario.Tipo,
                    }

                }).ToList();

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

        [HttpGet("/JogoPorTitulo/{titulo}")]
        public IActionResult GetJogoPorTitulo([FromRoute] string titulo)
        {
            try
            {
                var jogo = _jogoRepository.obterPorTitulo(titulo);
                var jogoDto = new JogoDto();
                jogoDto.Id = jogo.Id;
                jogoDto.Titulo = jogo.Titulo;
                jogoDto.Produtora = jogo.Produtora;
                jogoDto.DataCriacao = jogo.DataCriacao;
                jogoDto.UsuariosQueAdquiriram = jogo.UsuariosQueAdquiriram.Select(ua => new UsuarioJogoAdquiridoDto()
                {
                    Id = ua.Id,
                    DataCriacao = ua.DataCriacao,
                    UsuarioId = ua.UsuarioId,
                    JogoId = ua.JogoId,
                    Jogo = new JogoDto()
                    {
                        DataCriacao = ua.Jogo.DataCriacao,
                        Id = ua.Jogo.Id,
                        Produtora = ua.Jogo.Produtora,
                        Titulo = ua.Jogo.Titulo,
                        UsuarioCadastro = new UsuarioDto()
                        {
                            DataCriacao = ua.Usuario.DataCriacao,
                            Id = ua.Usuario.Id,
                            Nome = ua.Usuario.Nome,
                            Email = ua.Usuario.Email,
                            Tipo = ua.Usuario.Tipo,
                        }
                    },
                    Usuario = new UsuarioDto()
                    {
                        Id = ua.UsuarioId,
                        DataCriacao = ua.DataCriacao,
                        Nome = ua.Usuario.Nome,
                        Email = ua.Usuario.Email,
                        Tipo = ua.Usuario.Tipo,
                    }

                }).ToList();

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
        [Authorize(Policy = "Administrador")]
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
        [Authorize(Policy = "Administrador")]
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
        [Authorize(Policy = "Administrador")]
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
