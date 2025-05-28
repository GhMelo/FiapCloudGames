using Bogus;
using Domain.Entity;

namespace FIAP_Cloud_Games.Tests.Domain;

[TestFixture]
public class JogoTests
{
    private readonly Faker _faker = new();

    [Test]
    public void CriarJogo_DeveDefinirPropriedadesCorretamente()
    {
        var titulo = _faker.Commerce.ProductName();
        var produtora = _faker.Company.CompanyName();
        var usuarioCadastroId = _faker.Random.Int(1, 10000);

        var usuarioCadastro = new Usuario
        {
            Id = usuarioCadastroId,
            Nome = _faker.Name.FullName(),
            Email = _faker.Internet.Email(),
            Senha = _faker.Internet.Password(),
            Tipo = _faker.PickRandom<TipoUsuario>()
        };

        var jogo = new Jogo
        {
            Titulo = titulo,
            Produtora = produtora,
            UsuarioCadastroId = usuarioCadastroId,
            UsuarioCadastro = usuarioCadastro
        };

        Assert.That(jogo.Titulo, Is.EqualTo(titulo));
        Assert.That(jogo.Produtora, Is.EqualTo(produtora));
        Assert.That(jogo.UsuarioCadastroId, Is.EqualTo(usuarioCadastroId));
        Assert.That(jogo.UsuarioCadastro, Is.EqualTo(usuarioCadastro));
        Assert.That(jogo.UsuariosQueAdquiriram, Is.Not.Null);
        Assert.That(jogo.UsuariosQueAdquiriram, Is.Empty);
        Assert.That(jogo.DataCriacao, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(2)));
    }
    [Test]
    public void CriarJogo_SemValores_DeveInicializarListaEDataCriacao()
    {
        var jogo = new Jogo();

        Assert.That(jogo.Titulo, Is.Null.Or.Empty);
        Assert.That(jogo.Produtora, Is.Null.Or.Empty);
        Assert.That(jogo.UsuariosQueAdquiriram, Is.Not.Null);
        Assert.That(jogo.UsuariosQueAdquiriram, Is.Empty);
        Assert.That(jogo.DataCriacao, Is.Not.Null);
    }
}