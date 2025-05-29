using System.ComponentModel.DataAnnotations;
using Application.Input.PromocaoInput;
using Bogus;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace FIAP_Cloud_Games.Tests.Application.Inputs.PromocaoInputs
{
    internal class PromocaoCadastroInputTests
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
        public void PromocaoCadastroInput_DeveInstanciarCorretamente()
        {
            var input = new PromocaoCadastroInput
            {
                NomePromocao = _faker.Name.FullName(),
                Porcentagem = _faker.Random.Int(1,100),
                PromocaoAtiva = _faker.Random.Bool(),
                JogoId = _faker.Random.Int(1, 999),
            };
            var results = ValidateModel(input);
            var context = new ValidationContext(input);
            Assert.DoesNotThrow(() => Validator.ValidateObject(input, context, true));
        }
        [Test]
        public void PromocaoCadastroInput_DeveRetornarErro_SeNomePromocaoForNulo()
        {
            var input = new PromocaoCadastroInput
            {
                NomePromocao = null!,
                Porcentagem = _faker.Random.Int(1, 100),
                PromocaoAtiva = _faker.Random.Bool(),
                JogoId = _faker.Random.Int(1, 999),
            };
            var results = ValidateModel(input);
            Assert.That(results, Has.Exactly(1).Matches<ValidationResult>(r => r.ErrorMessage == "NomePromocao é obrigatório."));
        }
        [Test]
        public void PromocaoCadastroInput_DeveRetornarErro_SePorcentagemForInvalida()
        {
            var input = new PromocaoCadastroInput
            {
                NomePromocao = _faker.Name.FullName(),
                Porcentagem = 0,
                PromocaoAtiva = _faker.Random.Bool(),
                JogoId = _faker.Random.Int(1, 999),
            };
            var results = ValidateModel(input);
            Assert.That(results, Has.Exactly(1).Matches<ValidationResult>(r => r.ErrorMessage == "A porcentagem deve ser entre 1 e 100"));
        }
    }
}
