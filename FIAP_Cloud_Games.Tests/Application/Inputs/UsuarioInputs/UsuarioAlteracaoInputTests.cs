using System.ComponentModel.DataAnnotations;
using Application.Input.UsuarioInput;
using Bogus;
using Bogus.DataSets;
using Domain.Entity;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace FIAP_Cloud_Games.Tests.Application.Inputs.UsuarioInputs
{
    [TestFixture]
    public class UsuarioAlteracaoInputTests
    {
        private Faker _faker;

        
        [SetUp]
        public void Setup()
        {
            _faker = new Faker("pt_BR");
        }
        private string GerarSenhaSegura(Internet internet)
        {

            var r = internet.Random;

            var number = r.Replace("#");                  
            var letter = r.Replace("?");                  
            var lowerLetter = letter.ToLower();           
            var symbol = r.Char((char)33, (char)47);      

            var baseLength = number.Length + letter.Length + lowerLetter.Length + 1;
            var minPaddingLength = 8 - baseLength;
            var extraPadding = r.String2(r.Number(minPaddingLength, minPaddingLength + 4));

            return new string(r.Shuffle(number + letter + lowerLetter + symbol + extraPadding).ToArray());
        }
        [Test]
        public void UsuarioAlteracaoInput_Valido_NaoDeveLancarExcecao()
        {
            var input = new UsuarioAlteracaoInput
            {
                Id = 1,
                Nome = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Senha = GerarSenhaSegura(_faker.Internet),
                Tipo = TipoUsuario.Padrao
            };

            var context = new ValidationContext(input);

            Assert.DoesNotThrow(() => Validator.ValidateObject(input, context, true));
        }
        [Test]
        public void UsuarioAlteracaoInput_EmailInvalido_DeveLancarValidationException()
        {
            var input = new UsuarioAlteracaoInput
            {
                Id = 1,
                Nome = _faker.Name.FullName(),
                Email = "email_invalido",
                Senha = GerarSenhaSegura(_faker.Internet),
                Tipo = TipoUsuario.Padrao
            };

            var context = new ValidationContext(input);

            Assert.Throws<ValidationException>(() => Validator.ValidateObject(input, context, true));
        }
        [Test]
        public void UsuarioAlteracaoInput_SenhaCurta_DeveLancarValidationException()
        {
            var input = new UsuarioAlteracaoInput
            {
                Id = 1,
                Nome = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Senha = "123",
                Tipo = TipoUsuario.Padrao
            };

            var context = new ValidationContext(input);

            Assert.Throws<ValidationException>(() => Validator.ValidateObject(input, context, true));
        }
    }
}
