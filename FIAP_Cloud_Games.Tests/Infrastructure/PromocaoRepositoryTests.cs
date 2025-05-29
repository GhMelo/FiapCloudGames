using Application.Input.UsuarioInput;
using Bogus;
using Domain.Entity;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP_Cloud_Games.Tests.Infrastructure
{
    [TestFixture]
    internal class PromocaoRepositoryTests
    {
        private readonly Faker _faker = new();
        private ApplicationDbContext _context;
        private PromocaoRepository _promocaoRepository;
        private JogoRepository _jogoRepository;
        private UsuarioRepository _usuarioRepository;

        private Usuario GerarUsuarioFaker()
        {
            return new Usuario
            {
                Id = _faker.Random.Int(1, 10000),
                Nome = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Senha = _faker.Internet.Password(),
                Tipo = _faker.PickRandom<TipoUsuario>()
            };
        }

        private Jogo GerarJogoFaker(Usuario usuarioCadastro)
        {
            return new Jogo
            {
                Titulo = _faker.Commerce.ProductName(),
                Produtora = _faker.Company.CompanyName(),
                UsuarioCadastroId = _faker.Random.Int(1, 10000),
                UsuarioCadastro = usuarioCadastro,
            };
        }

        private Promocao GerarPromocaoFaker(Jogo jogoPromocao)
        {
            return new Promocao
            {
                NomePromocao = _faker.Commerce.ProductName(),
                Porcentagem = _faker.Random.Int(1, 100),
                PromocaoAtiva = _faker.Random.Bool(),
                JogoPromocao = jogoPromocao,
            };
        }
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            //Deleta o banco de dados toda vez para um teste não interferir no outro
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _jogoRepository = new JogoRepository(_context);
            _usuarioRepository = new UsuarioRepository(_context);
            _promocaoRepository = new PromocaoRepository(_context);
        }
        [Test]
        public void CadastrarPromocao_DeveAdicionarPromocaoAoBanco()
        {
            var usuario = GerarUsuarioFaker();
            _usuarioRepository.Cadastrar(usuario);

            var jogo = GerarJogoFaker(usuario);
            _jogoRepository.Cadastrar(jogo);

            var promocao = GerarPromocaoFaker(jogo);
            _promocaoRepository.Cadastrar(promocao);

            var promocaoNoBanco = _promocaoRepository.ObterPorId(promocao.Id);

            Assert.That(promocaoNoBanco, Is.Not.Null);
            Assert.That(promocaoNoBanco!.NomePromocao, Is.EqualTo(promocao.NomePromocao));
            Assert.That(promocaoNoBanco!.Porcentagem, Is.EqualTo(promocao.Porcentagem));
            Assert.That(promocaoNoBanco!.PromocaoAtiva, Is.EqualTo(promocao.PromocaoAtiva));
            Assert.That(promocaoNoBanco!.JogoId, Is.EqualTo(jogo.Id));
            Assert.That(promocaoNoBanco!.JogoPromocao, Is.EqualTo(jogo));
            Assert.That(promocaoNoBanco!.JogoPromocao.UsuarioCadastro, Is.EqualTo(usuario));
        }
        [Test]
        public void CadastrarPromocao_DeveLancarExcecao_QuandoCamposObrigatoriosNaoForemPreenchidos()
        {
            var promocaoInvalida = new Promocao();

            Assert.Throws<Exception>(() =>
            {
                _promocaoRepository.Cadastrar(promocaoInvalida);
            });
        }
        [Test]
        public void ObterPorId_DeveRetornarPromocao()
        {
            var usuario = GerarUsuarioFaker();
            _usuarioRepository.Cadastrar(usuario);

            var jogo = GerarJogoFaker(usuario);
            _jogoRepository.Cadastrar(jogo);

            var promocao = GerarPromocaoFaker(jogo);
            _promocaoRepository.Cadastrar(promocao);

            var promocaoNoBanco = _promocaoRepository.ObterPorId(promocao.Id);

            Assert.That(promocaoNoBanco, Is.Not.Null);
            Assert.That(promocaoNoBanco!.NomePromocao, Is.EqualTo(promocao.NomePromocao));
        }
        [Test]
        public void ObterPorId_DeveRetornarNull_QuandoIdNaoExiste()
        {
            var resultado = _promocaoRepository.ObterPorId(999999);
            Assert.That(resultado, Is.Null);
        }
        [Test]
        public void ObterTodos_DeveRetornarTodasPromocoes()
        {

            var usuario = GerarUsuarioFaker();
            _usuarioRepository.Cadastrar(usuario);

            var jogo1 = GerarJogoFaker(usuario);
            var jogo2 = GerarJogoFaker(usuario);

            _jogoRepository.Cadastrar(jogo1);
            _jogoRepository.Cadastrar(jogo2);

            var promocao1 = GerarPromocaoFaker(jogo1);
            _promocaoRepository.Cadastrar(promocao1);

            var promocao2 = GerarPromocaoFaker(jogo2);
            _promocaoRepository.Cadastrar(promocao2);

            var lista = _promocaoRepository.ObterTodos();

            Assert.That(lista.Count, Is.EqualTo(2));
        }
        [Test]
        public void ObterPorNomePromocao_DeveRetornarPromocao_QuandoPromocaoExiste()
        {

            var promocaoFake = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            _promocaoRepository.Cadastrar(promocaoFake);

            var resultado = _promocaoRepository.obterPorNomePromocao(promocaoFake.NomePromocao);

            Assert.That(resultado, Is.Not.Null);
            Assert.That(resultado!.NomePromocao, Is.EqualTo(promocaoFake.NomePromocao));
        }
        [Test]
        public void ObterPorNomePromocao_DeveRetornarNull_QuandoNomePromocaoNaoExiste()
        {
            var resultado = _promocaoRepository.obterPorNomePromocao("Inexistente");
            Assert.That(resultado, Is.Null);
        }
        [Test]
        public void Alterar_DeveAtualizarDadosDaPromocao()
        {

            var usuario = GerarUsuarioFaker();
            _usuarioRepository.Cadastrar(usuario);

            var jogo = GerarJogoFaker(usuario);
            _jogoRepository.Cadastrar(jogo);

            var promocao = GerarPromocaoFaker(jogo);
            _promocaoRepository.Cadastrar(promocao);

            var novoNomePromocao = _faker.Commerce.ProductName();
            promocao.NomePromocao = novoNomePromocao;

            _promocaoRepository.Alterar(promocao);

            var atualizado = _promocaoRepository.ObterPorId(promocao.Id);
            Assert.That(atualizado!.NomePromocao, Is.EqualTo(novoNomePromocao));
        }
        [Test]
        public void Alterar_DeveLancarExcecao_SePromocaoNaoExiste()
        {
            var promocaoFake = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            Assert.Throws<Exception>(() => _promocaoRepository.Alterar(promocaoFake));
        }
        [Test]
        public void AlterarPromocao_DeveLancarExcecao_QuandoIdForInvalido()
        {
            var promocaoFake = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            _promocaoRepository.Cadastrar(promocaoFake);

            promocaoFake.Id = 0;

            Assert.Throws<Exception>(() => _promocaoRepository.Alterar(promocaoFake));
        }
        [Test]
        public void Deletar_DeveRemoverPromocaoDoBanco()
        {
            var usuario = GerarUsuarioFaker();
            _usuarioRepository.Cadastrar(usuario);

            var jogo = GerarJogoFaker(usuario);
            _jogoRepository.Cadastrar(jogo);

            var promocao = GerarPromocaoFaker(jogo);
            _promocaoRepository.Cadastrar(promocao);

            _promocaoRepository.Deletar(promocao.Id);

            var deletado = _promocaoRepository.ObterPorId(promocao.Id);
            Assert.That(deletado, Is.Null);
        }
        [Test]
        public void Deletar_DeveLancarExcecao_SePromocaoNaoExiste()
        {
            Assert.Throws<Exception>(() => _promocaoRepository.Deletar(999999));
        }
    }
}
