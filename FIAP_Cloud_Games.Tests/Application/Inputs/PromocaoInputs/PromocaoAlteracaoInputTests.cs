using System.ComponentModel.DataAnnotations;
using Bogus;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace FIAP_Cloud_Games.Tests.Application.Inputs.PromocaoInputs
{
    internal class PromocaoAlteracaoInputTests
    {
        private Faker _faker = new();

        [SetUp]
        public void Setup()
        {
            _faker = new Faker();
        }
        private List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
        [Test]
        public void PromocaoAlteracaoInput_DeveRetornarErro_SeNomePromocaoForNulo()
        {

        }
        [Test]
        public void PromocaoAlteracaoInput_DeveRetornarErro_SePorcentagemForNulo()
        {

        }
        [Test]
        public void PromocaoAlteracaoInput_DeveRetornarErro_SePorcentagemForInvalida()
        {

        }
        [Test]
        public void PromocaoAlteracaoInput_DeveRetornarErro_SePromocaoAtivaForNulo()
        {

        }
        [Test]
        public void PromocaoAlteracaoInput_DeveRetornarErro_SeJogoIdForNulo()
        {

        }
    }
}
