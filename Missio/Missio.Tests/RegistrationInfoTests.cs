using System.Linq;
using Mission.Model.Exceptions;
using Mission.Model.Services;
using NUnit.Framework;

namespace Missio.Tests
{
    [TestFixture]
    public class RegistrationInfoTests
    {
        private RegistrationInfo _registrationInfo;

        [SetUp]
        public void SetUp()
        {
            _registrationInfo = new RegistrationInfo("", "", "");
        }

        [Test]
        [TestCase("")]
        [TestCase("A")]
        [TestCase("AA")]
        public void GetOfflineErrorMessages_UserNameIsTooShort_IsContainedInList(string username)
        {
            //Arrange
            _registrationInfo.UserName = username;
            //Act
            var errorMessages = _registrationInfo.GetOfflineErrorExceptions();
            //Assert
            Assert.That(errorMessages.Any(x => x is UserNameTooShortException));
        }

        [Test]
        [TestCase("haha")]
        [TestCase("GG")]
        [TestCase("WP")]
        public void GetOfflineErrorMessages_PasswordIsTooShort_IsContainedInList(string password)
        {
            //Arrange
            _registrationInfo.Password = password;
            //Act
            var errorMessages = _registrationInfo.GetOfflineErrorExceptions();
            //Assert
            Assert.That(errorMessages.Any(x => x is PasswordTooShortException));
        }

        [Test]
        [TestCase("", true)]
        [TestCase("AA", true)]
        [TestCase("BBB", false)]
        [TestCase("BBBBBB", false)]
        public void DoesUserNameHaveErrors_IsTooShort_ReturnsTrue(string username, bool expected)
        {
            //Arrange
            _registrationInfo.UserName = username;
            //Act
            var hasErrors = _registrationInfo.DoesUserNameHaveErrors();
            //Assert
            Assert.AreEqual(expected, hasErrors);
        }

        [Test]
        [TestCase("", true)]
        [TestCase("AA", true)]
        [TestCase("BBB", true)]
        [TestCase("BBBBB", false)]
        public void DoesPasswordHaveErrors_IsTooShort_ReturnsTrue(string password, bool expected)
        {
            //Arrange
            _registrationInfo.Password = password;
            //Act
            var hasErrors = _registrationInfo.DoesPasswordHaveErrors();
            //Assert
            Assert.AreEqual(expected, hasErrors);
        }

        [Test]
        public void UserName_IsNull_ReturnsEmptyString()
        {
            //Arrange
            _registrationInfo.UserName = null;
            //Act
            var result = _registrationInfo.UserName;
            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void Password_IsNull_ReturnsEmptyString()
        {
            //Arrange
            _registrationInfo.Password = null;
            //Act
            var result = _registrationInfo.Password;
            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void Email_IsNull_ReturnsEmptyString()
        {
            //Arrange
            _registrationInfo.Email = null;
            //Act
            var result = _registrationInfo.Email;
            //Assert
            Assert.IsEmpty(result);
        }
    }
}