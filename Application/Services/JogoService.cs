using Application.DTOs;
using Application.Input.JogoInput;
using Application.Interfaces.IService;
using Domain.Entity;
using Domain.Interfaces.IRepository;

namespace Application.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public JogoService(IJogoRepository jogoRepository, IUsuarioRepository usuarioRepository)
        {
            _jogoRepository = jogoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public void AlterarJogo(JogoAlteracaoInput jogoAlteracaoInput)
        {
            var jogo = _jogoRepository.ObterPorId(jogoAlteracaoInput.Id);
            jogo.Titulo = jogoAlteracaoInput.Titulo;
            jogo.Produtora = jogoAlteracaoInput.Produtora;
            _jogoRepository.Alterar(jogo);
        }

        public void CadastrarJogo(JogoCadastroInput jogoCadastroInput, string nomeUsuarioLogado)
        {
            var usuarioId = _usuarioRepository.obterPorNome(nomeUsuarioLogado).Id;
            var jogo = new Jogo()
            {
                Titulo = jogoCadastroInput.Titulo,
                Produtora = jogoCadastroInput.Produtora,
                UsuarioCadastroId = usuarioId
            };
            _jogoRepository.Cadastrar(jogo);
        }

        public void DeletarJogo(int id)
        {
            _jogoRepository.Deletar(id);
        }

        public JogoDto ObterJogoDtoPorId(int id)
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
                        Tipo = (TipoUsuarioDto)ua.Usuario.Tipo,
                    }
                },
                Usuario = new UsuarioDto()
                {
                    Id = ua.UsuarioId,
                    DataCriacao = ua.DataCriacao,
                    Nome = ua.Usuario.Nome,
                    Email = ua.Usuario.Email,
                    Tipo = (TipoUsuarioDto)ua.Usuario.Tipo,
                }

            }).ToList();

            jogoDto.UsuarioCadastro = new UsuarioDto()
            {
                Id = jogo.UsuarioCadastro.Id,
                DataCriacao = jogo.UsuarioCadastro.DataCriacao,
                Nome = jogo.UsuarioCadastro.Nome,
                Email = jogo.UsuarioCadastro.Email,
                Tipo = (TipoUsuarioDto)jogo.UsuarioCadastro.Tipo,
            };

            jogoDto.PromocoesAderidas = jogo.PromocoesAderidas.Select(pa => new PromocaoDto()
            {
                Id = pa.Id,
                DataCriacao = pa.DataCriacao,
                JogoId = pa.JogoId,
                NomePromocao = pa.NomePromocao,
                Porcentagem = pa.Porcentagem,
                PromocaoAtiva = pa.PromocaoAtiva
            }).ToList();

            return jogoDto;
        }

        public JogoDto ObterJogoDtoPorTitulo(string titulo)
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
                        Tipo = (TipoUsuarioDto)ua.Usuario.Tipo,
                    }
                },
                Usuario = new UsuarioDto()
                {
                    Id = ua.UsuarioId,
                    DataCriacao = ua.DataCriacao,
                    Nome = ua.Usuario.Nome,
                    Email = ua.Usuario.Email,
                    Tipo = (TipoUsuarioDto)ua.Usuario.Tipo,
                }

            }).ToList();

            jogoDto.UsuarioCadastro = new UsuarioDto()
            {
                Id = jogo.UsuarioCadastro.Id,
                DataCriacao = jogo.UsuarioCadastro.DataCriacao,
                Nome = jogo.UsuarioCadastro.Nome,
                Email = jogo.UsuarioCadastro.Email,
                Tipo = (TipoUsuarioDto)jogo.UsuarioCadastro.Tipo,
            };

            jogoDto.PromocoesAderidas = jogo.PromocoesAderidas.Select(pa => new PromocaoDto()
            {
                Id = pa.Id,
                DataCriacao = pa.DataCriacao,
                JogoId = pa.JogoId,
                NomePromocao = pa.NomePromocao,
                Porcentagem = pa.Porcentagem,
                PromocaoAtiva = pa.PromocaoAtiva
            }).ToList();

            return jogoDto;
        }

        public IEnumerable<JogoDto> ObterTodosJogosDto()
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
                    Tipo = (TipoUsuarioDto)tj.UsuarioCadastro.Tipo,
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
                        Tipo = (TipoUsuarioDto)u.Usuario.Tipo
                    }
                }).ToList(),
                PromocoesAderidas = tj.PromocoesAderidas.Select(pa => new PromocaoDto()
                {
                    Id = pa.Id,
                    DataCriacao = pa.DataCriacao,
                    JogoId = pa.JogoId,
                    NomePromocao = pa.NomePromocao,
                    Porcentagem = pa.Porcentagem,
                    PromocaoAtiva = pa.PromocaoAtiva
                }).ToList()
            }).ToList();

            return jogoDto;
        }
    }
}
