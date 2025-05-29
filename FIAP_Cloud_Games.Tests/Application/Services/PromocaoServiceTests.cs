using Application.Input.PromocaoInput;
using Application.Services;
using Bogus;
using Domain.Entity;
using Domain.Interfaces.IRepository;
using Moq;

namespace FIAP_Cloud_Games.Tests.Application.Services
{
    [TestFixture]
    internal class PromocaoServiceTests
    {
        private Faker _faker = new();
        private Mock<IJogoRepository> _jogoRepositoryMock;
        private Mock<IPromocaoRepository> _promocaoRepositoryMock;
        private PromocaoService _promocaoService;
        [SetUp]
        public void SetUp()
        {
            _faker = new Faker();
            _jogoRepositoryMock = new Mock<IJogoRepository>();
            _promocaoRepositoryMock = new Mock<IPromocaoRepository>();
            _promocaoService = new PromocaoService(_promocaoRepositoryMock.Object, _jogoRepositoryMock.Object);
        }
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
                Id = _faker.Random.Int(1, 10000),
                Titulo = _faker.Commerce.ProductName(),
                Produtora = _faker.Company.CompanyName(),
                UsuarioCadastroId = usuarioCadastro.Id,
                UsuarioCadastro = usuarioCadastro,
            };
        }

        private Promocao GerarPromocaoFaker(Jogo jogoPromocao)
        {
            return new Promocao
            {
                Id = _faker.Random.Int(1, 10000),
                JogoId = jogoPromocao.Id,
                NomePromocao = _faker.Commerce.ProductName(),
                Porcentagem = _faker.Random.Int(1, 100),
                PromocaoAtiva = _faker.Random.Bool(),
                JogoPromocao = jogoPromocao,
            };
        }

        [Test]
        public void ObterPromocaoDtoPorNomePromocao_DeveRetornarPromocaoDtoComDadosValidos()
        {
            var promocao = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker())); 
            _promocaoRepositoryMock.Setup(r => r.obterPorNomePromocao(promocao.NomePromocao)).Returns(promocao);

            var result = _promocaoService.ObterPromocaoDtoPorNomePromocao(promocao.NomePromocao);

            Assert.That(promocao.Id, Is.EqualTo(result.Id));
            Assert.That(promocao.NomePromocao, Is.EqualTo(result.NomePromocao));
        }
        [Test]
        public void ObterPromocaoDtoPorNomePromocao_DeveRetornarNullException_QuandoNaoExistemPromocoesComNomePromocao()
        {
            _promocaoRepositoryMock.Setup(r => r.obterPorNomePromocao("Inexistente")).Returns((Promocao)null);

            Assert.Throws<NullReferenceException>(() => _promocaoService.ObterPromocaoDtoPorNomePromocao("Inexistente"));
        }
        [Test]
        public void ObterPromocaoDtoAtivas_DeveRetornarListaPromocaoDtoComDadosValidos()
        {
            var promocoes = new List<Promocao>();

            var promocao1 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            var promocao2 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));

            promocao1.PromocaoAtiva = true;
            promocao2.PromocaoAtiva = true;

            promocoes.Add(promocao1);
            promocoes.Add(promocao2);

            _promocaoRepositoryMock.Setup(r => r.ObterTodos()).Returns(promocoes);

            var result = _promocaoService.ObterTodosPromocaoDtoAtivas();

            Assert.That(result, Is.Not.Empty);
            Assert.That(2, Is.EqualTo(result.Count()));
            Assert.That(promocoes.FirstOrDefault().NomePromocao, Is.EqualTo(result.First().NomePromocao));
        }
        [Test]
        public void ObterPromocaoDtoAtivas_DeveRetornarListaVazia_QuandoNaoExistemPromocoesAtivas()
        {
            var promocoes = new List<Promocao>();
            
            var promocao1 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            var promocao2 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));

            promocao1.PromocaoAtiva = false;
            promocao2.PromocaoAtiva = false;

            promocoes.Add(promocao1);
            promocoes.Add(promocao2);

            _promocaoRepositoryMock.Setup(r => r.ObterTodos()).Returns(promocoes);

            var result = _promocaoService.ObterTodosPromocaoDtoAtivas();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void ObterPromocaoDtoInativas_DeveRetornarListaPromocaoDtoComDadosValidos()
        {
            var promocoes = new List<Promocao>();

            var promocao1 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            var promocao2 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));

            promocao1.PromocaoAtiva = false;
            promocao2.PromocaoAtiva = false;

            promocoes.Add(promocao1);
            promocoes.Add(promocao2);

            _promocaoRepositoryMock.Setup(r => r.ObterTodos()).Returns(promocoes);

            var result = _promocaoService.ObterTodosPromocaoDtoInativas();

            Assert.That(result, Is.Not.Empty);
            Assert.That(2, Is.EqualTo(result.Count()));
            Assert.That(promocoes.FirstOrDefault().NomePromocao, Is.EqualTo(result.First().NomePromocao));
        }
        [Test]
        public void ObterPromocaoDtoInativas_DeveRetornarListaVazia_QuandoNaoExistemPromocoesInativas()
        {
            var promocoes = new List<Promocao>();

            var promocao1 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            var promocao2 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));

            promocao1.PromocaoAtiva = true;
            promocao2.PromocaoAtiva = true;

            promocoes.Add(promocao1);
            promocoes.Add(promocao2);

            _promocaoRepositoryMock.Setup(r => r.ObterTodos()).Returns(promocoes);

            var result = _promocaoService.ObterTodosPromocaoDtoInativas();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void ObterPromocaoDtoPorId_DeveRetornarPromocaoDtoComDadosValidos()
        {
            var promocao = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            _promocaoRepositoryMock.Setup(r => r.ObterPorId(promocao.Id)).Returns(promocao);

            var result = _promocaoService.ObterPromocaoDtoPorId(promocao.Id);

            Assert.That(promocao.Id, Is.EqualTo(result.Id));
            Assert.That(promocao.NomePromocao, Is.EqualTo(result.NomePromocao));
        }
        [Test]
        public void ObterTodosPromocaoDto_DeveRetornarListaDePromocoes()
        {
            var promocoes = new List<Promocao>();

            var promocao1 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            var promocao2 = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));

            promocoes.Add(promocao1);
            promocoes.Add(promocao2);

            _promocaoRepositoryMock.Setup(r => r.ObterTodos()).Returns(promocoes);

            var result = _promocaoService.ObterTodosPromocaoDto();

            Assert.That(result, Is.Not.Empty);
            Assert.That(2, Is.EqualTo(result.Count()));
            Assert.That(promocoes.FirstOrDefault().Id, Is.EqualTo(result.First().Id));
        }
        [Test]
        public void ObterTodosPromocaoDto_DeveRetornarListaVazia_SeNaoExistiremPromocoes()
        {
            _promocaoRepositoryMock.Setup(r => r.ObterTodos()).Returns(new List<Promocao>());

            var result = _promocaoService.ObterTodosPromocaoDto();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void ObterPromocaoDtoPorId_DeveLancarExcecao_SePromocaoNaoExistir()
        {
            _promocaoRepositoryMock.Setup(r => r.ObterPorId(999)).Returns((Promocao)null);

            Assert.Throws<NullReferenceException>(() => _promocaoService.ObterPromocaoDtoPorId(999));
        }
        [Test]
        public void CadastrarPromocao_DeveChamarCadastrarNoRepositorio_ComDadosValidos()
        {
            var jogoId = _faker.Random.Int(1, 999);
            var input = new PromocaoCadastroInput
            {
                NomePromocao = _faker.Name.FullName(),
                Porcentagem = _faker.Random.Int(1, 100),
                PromocaoAtiva = _faker.Random.Bool(),
                JogoId = _faker.Random.Int(1, 999),
            };

            var jogo = GerarJogoFaker(GerarUsuarioFaker());
            jogo.Id = jogoId;
            input.JogoId = jogoId;

            _jogoRepositoryMock.Setup(r => r.ObterPorId(jogoId)).Returns(jogo);

            Promocao PromocaoCadastrada = null;

            _promocaoRepositoryMock.Setup(r => r.Cadastrar(It.IsAny<Promocao>())).Callback<Promocao>(j => PromocaoCadastrada = j);

            _promocaoService.CadastrarPromocao(input);

            _promocaoRepositoryMock.Verify(r => r.Cadastrar(It.IsAny<Promocao>()), Times.Once);

            Assert.That(input.NomePromocao, Is.EqualTo(PromocaoCadastrada.NomePromocao));
            Assert.That(input.Porcentagem, Is.EqualTo(PromocaoCadastrada.Porcentagem));
            Assert.That(input.PromocaoAtiva, Is.EqualTo(PromocaoCadastrada.PromocaoAtiva));
            Assert.That(input.JogoId, Is.EqualTo(PromocaoCadastrada.JogoId));
        }
        [Test]
        public void CadastrarPromocao_DeveLancarExcecao_SeJogoNaoExistir()
        {
            _jogoRepositoryMock.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns((Jogo)null);

            var input = new PromocaoCadastroInput
            {
                NomePromocao = _faker.Name.FullName(),
                Porcentagem = _faker.Random.Int(1, 100),
                PromocaoAtiva = _faker.Random.Bool(),
                JogoId = _faker.Random.Int(1, 999),
            };

            Assert.Throws<NullReferenceException>(() => _promocaoService.CadastrarPromocao(input));
        }
        [Test]
        public void AlterarPromocao_DeveAlterarDadosCorretamente()
        {
            var promocaoExistente = GerarPromocaoFaker(GerarJogoFaker(GerarUsuarioFaker()));
            promocaoExistente.NomePromocao = "Antiga";
            promocaoExistente.Porcentagem = 50;
            promocaoExistente.PromocaoAtiva = true;

            var input = new PromocaoAlteracaoInput { 
                PromocaoId = promocaoExistente.Id, 
                NomePromocao = "Novo", 
                Porcentagem = 75, 
                PromocaoAtiva = false 
            };

            _promocaoRepositoryMock.Setup(r => r.ObterPorId(promocaoExistente.Id)).Returns(promocaoExistente);

            _promocaoService.AlterarPromocao(input);

            _promocaoRepositoryMock.Verify(r => r.Alterar(It.Is<Promocao>(j => j.NomePromocao == "Novo" && j.Porcentagem == 75 && j.PromocaoAtiva == false)), Times.Once);
        }
        [Test]
        public void AlterarPromocao_DeveLancarExcecao_SePromocaoNaoExistir()
        {
            _promocaoRepositoryMock.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns((Promocao)null);

            var input = new PromocaoAlteracaoInput
            {
                PromocaoId = _faker.Random.Int(1, 999),
                NomePromocao = _faker.Commerce.ProductName(),
                Porcentagem = _faker.Random.Int(1, 100),
                PromocaoAtiva = _faker.Random.Bool()
            };

            Assert.Throws<NullReferenceException>(() => _promocaoService.AlterarPromocao(input));
        }
        [Test]
        public void DeletarPromocao_DeveChamarRepositorioComId()
        {
            var id = 1;
            _promocaoService.DeletarPromocao(id);
            _promocaoRepositoryMock.Verify(r => r.Deletar(id), Times.Once);
        }
        [Test]
        public void DeletarPromocao_DeveLancarExcecao_QuandoJogoNaoExiste()
        {
            var id = _faker.Random.Int(1, 1000);

            _promocaoRepositoryMock.Setup(r => r.Deletar(id)).Throws(new InvalidOperationException("Promocao não encontrado"));

            Assert.Throws<InvalidOperationException>(() => _promocaoService.DeletarPromocao(id));
        }
    }
}
