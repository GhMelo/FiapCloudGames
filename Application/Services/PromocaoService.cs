using Application.DTOs;
using Application.Input.PromocaoInput;
using Application.Interfaces.IService;
using Domain.Entity;
using Domain.Interfaces.IRepository;
using Infrastructure.Repository;

namespace Application.Services
{
    internal class PromocaoService : IPromocaoService
    {
        private readonly IPromocaoRepository _promocaoRepository;
        public PromocaoService(IPromocaoRepository promocaoRepository)
            => _promocaoRepository = promocaoRepository;

        public void AlterarPromocao(PromocaoAlteracaoInput promocaoAlteracaoInput)
        {
            var promocaoAlteracao = _promocaoRepository.ObterPorId(promocaoAlteracaoInput.PromocaoId);
            promocaoAlteracao.NomePromocao = promocaoAlteracaoInput.NomePromocao;
            promocaoAlteracao.Porcentagem = promocaoAlteracaoInput.Porcentagem;
            promocaoAlteracao.PromocaoAtiva = promocaoAlteracaoInput.PromocaoAtiva;
            _promocaoRepository.Alterar(promocaoAlteracao);
        }

        public void CadastrarPromocao(PromocaoCadastroInput promocaoCadastroInput)
        {
            var promocaoCadastro = new Promocao()
            {
                NomePromocao = promocaoCadastroInput.NomePromocao,
                JogoId = promocaoCadastroInput.JogoId,
                Porcentagem = promocaoCadastroInput.Porcentagem,
                PromocaoAtiva = promocaoCadastroInput.PromocaoAtiva
            };
            _promocaoRepository.Cadastrar(promocaoCadastro);
        }

        public void DeletarPromocao(int id)
        {
            _promocaoRepository.Deletar(id);
        }

        public PromocaoDto ObterPromocaoDtoPorId(int id)
        {
            var promocaoBd = _promocaoRepository.ObterPorId(id);
            var promocaoDto = new PromocaoDto
            {
                Id = promocaoBd.Id,
                NomePromocao = promocaoBd.NomePromocao,
                JogoId = promocaoBd.JogoId,
                Porcentagem = promocaoBd.Porcentagem,
                PromocaoAtiva = promocaoBd.PromocaoAtiva,
                JogoPromocao = new JogoDto
                {
                    Id = promocaoBd.JogoPromocao.Id,
                    Titulo = promocaoBd.JogoPromocao.Titulo,
                    Produtora = promocaoBd.JogoPromocao.Produtora,
                    DataCriacao = promocaoBd.JogoPromocao.DataCriacao,
                    UsuarioCadastro = new UsuarioDto
                    {
                        Nome = promocaoBd.JogoPromocao.UsuarioCadastro.Nome,
                        Email = promocaoBd.JogoPromocao.UsuarioCadastro.Email,
                        Tipo = (TipoUsuarioDto)promocaoBd.JogoPromocao.UsuarioCadastro.Tipo,
                        DataCriacao = promocaoBd.JogoPromocao.UsuarioCadastro.DataCriacao,
                        Id = promocaoBd.JogoPromocao.UsuarioCadastro.Id
                    }
                }
            };
            return promocaoDto;
        }

        public PromocaoDto ObterPromocaoDtoPorNomePromocao(string nomePromocao)
        {
            var promocaoBd = _promocaoRepository.obterPorNomePromocao(nomePromocao);
            var promocaoDto = new PromocaoDto
            {
                Id = promocaoBd.Id,
                NomePromocao = promocaoBd.NomePromocao,
                JogoId = promocaoBd.JogoId,
                Porcentagem = promocaoBd.Porcentagem,
                PromocaoAtiva = promocaoBd.PromocaoAtiva,
                JogoPromocao = new JogoDto
                {
                    Id = promocaoBd.JogoPromocao.Id,
                    Titulo = promocaoBd.JogoPromocao.Titulo,
                    Produtora = promocaoBd.JogoPromocao.Produtora,
                    DataCriacao = promocaoBd.JogoPromocao.DataCriacao,
                    UsuarioCadastro = new UsuarioDto
                    {
                        Nome = promocaoBd.JogoPromocao.UsuarioCadastro.Nome,
                        Email = promocaoBd.JogoPromocao.UsuarioCadastro.Email,
                        Tipo = (TipoUsuarioDto)promocaoBd.JogoPromocao.UsuarioCadastro.Tipo,
                        DataCriacao = promocaoBd.JogoPromocao.UsuarioCadastro.DataCriacao,
                        Id = promocaoBd.JogoPromocao.UsuarioCadastro.Id
                    }
                }
            };
            return promocaoDto;
        }

