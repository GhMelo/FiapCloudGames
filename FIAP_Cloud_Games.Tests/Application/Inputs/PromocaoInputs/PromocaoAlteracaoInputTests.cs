using System.ComponentModel.DataAnnotations;
using Application.Input.PromocaoInput;
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
        public void PromocaoAlteracaoInput_DeveInstanciarCorretamente()
        {
            var input = new PromocaoAlteracaoInput
            {
                PromocaoId = _faker.Random.Int(1, 999),
                NomePromocao = _faker.Name.FullName(),
                Porcentagem = _faker.Random.Int(1, 100),
                PromocaoAtiva = _faker.Random.Bool(),
            };
            var results = ValidateModel(input);
            var context = new ValidationContext(input);
            Assert.DoesNotThrow(() => Validator.ValidateObject(input, context, true));
        }
        [Test]
        public void PromocaoAlteracaoInput_DeveRetornarErro_SeNomePromocaoForNulo()
        {
            var input = new PromocaoAlteracaoInput
            {
                PromocaoId = _faker.Random.Int(1, 999),
                NomePromocao = null!,
                Porcentagem = _faker.Random.Int(1, 100),
                PromocaoAtiva = _faker.Random.Bool(),
            };
            var results = ValidateModel(input);
            Assert.That(results, Has.Exactly(1).Matches<ValidationResult>(r => r.ErrorMessage == "NomePromocao é obrigatório."));
        }
        [Test]
        public void PromocaoAlteracaoInput_DeveRetornarErro_SePorcentagemForInvalida()
        {
            var input = new PromocaoAlteracaoInput
            {
                PromocaoId = _faker.Random.Int(1, 999),
                NomePromocao = _faker.Name.FullName(),
                Porcentagem = 0,
                PromocaoAtiva = _faker.Random.Bool(),
            };
            var results = ValidateModel(input);
            Assert.That(results, Has.Exactly(1).Matches<ValidationResult>(r => r.ErrorMessage == "A porcentagem deve ser entre 1 e 100"));
        }
    }
}
