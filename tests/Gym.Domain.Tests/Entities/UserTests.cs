using Gym.Domain.Entities;

namespace Gym.Domain.Tests.Entities;

[TestFixture]
public class UserTests
{
    public class Constructor
    {
        [Test]
        public void Should_create_user_successfully()
        {
            var expectedFirstName = "Lian";
            var expectedLastName = "Souza Miranda";
            var expectedEmail = "lian@gmail.com";
            var expectedPassword = "1234";

            var user = new User(expectedFirstName, expectedLastName, expectedEmail, expectedPassword);

            Assert.That(user.FirstName, Is.EqualTo(expectedFirstName));
            Assert.That(user.LastName, Is.EqualTo(expectedLastName));
            Assert.That(user.Email, Is.EqualTo(expectedEmail));
            Assert.That(user.PasswordHash, Is.EqualTo(expectedPassword));
        }

        [TestCase("", "Souza", "lian@gmail.com", "1234")]
        [TestCase("Lian", "", "lian@gmail.com", "1234")]
        [TestCase("Lian", "Souza", "", "1234")]
        [TestCase("Lian", "Souza", "lian@gmail.com", "")]
        public void Should_throw_error_when_creating_user_with_empty_parameter(string firstName, string lastName,
            string email, string password)
        {
            var error = Assert.Throws<ArgumentException>(() => new User(firstName, lastName, email, password));
            
            Assert.That(error.Message,
                Does.Contain("The value cannot be an empty string or composed entirely of whitespace."));
        }
    }
}

