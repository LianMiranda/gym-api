using Gym.Domain.Entities;

namespace Gym.Domain.Tests.Entities;

[TestFixture]
public class UserTests
{
    private const string EmptyOrWhitespaceErrorMessage  =
        "The value cannot be an empty string or composed entirely of whitespace.";

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

            Assert.That(error.Message, Does.Contain(EmptyOrWhitespaceErrorMessage));
        }

        [TestCase("   ", "Souza", "lian@gmail.com", "1234")]
        [TestCase("Lian", "   ", "lian@gmail.com", "1234")]
        [TestCase("Lian", "Souza", "   ", "1234")]
        [TestCase("Lian", "Souza", "lian@gmail.com", "   ")]
        public void Should_throw_error_when_creating_user_with_whitespace_parameter(
            string firstName, string lastName, string email, string password)
        {
            var error = Assert.Throws<ArgumentException>(() => new User(firstName, lastName, email, password)
            );

            Assert.That(error.Message, Does.Contain(EmptyOrWhitespaceErrorMessage));
        }
    }

    public class UpdateFirstName
    {
        private User _user = null!;

        [SetUp]
        public void SetUp()
        {
            _user = new User("Nome Teste", "Sobrenome Teste", "teste@gmail.com.br", "1234");
        }

        [Test]
        public void Should_update_firstName_successfully()
        {
            var oldUpdatedAt = _user.UpdatedAt;
            Thread.Sleep(5);

            var newName = "Novo nome";
            _user.UpdateFirstName(newName);

            Assert.That(_user.FirstName, Is.EqualTo(newName));
            Assert.That(_user.UpdatedAt, Is.GreaterThan(oldUpdatedAt));
        }

        [TestCase(" ")]
        [TestCase("")]
        public void Should_throw_error_when_updating_firstName_with_empty_parameter(string newName)
        {
            var error = Assert.Throws<ArgumentException>(() => _user.UpdateFirstName(newName));

            Assert.That(error.Message, Does.Contain(EmptyOrWhitespaceErrorMessage));
            Assert.That(error.ParamName, Is.EqualTo("newFirstName"));
        }
    }

    public class UpdateLastName
    {
        private User _user = null!;

        [SetUp]
        public void SetUp()
        {
            _user = new User("Nome Teste", "Sobrenome Teste", "teste@gmail.com.br", "1234");
        }

        [Test]
        public void Should_update_lastName_successfully()
        {
            var oldUpdatedAt = _user.UpdatedAt;
            Thread.Sleep(5);

            var newLastName = "Novo sobrenome";
            _user.UpdateLastName(newLastName);

            Assert.That(_user.LastName, Is.EqualTo(newLastName));
            Assert.That(_user.UpdatedAt, Is.GreaterThan(oldUpdatedAt));
        }

        [TestCase(" ")]
        [TestCase("")]
        public void Should_throw_error_when_updating_lastName_with_empty_parameter(string newLastName)
        {
            var error = Assert.Throws<ArgumentException>(() => _user.UpdateLastName(newLastName));

            Assert.That(error.Message, Does.Contain(EmptyOrWhitespaceErrorMessage));
            Assert.That(error.ParamName, Is.EqualTo("newLastName"));
        }
    }

    public class UpdateEmail
    {
        private User _user = null!;

        [SetUp]
        public void SetUp()
        {
            _user = new User("Nome Teste", "Sobrenome Teste", "teste@gmail.com.br", "1234");
        }

        [Test]
        public void Should_update_email_successfully()
        {
            var oldUpdatedAt = _user.UpdatedAt;
            Thread.Sleep(5);

            var newEmail = "lianmiranda@gmail.com";
            _user.UpdateEmail(newEmail);

            Assert.That(_user.Email, Is.EqualTo(newEmail));
            Assert.That(_user.UpdatedAt, Is.GreaterThan(oldUpdatedAt));
        }

        [TestCase(" ")]
        [TestCase("")]
        public void Should_throw_error_when_updating_email_with_empty_parameter(string newEmail)
        {
            var error = Assert.Throws<ArgumentException>(() => _user.UpdateEmail(newEmail));

            Assert.That(error.Message, Does.Contain(EmptyOrWhitespaceErrorMessage));
            Assert.That(error.ParamName, Is.EqualTo("newEmail"));
        }
    }

    public class UpdatePassword
    {
        private User _user = null!;

        [SetUp]
        public void SetUp()
        {
            _user = new User("Nome Teste", "Sobrenome Teste", "teste@gmail.com.br", "1234");
        }

        [Test]
        public void Should_update_password_successfully()
        {
            var oldUpdatedAt = _user.UpdatedAt;
            Thread.Sleep(5);

            var newPassword = "$2a$12$UnCg4PNoRkPBCQ.WsEegVu2T1sFTTv9FVyeXvvRYd3m.ZD3sT3GQe";
            _user.UpdatePasswordHash(newPassword);

            Assert.That(_user.PasswordHash, Is.EqualTo(newPassword));
            Assert.That(_user.UpdatedAt, Is.GreaterThan(oldUpdatedAt));
        }

        [TestCase(" ")]
        [TestCase("")]
        public void Should_throw_error_when_updating_password_with_empty_parameter(string newPassword)
        {
            var error = Assert.Throws<ArgumentException>(() => _user.UpdatePasswordHash(newPassword));

            Assert.That(error.Message, Does.Contain(EmptyOrWhitespaceErrorMessage));
            Assert.That(error.ParamName, Is.EqualTo("newPasswordHash"));
        }
    }
}