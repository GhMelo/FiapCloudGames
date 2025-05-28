using Application.Services;
using Bogus;
using Domain.Interfaces.IRepository;
using Moq;

namespace FIAP_Cloud_Games.Tests.Application.Services
{
    [TestFixture]
    internal class PromocaoServiceTests
    {
        private Faker _faker = new();

        [SetUp]
        public void SetUp()
        {
            _faker = new Faker();
        }
        [Test]
        public void ObterPromocaoDtoPorTituloJogo_DeveRetornarListaPromocaoDtoComDadosValidos()
        {

        }
        [Test]
        public void ObterPromocaoDtoPorTituloJogo_DeveRetornarListaVazia_QuandoNaoExistemPromocoesComJogo()
        {

        }
        [Test]
        public void ObterPromocaoDtoAtivas_DeveRetornarListaPromocaoDtoComDadosValidos()
        {

        }
        [Test]
        public void ObterPromocaoDtoAtivas_DeveRetornarListaVazia_QuandoNaoExistemPromocoesAtivas()
        {

        }
        [Test]
        public void ObterPromocaoDtoInativas_DeveRetornarListaPromocaoDtoComDadosValidos()
        {

        }
        [Test]
        public void ObterPromocaoDtoInativas_DeveRetornarListaVazia_QuandoNaoExistemPromocoesInativas()
        {

        }
        [Test]
        public void ObterPromocaoDtoPorId_DeveRetornarPromocaoDtoComDadosValidos()
        {

        }
        [Test]
        public void ObterPromocaoDtoPorNomePromocao_DeveRetornarPromocaoDtoComDadosValidos()
        {

        }
        [Test]
        public void ObterTodosPromocaoDto_DeveRetornarListaDePromocoes()
        {

        }
        [Test]
        public void ObterPromocaoDtoPorId_DeveLancarExcecao_SePromocaoNaoExistir()
        {

        }
        [Test]
        public void ObterPromocaoDtoPorNomePromocao_DeveLancarExcecao_SeNomePromocaoNaoExistir()
        {

        }
        [Test]
        public void ObterTodosPromocaoDto_DeveRetornarListaVazia_SeNaoExistiremPromocoes()
        {

        }
        [Test]
        public void CadastrarPromocao_DeveChamarCadastrarNoRepositorio_ComDadosValidos()
        {

        }
        [Test]
        public void CadastrarPromocao_DeveLancarExcecao_SeJogoNaoExistir()
        {

        }
        [Test]
        public void AlterarPromocao_DeveAlterarDadosCorretamente()
        {

        }
        [Test]
        public void AlterarPromocao_DeveLancarExcecao_SePromocaoNaoExistir()
        {

        }
        [Test]
        public void DeletarPromocao_DeveChamarRepositorioComId()
        {

        }
        [Test]
        public void DeletarPromocao_DeveLancarExcecao_QuandoJogoNaoExiste()
        {

        }
    }
}
