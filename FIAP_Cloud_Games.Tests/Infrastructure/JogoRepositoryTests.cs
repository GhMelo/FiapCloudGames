using Bogus;
using Domain.Entity;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP_Cloud_Games.Tests.Infrastructure;

[TestFixture]
public class JogoRepositoryTests
{
    private ApplicationDbContext _context;
    private JogoRepository _jogoRepository;
    private UsuarioRepository _usuarioRepository;
    private readonly Faker _faker = new();

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

    private Jogo GerarJogoFaker(Usuario usuario)
    {
        return new Jogo
        {
            Titulo = _faker.Commerce.ProductName(),
            Produtora = _faker.Company.CompanyName(),
            UsuarioCadastroId = usuario.Id,
            UsuarioCadastro = usuario
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
    }

    [Test]
    public void CadastrarJogo_DeveAdicionarJogoAoBanco()
    {
        var usuario = GerarUsuarioFaker();
        _usuarioRepository.Cadastrar(usuario);

        var jogo = GerarJogoFaker(usuario);
        _jogoRepository.Cadastrar(jogo);

        var jogoNoBanco = _jogoRepository.ObterPorId(jogo.Id);

        Assert.That(jogoNoBanco, Is.Not.Null);
        Assert.That(jogoNoBanco!.Titulo, Is.EqualTo(jogo.Titulo));
    }
    [Test]
    public void CadastrarJogo_DeveLancarExcecao_QuandoCamposObrigatoriosNaoForemPreenchidos()
    {
        var jogoInvalido = new Jogo();

        Assert.Throws<Exception>(() =>
        {
            _jogoRepository.Cadastrar(jogoInvalido);
        });

        Assert.Throws<Exception>(() =>
        {
            jogoInvalido.Titulo = _faker.Commerce.ProductName();
            _jogoRepository.Cadastrar(jogoInvalido);
        });

        Assert.Throws<Exception>(() =>
        {
            jogoInvalido.Titulo = null;
            jogoInvalido.Produtora = _faker.Company.CompanyName();    
            _jogoRepository.Cadastrar(jogoInvalido);
        });
    }
    [Test]
    public void ObterPorId_DeveRetornarJogo()
    {
        var usuario = GerarUsuarioFaker();
        _usuarioRepository.Cadastrar(usuario);

        var jogo = GerarJogoFaker(usuario);
        _jogoRepository.Cadastrar(jogo);

        var resultado = _jogoRepository.ObterPorId(jogo.Id);

        Assert.That(resultado, Is.Not.Null);
        Assert.That(resultado!.Produtora, Is.EqualTo(jogo.Produtora));
    }
    [Test]
    public void ObterPorId_DeveRetornarNull_QuandoIdNaoExiste()
    {
        var resultado = _jogoRepository.ObterPorId(999999);
        Assert.That(resultado, Is.Null);
    }
    [Test]
    public void ObterTodos_DeveRetornarTodosJogos()
    {
        var usuario = GerarUsuarioFaker();
        _usuarioRepository.Cadastrar(usuario);

        var jogo1 = GerarJogoFaker(usuario);
        var jogo2 = GerarJogoFaker(usuario);

        _jogoRepository.Cadastrar(jogo1);
        _jogoRepository.Cadastrar(jogo2);

        var lista = _jogoRepository.ObterTodos();

        Assert.That(lista.Count, Is.EqualTo(2));
    }
    [Test]
    public void ObterPorTitulo_DeveRetornarJogo_QuandoJogoExiste()
    {
        var jogoFake = GerarJogoFaker(GerarUsuarioFaker());
        _jogoRepository.Cadastrar(jogoFake);

        var resultado = _jogoRepository.obterPorTitulo(jogoFake.Titulo);

        Assert.That(resultado, Is.Not.Null);
        Assert.That(resultado!.Titulo, Is.EqualTo(jogoFake.Titulo));
    }
    [Test]
    public void ObterPorTitulo_DeveRetornarNull_QuandoTituloNaoExiste()
    {
        var resultado = _jogoRepository.obterPorTitulo("Inexistente");
        Assert.That(resultado, Is.Null);
    }
    [Test]
    public void Alterar_DeveAtualizarDadosDoJogo()
    {
        var usuario = GerarUsuarioFaker();
        _usuarioRepository.Cadastrar(usuario);

        var jogo = GerarJogoFaker(usuario);
        _jogoRepository.Cadastrar(jogo);

        var novoTitulo = _faker.Commerce.ProductName();
        jogo.Titulo = novoTitulo;

        _jogoRepository.Alterar(jogo);

        var atualizado = _jogoRepository.ObterPorId(jogo.Id);
        Assert.That(atualizado!.Titulo, Is.EqualTo(novoTitulo));
    }
    [Test]
    public void Alterar_DeveLancarExcecao_SeJogoNaoExiste()
    {
        var jogoFake = GerarJogoFaker(GerarUsuarioFaker());
        Assert.Throws<Exception>(() => _jogoRepository.Alterar(jogoFake));
    }
    [Test]
    public void AlterarJogo_DeveLancarExcecao_QuandoIdForInvalido()
    {
        var jogoFake = GerarJogoFaker(GerarUsuarioFaker());
        _jogoRepository.Cadastrar(jogoFake);

        jogoFake.Id = 0;

        Assert.Throws<Exception>(() => _jogoRepository.Alterar(jogoFake));
    }
    [Test]
    public void Deletar_DeveRemoverJogoDoBanco()
    {
        var usuario = GerarUsuarioFaker();
        _usuarioRepository.Cadastrar(usuario);

        var jogo = GerarJogoFaker(usuario);
        _jogoRepository.Cadastrar(jogo);

        _jogoRepository.Deletar(jogo.Id);

        var deletado = _jogoRepository.ObterPorId(jogo.Id);
        Assert.That(deletado, Is.Null);
    }
    [Test]
    public void Deletar_DeveLancarExcecao_SeJogoNaoExiste()
    {
        Assert.Throws<Exception>(() => _jogoRepository.Deletar(999999));
    }
}