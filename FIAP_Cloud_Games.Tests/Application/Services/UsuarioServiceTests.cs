using Moq;
using Application.Services;
using Domain.Interfaces.IRepository;
using Application.Input.UsuarioInput;
using Domain.Entity;
using Bogus;

namespace FIAP_Cloud_Games.Tests.Application.Services
{
    [TestFixture]
    public class UsuarioServiceTests
    {
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private UsuarioService _usuarioService;
        private readonly Faker _faker = new();

        [SetUp]
        public void SetUp()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        private Usuario GerarUsuarioComJogos()
        {
            var id = _faker.Random.Int(1, 1000);
            var nome = _faker.Name.FullName();
            var email = _faker.Internet.Email();
            var senha = _faker.Internet.Password();
            var dataCriacao = _faker.Date.Past();

            var jogo = new Jogo
            {
                Id = _faker.Random.Int(1, 100),
                Titulo = _faker.Lorem.Word(),
                Produtora = _faker.Company.CompanyName(),
                DataCriacao = _faker.Date.Past()
            };

            var usuario = new Usuario
            {
                Id = id,
                Nome = nome,
                Email = email,
                Senha = senha,
                Tipo = TipoUsuario.Administrador,
                DataCriacao = dataCriacao,
                JogosCadastrados = new List<Jogo> { jogo },
                JogosAdquiridos = new List<UsuarioJogoAdquirido>
                {
                    new UsuarioJogoAdquirido
                    {
                        Id = _faker.Random.Int(1, 1000),
                        UsuarioId = id,
                        JogoId = jogo.Id,
                        DataCriacao = _faker.Date.Past(),
                        Usuario = new Usuario { Id = id, Nome = nome, Email = email, Senha = senha, Tipo = TipoUsuario.Administrador, DataCriacao = dataCriacao },
                        Jogo = jogo
                    }
                }
            };

            return usuario;
        }
        [Test]
        public void ObterTodosUsuariosDto_DeveRetornarListaDeDtos()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nome = _faker.Name.FullName(),
                    Email = _faker.Internet.Email(),
                    Tipo = TipoUsuario.Padrao,
                    Senha = _faker.Internet.Password(),
                    DataCriacao = _faker.Date.Past(),
                    JogosCadastrados = new List<Jogo>
                    {
                        new Jogo { Id = 1, Titulo = _faker.Lorem.Word(), Produtora = _faker.Company.CompanyName(), DataCriacao = _faker.Date.Past() }
                    },
                    JogosAdquiridos = new List<UsuarioJogoAdquirido>
                    {
                        new UsuarioJogoAdquirido
                        {
                            Id = 1,
                            JogoId = 1,
                            UsuarioId = 1,
                            DataCriacao = _faker.Date.Past(),
                            Usuario = new Usuario { Id = 1, Nome = _faker.Name.FullName(), Email = _faker.Internet.Email(), Senha = _faker.Internet.Password(), Tipo = TipoUsuario.Padrao, DataCriacao = _faker.Date.Past() },
                            Jogo = new Jogo { Id = 1, Titulo = _faker.Lorem.Word(), Produtora = _faker.Company.CompanyName(), DataCriacao = _faker.Date.Past() }
                        }
                    }
                }
            };

            _usuarioRepositoryMock.Setup(r => r.ObterTodos()).Returns(usuarios);

            var result = _usuarioService.ObterTodosUsuariosDto();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
        }
        [Test]
        public void ObterUsuarioDtoPorId_DeveRetornarUsuarioDto()
        {
            var usuario = GerarUsuarioComJogos();

            _usuarioRepositoryMock.Setup(r => r.ObterPorId(usuario.Id)).Returns(usuario);

            var result = _usuarioService.ObterUsuarioDtoPorId(usuario.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(usuario.Id, Is.EqualTo(result.Id));
        }
        [Test]
        public void ObterUsuarioDtoPorNome_DeveRetornarUsuarioDto()
        {
            var usuario = GerarUsuarioComJogos();

            _usuarioRepositoryMock.Setup(r => r.obterPorNome(usuario.Nome)).Returns(usuario);

            var result = _usuarioService.ObterUsuarioDtoPorNome(usuario.Nome);

            Assert.That(result, Is.Not.Null);
            Assert.That(usuario.Nome, Is.EqualTo(result.Nome));
        }
        [Test]
        public void ObterTodosUsuariosDto_RetornaNulo_DeveLancarExcecao()
        {
            // Arrange
            _usuarioRepositoryMock.Setup(r => r.ObterTodos()).Returns((List<Usuario>)null);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _usuarioService.ObterTodosUsuariosDto());
        }
        [Test]
        public void ObterUsuarioDtoPorId_UsuarioNaoExiste_DeveLancarExcecao()
        {
            int idInvalido = _faker.Random.Int(1, 100);
            _usuarioRepositoryMock.Setup(r => r.ObterPorId(idInvalido)).Returns((Usuario)null);

            Assert.Throws<NullReferenceException>(() => _usuarioService.ObterUsuarioDtoPorId(idInvalido));
        }
        [Test]
        public void ObterUsuarioDtoPorNome_UsuarioNaoExiste_DeveLancarExcecao()
        {
            string nomeInvalido = _faker.Name.FullName();
            _usuarioRepositoryMock.Setup(r => r.obterPorNome(nomeInvalido)).Returns((Usuario)null);

            Assert.Throws<NullReferenceException>(() => _usuarioService.ObterUsuarioDtoPorNome(nomeInvalido));
        }
        [Test]
        public void CadastrarUsuarioAdministrador_DeveCadastrarComTipoAdministrador()
        {
            var input = new UsuarioCadastroInput
            {
                Nome = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Senha = _faker.Internet.Password()
            };

            _usuarioService.CadastrarUsuarioAdministrador(input);

            _usuarioRepositoryMock.Verify(r => r.Cadastrar(It.Is<Usuario>(
                u => u.Nome == input.Nome &&
                     u.Email == input.Email &&
                     u.Senha == input.Senha &&
                     u.Tipo == TipoUsuario.Administrador
            )), Times.Once);
        }
        [Test]
        public void CadastrarUsuarioPadrao_DeveCadastrarComTipoPadrao()
        {
            var input = new UsuarioCadastroInput
            {
                Nome = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Senha = _faker.Internet.Password()
            };

            _usuarioService.CadastrarUsuarioPadrao(input);

            _usuarioRepositoryMock.Verify(r => r.Cadastrar(It.Is<Usuario>(
                u => u.Nome == input.Nome &&
                     u.Email == input.Email &&
                     u.Senha == input.Senha &&
                     u.Tipo == TipoUsuario.Padrao
            )), Times.Once);
        }
        [Test]
        public void AlterarUsuario_DeveAlterarDadosDoUsuario()
        {
            var id = _faker.Random.Int(1, 100);
            var usuarioExistente = new Usuario
            {
                Id = id,
                Nome = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Senha = _faker.Internet.Password(),
                Tipo = TipoUsuario.Padrao
            };

            var novoNome = _faker.Name.FullName();
            var novoEmail = _faker.Internet.Email();
            var novaSenha = _faker.Internet.Password();
            var novoTipo = TipoUsuario.Administrador;

            var input = new UsuarioAlteracaoInput
            {
                Id = id,
                Nome = novoNome,
                Email = novoEmail,
                Senha = novaSenha,
                Tipo = novoTipo
            };

            _usuarioRepositoryMock.Setup(r => r.ObterPorId(id)).Returns(usuarioExistente);

            _usuarioService.AlterarUsuario(input);

            _usuarioRepositoryMock.Verify(r => r.ObterPorId(id), Times.Once);
            _usuarioRepositoryMock.Verify(r => r.Alterar(It.Is<Usuario>(
                u => u.Id == id &&
                     u.Nome == novoNome &&
                     u.Email == novoEmail &&
                     u.Senha == novaSenha &&
                     u.Tipo == novoTipo
            )), Times.Once);
        }
        [Test]
        public void AlterarUsuario_UsuarioNaoExiste_DeveLancarExcecao()
        {
            var input = new UsuarioAlteracaoInput
            {
                Id = _faker.Random.Int(1, 100),
                Nome = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Senha = _faker.Internet.Password(),
                Tipo = TipoUsuario.Padrao
            };

            _usuarioRepositoryMock.Setup(r => r.ObterPorId(input.Id)).Returns((Usuario)null);

            Assert.Throws<NullReferenceException>(() => _usuarioService.AlterarUsuario(input));
        }
        [Test]
        public void DeletarUsuario_DeveChamarRepositorioComIdCorreto()
        {
            var id = _faker.Random.Int(1, 1000);

            _usuarioService.DeletarUsuario(id);

            _usuarioRepositoryMock.Verify(r => r.Deletar(id), Times.Once);
        }
        [Test]
        public void DeletarUsuario_UsuarioNaoExiste_DeveLancarExcecao()
        {
            var id = _faker.Random.Int(1, 1000);

            _usuarioRepositoryMock.Setup(r => r.Deletar(id)).Throws(new InvalidOperationException("Usuário não encontrado"));

            Assert.Throws<InvalidOperationException>(() => _usuarioService.DeletarUsuario(id));
        }
    }
}