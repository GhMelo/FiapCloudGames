using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Input.JogoInput;
using Application.Interfaces.IService;
using Application.Services;
using Bogus;
using Domain.Entity;
using Domain.Interfaces.IRepository;
using Moq;

namespace FIAP_Cloud_Games.Tests.Application.Services
{
    [TestFixture]
    internal class JogoServiceTests
    {
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private Mock<IJogoRepository> _jogoRepositoryMock;
        private JogoService _jogoService;
        private Faker _faker;

        [SetUp]
        public void SetUp()
        {
            _faker = new Faker();
            _jogoRepositoryMock = new Mock<IJogoRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _jogoService = new JogoService(_jogoRepositoryMock.Object, _usuarioRepositoryMock.Object);
        }
        [Test]
        public void CadastrarJogo_DeveChamarCadastrarNoRepositorio_ComDadosCorretos()
        {
            var nomeUsuario = _faker.Internet.UserName();
            var usuario = new Usuario { Id = _faker.Random.Int(1, 100) };

            var input = new JogoCadastroInput
            {
                Titulo = _faker.Lorem.Word(),
                Produtora = _faker.Company.CompanyName()
            };

            _usuarioRepositoryMock.Setup(r => r.obterPorNome(nomeUsuario)).Returns(usuario);
            Jogo jogoCadastrado = null;
            _jogoRepositoryMock.Setup(r => r.Cadastrar(It.IsAny<Jogo>())).Callback<Jogo>(j => jogoCadastrado = j);

            _jogoService.CadastrarJogo(input, nomeUsuario);

            _jogoRepositoryMock.Verify(r => r.Cadastrar(It.IsAny<Jogo>()), Times.Once);
            Assert.That(input.Titulo, Is.EqualTo(jogoCadastrado.Titulo));
            Assert.That(input.Produtora, Is.EqualTo(jogoCadastrado.Produtora));
            Assert.That(usuario.Id, Is.EqualTo(jogoCadastrado.UsuarioCadastroId));
        }
        [Test]
        public void AlterarJogo_DeveAlterarTituloEProdutora()
        {
            var jogoExistente = new Jogo { Id = 1, Titulo = "Velho", Produtora = "Antiga" };
            var input = new JogoAlteracaoInput { Id = 1, Titulo = "Novo", Produtora = "Nova" };

            _jogoRepositoryMock.Setup(r => r.ObterPorId(1)).Returns(jogoExistente);

            _jogoService.AlterarJogo(input);

            _jogoRepositoryMock.Verify(r => r.Alterar(It.Is<Jogo>(j => j.Titulo == "Novo" && j.Produtora == "Nova")), Times.Once);
        }
        [Test]
        public void DeletarJogo_DeveChamarRepositorioComId()
        {
            var id = 1;
            _jogoService.DeletarJogo(id);
            _jogoRepositoryMock.Verify(r => r.Deletar(id), Times.Once);
        }
        [Test]
        public void DeletarJogo_JogoNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var id = _faker.Random.Int(1, 1000);

