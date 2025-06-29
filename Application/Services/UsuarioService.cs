﻿
using Application.DTOs;
using Application.Input.UsuarioInput;
using Domain.Entity;
using Domain.Interfaces.IRepository;
using Application.Interfaces.IService;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IKafkaProducerService _kafkaProducer;
        public UsuarioService(IUsuarioRepository usuarioRepository, IKafkaProducerService kafkaProducerService)
        {
            _usuarioRepository = usuarioRepository;
            _kafkaProducer = kafkaProducerService;
        }

        public void AlterarUsuario(UsuarioAlteracaoInput usuarioAlteracaoInput)
        {
            var usuario = _usuarioRepository.ObterPorId(usuarioAlteracaoInput.Id);
            usuario.Nome = usuarioAlteracaoInput.Nome;
            usuario.Tipo = usuarioAlteracaoInput.Tipo;
            usuario.Email = usuarioAlteracaoInput.Email;
            usuario.Senha = usuarioAlteracaoInput.Senha;
            _usuarioRepository.Alterar(usuario);
        }

        public void CadastrarUsuarioAdministrador(UsuarioCadastroInput UsuarioCadastroInput)
        {
            var Usuario = new Usuario()
            {
                Nome = UsuarioCadastroInput.Nome,
                Email = UsuarioCadastroInput.Email,
                Tipo = TipoUsuario.Administrador,
                Senha = UsuarioCadastroInput.Senha
            };
            _usuarioRepository.Cadastrar(Usuario);
        }

        public async void CadastrarUsuarioPadrao(UsuarioCadastroInput UsuarioCadastroInput)
        {
            var Usuario = new Usuario()
            {
                Nome = UsuarioCadastroInput.Nome,
                Email = UsuarioCadastroInput.Email,
                Tipo = TipoUsuario.Padrao,
                Senha = UsuarioCadastroInput.Senha
            };
            _usuarioRepository.Cadastrar(Usuario);

            await _kafkaProducer.SendEmailMessageAsync(new EmailMessageDto
            {
                Nome = UsuarioCadastroInput.Nome,
                Email = UsuarioCadastroInput.Email,
                Mensagem = $"Olá {UsuarioCadastroInput.Nome}, seu cadastro foi realizado com sucesso!"
            });
        }

        public void DeletarUsuario(int id)
        {
            _usuarioRepository.Deletar(id);
        }

        public IEnumerable<UsuarioDto> ObterTodosUsuariosDto()
        {
            var todosUsuarios = _usuarioRepository.ObterTodos();
            var usuariosDto = new List<UsuarioDto>();
            usuariosDto = todosUsuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                DataCriacao = u.DataCriacao,
                Tipo = (TipoUsuarioDto)u.Tipo,
                Nome = u.Nome,
                Email = u.Email,
                Senha = u.Senha,
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
                            Senha = uj.Usuario.Senha,
                            Tipo = (TipoUsuarioDto)uj.Usuario.Tipo,
                        },
                        PromocoesAderidas = uj.Jogo.PromocoesAderidas.Select(p => new PromocaoDto
                        {
                            Id = p.Id,
                            NomePromocao = p.NomePromocao,
                            Porcentagem = p.Porcentagem,
                            PromocaoAtiva = p.PromocaoAtiva,
                            DataCriacao = p.DataCriacao,
                        }).ToList()
                    }
                }).ToList()
            }
            ).ToList();

            return usuariosDto;
        }

        public UsuarioDto ObterUsuarioDtoPorId(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);
            var usuarioDto = new UsuarioDto();

            usuarioDto.Id = usuario.Id;
            usuarioDto.DataCriacao = usuario.DataCriacao;
            usuarioDto.Tipo = (TipoUsuarioDto)usuario.Tipo;
            usuarioDto.Nome = usuario.Nome;
            usuarioDto.Email = usuario.Email;
            usuarioDto.Senha = usuario.Senha;
            usuarioDto.JogosCadastrados = usuario.JogosCadastrados.Select(j => new JogoDto
            {
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
                        Senha = uj.Usuario.Senha,
                        Tipo = (TipoUsuarioDto)uj.Usuario.Tipo,
                    },
                    PromocoesAderidas = uj.Jogo.PromocoesAderidas.Select(p => new PromocaoDto
                    {
                        Id = p.Id,
                        NomePromocao = p.NomePromocao,
                        Porcentagem = p.Porcentagem,
                        PromocaoAtiva = p.PromocaoAtiva,
                        DataCriacao = p.DataCriacao,
                    }).ToList()
                }
            }).ToList();

            return usuarioDto;
        }

        public UsuarioDto ObterUsuarioDtoPorNome(string nome)
        {
            var usuario = _usuarioRepository.obterPorNome(nome);
            var usuarioDto = new UsuarioDto();

            usuarioDto.Id = usuario.Id;
            usuarioDto.DataCriacao = usuario.DataCriacao;
            usuarioDto.Tipo = (TipoUsuarioDto)usuario.Tipo;
            usuarioDto.Nome = usuario.Nome;
            usuarioDto.Email = usuario.Email;
            usuarioDto.Senha = usuario.Senha;
            usuarioDto.JogosCadastrados = usuario.JogosCadastrados.Select(j => new JogoDto
            {
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
                        Senha = uj.Usuario.Senha,
                        Tipo = (TipoUsuarioDto)uj.Usuario.Tipo,
                    },
                    PromocoesAderidas = uj.Jogo.PromocoesAderidas.Select(p => new PromocaoDto
                    {
                        Id = p.Id,
                        NomePromocao = p.NomePromocao,
                        Porcentagem = p.Porcentagem,
                        PromocaoAtiva = p.PromocaoAtiva,
                        DataCriacao = p.DataCriacao,
                    }).ToList()
                }
            }).ToList();

            return usuarioDto;
        }
    }
}
