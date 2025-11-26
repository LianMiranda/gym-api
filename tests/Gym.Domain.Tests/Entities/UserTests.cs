using Gym.Domain.Entities;

namespace Gym.Domain.Tests.Entities;

[TestFixture]
public class UserTests
{
    public class Constructor
    {
        [Test]
        public void Deve_criar_o_usuario_com_sucesso()
        {
            var expectedFirstName = "Lian";
            var expectedLastName = "Souza Miranda";
            var expectedEmail = "lian@gmail.com";
            var expectedPassword = "1234";

            var user = new User(expectedFirstName, expectedLastName, expectedEmail, expectedPassword);

            Assert.That(expectedFirstName, Is.EqualTo(user.FirstName));
            Assert.That(expectedLastName, Is.EqualTo(user.LastName));
            Assert.That(expectedEmail, Is.EqualTo(user.Email));
            Assert.That(expectedPassword, Is.EqualTo(user.PasswordHash));
        }
        
        [TestCase("", "Souza", "lian@gmail.com", "1234")]
        [TestCase("Lian", "", "lian@gmail.com", "1234")]
        [TestCase("Lian", "Souza", "", "1234")]
        [TestCase("Lian", "Souza", "lian@gmail.com", "")]
        public void Deve_retornar_erro_ao_criar_usuario_com_parametro_vazio(string firstName, string lastName, string email, string password)
        {
            var error = Assert.Throws<ArgumentException>(() => new User(firstName, lastName, email, password));
            Assert.That(error.Message,
                Does.Contain("The value cannot be an empty string or composed entirely of whitespace."));
        }
    }
    
    
}