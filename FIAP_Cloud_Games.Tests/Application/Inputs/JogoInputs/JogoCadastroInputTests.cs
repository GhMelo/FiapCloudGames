using System.ComponentModel.DataAnnotations;
using Application.Input.JogoInput;
using Bogus;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace FIAP_Cloud_Games.Tests.Application.Inputs.JogoInputs
{
    internal class JogoCadastroInputTests
    {
        private readonly Faker _faker = new();
        private List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
        [Test]
        public void JogoCadastroInput_DeveInstanciarCorretamente()
        {
            var input = new JogoCadastroInput { 
                Titulo = _faker.Name.FullName(),
                Produtora = _faker.Company.CompanyName() 
            };
            var results = ValidateModel(input);
            var context = new ValidationContext(input);
            Assert.DoesNotThrow(() => Validator.ValidateObject(input, context, true));
        }
        [Test]
        public void JogoCadastroInput_DeveRetornarErro_SeTituloForNulo()
        {
            var input = new JogoCadastroInput { 
                Titulo = null!, 
                Produtora = _faker.Company.CompanyName() 
            };
            var results = ValidateModel(input);
            Assert.That(results, Has.Exactly(1).Matches<ValidationResult>(r => r.ErrorMessage == "Titulo é obrigatório."));
        }
        [Test]
        public void JogoCadastroInput_DeveRetornarErro_SeProdutoraForNulo()
        {
            var input = new JogoCadastroInput { 
                Titulo = _faker.Name.FullName(), 
                Produtora = null! 
            };
            var results = ValidateModel(input);
            Assert.That(results, Has.Exactly(1).Matches<ValidationResult>(r => r.ErrorMessage == "Produtora é obrigatório."));
        }


    }
}
