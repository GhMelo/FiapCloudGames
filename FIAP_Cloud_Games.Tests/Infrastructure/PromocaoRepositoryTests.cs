using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Domain.Interfaces.IRepository;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP_Cloud_Games.Tests.Infrastructure
{
    [TestFixture]
    internal class PromocaoRepositoryTests
    {
        private readonly Faker _faker = new();
        private ApplicationDbContext _context; 

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

        }

        [Test]
        public void CadastrarPromocao_DeveAdicionarPromocaoAoBanco()
        {

        }
        [Test]
        public void CadastrarPromocao_DeveLancarExcecao_QuandoCamposObrigatoriosNaoForemPreenchidos()
        {

        }
        [Test]
        public void ObterPorId_DeveRetornarPromocao()
        {

        }
        [Test]
        public void ObterPorId_DeveRetornarNull_QuandoIdNaoExiste()
        {

        }
        [Test]
        public void ObterTodos_DeveRetornarTodasPromocoes()
        {

        }
        [Test]
        public void ObterPorNomePromocao_DeveRetornarPromocao_QuandoPromocaoExiste()
        {

        }
        [Test]
        public void ObterPorNomePromocao_DeveRetornarNull_QuandoNomePromocaoNaoExiste()
        {

        }
        [Test]
        public void Alterar_DeveAtualizarDadosDaPromocao()
        {

        }
        [Test]
        public void Alterar_DeveLancarExcecao_SePromocaoNaoExiste()
        {

        }
        [Test]
        public void AlterarPromocao_DeveLancarExcecao_QuandoIdForInvalido()
        {

        }
        [Test]
        public void Deletar_DeveRemoverPromocaoDoBanco()
        {

        }
        [Test]
        public void Deletar_DeveLancarExcecao_SePromocaoNaoExiste()
        {

        }
    }
}
