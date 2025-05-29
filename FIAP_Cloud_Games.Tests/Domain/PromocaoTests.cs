using Bogus;
using Domain.Entity;

namespace FIAP_Cloud_Games.Tests.Domain
{
    [TestFixture]
    internal class PromocaoTests
    {
        private readonly Faker _faker = new(); 
        
        [Test]
        public void CriarPromocao_SemValores_DeveInicializarDataCriacao()
        {
            var promocao = new Promocao();
            Assert.That(promocao.DataCriacao, Is.Not.Null);
        }
        [Test]
        public void CriarPromocao_DeveDefinirPropriedadesCorretamente()
        {
            var nomePromocao = _faker.Company.CompanyName();
            var porcentagem = _faker.Random.Int(1, 100);
            var promocaoAtiva = _faker.Random.Bool();
            var jogoId = _faker.Random.Int(1, 10000);

            var jogoPromocao = new Jogo
            {
                Titulo = _faker.Commerce.ProductName(),
                Produtora = _faker.Company.CompanyName(),
                UsuarioCadastroId = _faker.Random.Int(1, 10000),
                DataCriacao = DateTime.Now
            };

            var promocao = new Promocao
            {
                NomePromocao = nomePromocao,
                Porcentagem = porcentagem,
                PromocaoAtiva = promocaoAtiva,
                JogoId = jogoId,
                JogoPromocao = jogoPromocao
            };

            Assert.That(promocao.NomePromocao, Is.EqualTo(nomePromocao));
            Assert.That(promocao.Porcentagem, Is.EqualTo(porcentagem));
            Assert.That(promocao.PromocaoAtiva, Is.EqualTo(promocaoAtiva));
            Assert.That(promocao.JogoId, Is.EqualTo(jogoId));
            Assert.That(promocao.JogoPromocao, Is.EqualTo(jogoPromocao));
            Assert.That(promocao.DataCriacao, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(2)));
        }
    }
}