            _jogoRepositoryMock.Setup(r => r.Deletar(id)).Throws(new InvalidOperationException("Jogo não encontrado"));

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _jogoService.DeletarJogo(id));
        }
        [Test]
        public void ObterJogoDtoPorId_DeveRetornarJogoDtoComDadosCorretos()
        {
            var jogo = new Jogo
            {
                Id = 1,
                Titulo = "Titulo",
                Produtora = "Produtora",
                DataCriacao = DateTime.Now,
                UsuarioCadastro = new Usuario
                {
                    Id = 1,
                    Nome = "Usuario",
                    Email = "email@teste.com",
                    Senha = "senha",
                    Tipo = TipoUsuario.Administrador,
                    DataCriacao = DateTime.Now
                },
                UsuariosQueAdquiriram = new List<UsuarioJogoAdquirido>()
            };

            _jogoRepositoryMock.Setup(r => r.ObterPorId(1)).Returns(jogo);

            var result = _jogoService.ObterJogoDtoPorId(1);

            Assert.That(jogo.Id, Is.EqualTo(result.Id));
            Assert.That(jogo.Titulo, Is.EqualTo(result.Titulo));
        }

        [Test]
        public void ObterJogoDtoPorTitulo_DeveRetornarJogoDtoComDadosCorretos()
        {
            var jogo = new Jogo
            {
                Id = 1,
                Titulo = "Titulo",
                Produtora = "Produtora",
                DataCriacao = DateTime.Now,
                UsuarioCadastro = new Usuario
                {
                    Id = 1,
                    Nome = "Usuario",
                    Email = "email@teste.com",
                    Senha = "senha",
                    Tipo = TipoUsuario.Padrao,
                    DataCriacao = DateTime.Now
                },
                UsuariosQueAdquiriram = new List<UsuarioJogoAdquirido>()
            };

            _jogoRepositoryMock.Setup(r => r.obterPorTitulo("Titulo")).Returns(jogo);

            var result = _jogoService.ObterJogoDtoPorTitulo("Titulo");

            Assert.That(jogo.Id, Is.EqualTo(result.Id));
            Assert.That(jogo.Titulo, Is.EqualTo(result.Titulo));
        }

        [Test]
        public void ObterTodosJogosDto_DeveRetornarListaDeJogos()
        {
            var jogos = new List<Jogo>
            {
                new Jogo
                {
                    Id = 1,
                    Titulo = "Jogo 1",
                    Produtora = "Produtora 1",
                    DataCriacao = DateTime.Now,
                    UsuarioCadastro = new Usuario
                    {
                        Id = 1,
                        Nome = "Usuario",
                        Email = "email@teste.com",
                        Senha = "senha",
                        Tipo = TipoUsuario.Padrao,
                        DataCriacao = DateTime.Now
                    },
                    UsuariosQueAdquiriram = new List<UsuarioJogoAdquirido>()
                }
            };

            _jogoRepositoryMock.Setup(r => r.ObterTodos()).Returns(jogos);

            var result = _jogoService.ObterTodosJogosDto();

            Assert.That(result, Is.Not.Empty);
            Assert.That(1, Is.EqualTo(result.Count()));
            Assert.That("Jogo 1", Is.EqualTo(result.First().Titulo));
        }
        [Test]
        public void AlterarJogo_DeveLancarExcecao_SeJogoNaoExistir()
        {
            _jogoRepositoryMock.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns((Jogo)null);

            var input = new JogoAlteracaoInput { Id = 1, Titulo = "Novo Título", Produtora = "Nova Produtora" };

            Assert.Throws<NullReferenceException>(() => _jogoService.AlterarJogo(input));
        }

        [Test]
        public void CadastrarJogo_DeveLancarExcecao_SeUsuarioNaoExistir()
        {
            _usuarioRepositoryMock.Setup(r => r.obterPorNome(It.IsAny<string>())).Returns((Usuario)null);

            var input = new JogoCadastroInput { Titulo = "God of War", Produtora = "Santa Monica" };

            Assert.Throws<NullReferenceException>(() => _jogoService.CadastrarJogo(input, "UsuarioInvalido"));
        }

        [Test]
        public void ObterJogoDtoPorId_DeveLancarExcecao_SeJogoNaoExistir()
        {
            _jogoRepositoryMock.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns((Jogo)null);

            Assert.Throws<NullReferenceException>(() => _jogoService.ObterJogoDtoPorId(999));
        }

        [Test]
        public void ObterJogoDtoPorTitulo_DeveLancarExcecao_SeTituloNaoExistir()
        {
            _jogoRepositoryMock.Setup(r => r.obterPorTitulo(It.IsAny<string>())).Returns((Jogo)null);

            Assert.Throws<NullReferenceException>(() => _jogoService.ObterJogoDtoPorTitulo("TituloInvalido"));
        }

        [Test]
        public void ObterTodosJogosDto_DeveRetornarListaVazia_SeNaoExistiremJogos()
        {
            _jogoRepositoryMock.Setup(r => r.ObterTodos()).Returns(new List<Jogo>());

            var resultado = _jogoService.ObterTodosJogosDto();

            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado, Is.Empty);
        }
    }
}
