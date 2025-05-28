using System.ComponentModel.DataAnnotations;
using Application.Input.JogoInput;
using Bogus;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace FIAP_Cloud_Games.Tests.Application.Inputs.JogoInputs
{
    internal class JogoAlteracaoInputTests
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
        public void JogoAlteracaoInput_DeveInstanciarCorretamente()
        {
            var input = new JogoAlteracaoInput { 
                    Id = _faker.Random.Number(), 
                    Titulo = _faker.Name.FullName(), 
                    Produtora = _faker.Company.CompanyName() 
            };
            var results = ValidateModel(input);
            var context = new ValidationContext(input);
            Assert.DoesNotThrow(() => Validator.ValidateObject(input, context, true));
        }
        [Test]
        public void JogoAlteracaoInput_DeveRetornarErro_SeTituloForNulo()
        {
            var input = new JogoAlteracaoInput { 
                Id = 1, 
                Titulo = null!, 
                Produtora = _faker.Company.CompanyName() 
            };
            var results = ValidateModel(input);
            Assert.That(results, Has.Exactly(1).Matches<ValidationResult>(r => r.ErrorMessage == "Titulo é obrigatório."));
        }
        [Test]
        public void JogoAlteracaoInput_DeveRetornarErro_SeProdutoraForNulo()
        {
            var input = new JogoAlteracaoInput { 
                Id = 1, 
                Titulo = _faker.Name.FullName(), 
                Produtora = null! 
            };
            var results = ValidateModel(input);
            Assert.That(results, Has.Exactly(1).Matches<ValidationResult>(r => r.ErrorMessage == "Produtora é obrigatório."));
        }
    }
}