        public IEnumerable<PromocaoDto> ObterTodosPromocaoDto()
        {
            var promocaoBd = _promocaoRepository.ObterTodos();
            var promocaoDto = new List<PromocaoDto>();
            promocaoDto = promocaoBd.Select(p => new PromocaoDto
            {
                Id = p.Id,
                NomePromocao = p.NomePromocao,
                JogoId = p.JogoId,
                Porcentagem = p.Porcentagem,
                PromocaoAtiva = p.PromocaoAtiva,
                JogoPromocao = new JogoDto
                {
                    Id = p.JogoPromocao.Id,
                    Titulo = p.JogoPromocao.Titulo,
                    Produtora = p.JogoPromocao.Produtora,
                    DataCriacao = p.JogoPromocao.DataCriacao,
                    UsuarioCadastro = new UsuarioDto
                    {
                        Nome = p.JogoPromocao.UsuarioCadastro.Nome,
                        Email = p.JogoPromocao.UsuarioCadastro.Email,
                        Tipo = (TipoUsuarioDto)p.JogoPromocao.UsuarioCadastro.Tipo,
                        DataCriacao = p.JogoPromocao.UsuarioCadastro.DataCriacao,
                        Id = p.JogoPromocao.UsuarioCadastro.Id
                    }
                }
            }).ToList();
            return promocaoDto;
        }

        public IEnumerable<PromocaoDto> ObterTodosPromocaoDtoAtivas()
        {
            var promocaoBd = _promocaoRepository.ObterTodos().Where(x=>x.PromocaoAtiva == true);
            var promocaoDto = new List<PromocaoDto>();
            promocaoDto = promocaoBd.Select(p => new PromocaoDto
            {
                Id = p.Id,
                NomePromocao = p.NomePromocao,
                JogoId = p.JogoId,
                Porcentagem = p.Porcentagem,
                PromocaoAtiva = p.PromocaoAtiva,
                JogoPromocao = new JogoDto
                {
                    Id = p.JogoPromocao.Id,
                    Titulo = p.JogoPromocao.Titulo,
                    Produtora = p.JogoPromocao.Produtora,
                    DataCriacao = p.JogoPromocao.DataCriacao,
                    UsuarioCadastro = new UsuarioDto
                    {
                        Nome = p.JogoPromocao.UsuarioCadastro.Nome,
                        Email = p.JogoPromocao.UsuarioCadastro.Email,
                        Tipo = (TipoUsuarioDto)p.JogoPromocao.UsuarioCadastro.Tipo,
                        DataCriacao = p.JogoPromocao.UsuarioCadastro.DataCriacao,
                        Id = p.JogoPromocao.UsuarioCadastro.Id
                    }
                }
            }).ToList();
            return promocaoDto;
        }

        public IEnumerable<PromocaoDto> ObterTodosPromocaoDtoInativas()
        {
            var promocaoBd = _promocaoRepository.ObterTodos().Where(x => x.PromocaoAtiva == false);
            var promocaoDto = new List<PromocaoDto>();
            promocaoDto = promocaoBd.Select(p => new PromocaoDto
            {
                Id = p.Id,
                NomePromocao = p.NomePromocao,
                JogoId = p.JogoId,
                Porcentagem = p.Porcentagem,
                PromocaoAtiva = p.PromocaoAtiva,
                JogoPromocao = new JogoDto
                {
                    Id = p.JogoPromocao.Id,
                    Titulo = p.JogoPromocao.Titulo,
                    Produtora = p.JogoPromocao.Produtora,
                    DataCriacao = p.JogoPromocao.DataCriacao,
                    UsuarioCadastro = new UsuarioDto
                    {
                        Nome = p.JogoPromocao.UsuarioCadastro.Nome,
                        Email = p.JogoPromocao.UsuarioCadastro.Email,
                        Tipo = (TipoUsuarioDto)p.JogoPromocao.UsuarioCadastro.Tipo,
                        DataCriacao = p.JogoPromocao.UsuarioCadastro.DataCriacao,
                        Id = p.JogoPromocao.UsuarioCadastro.Id
                    }
                }
            }).ToList();
            return promocaoDto;
        }
    }
}
