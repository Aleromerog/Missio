using System.Linq;
using Missio.Registration;
using NUnit.Framework;

namespace Missio.RegistrationTests
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
            _registrationInfo.UserName = username;

            var errorMessages = _registrationInfo.GetOfflineErrorExceptions();

            Assert.That(errorMessages.Any(x => x is UserNameTooShortException));
        }

        [Test]
        [TestCase("haha")]
        [TestCase("GG")]
        [TestCase("WP")]
        public void GetOfflineErrorMessages_PasswordIsTooShort_IsContainedInList(string password)
        {
            _registrationInfo.Password = password;

            var errorMessages = _registrationInfo.GetOfflineErrorExceptions();

            Assert.That(errorMessages.Any(x => x is PasswordTooShortException));
        }

        [Test]
        [TestCase("", true)]
        [TestCase("AA", true)]
        [TestCase("BBB", false)]
        [TestCase("BBBBBB", false)]
        public void DoesUserNameHaveErrors_IsTooShort_ReturnsTrue(string username, bool expected)
        {
            _registrationInfo.UserName = username;

            var hasErrors = _registrationInfo.DoesUserNameHaveErrors();

            Assert.AreEqual(expected, hasErrors);
        }

        [Test]
        [TestCase("", true)]
        [TestCase("AA", true)]
        [TestCase("BBB", true)]
        [TestCase("BBBBB", false)]
        public void DoesPasswordHaveErrors_IsTooShort_ReturnsTrue(string password, bool expected)
        {
            _registrationInfo.Password = password;

            var hasErrors = _registrationInfo.DoesPasswordHaveErrors();

            Assert.AreEqual(expected, hasErrors);
        }

        [Test]
        public void UserName_IsNull_ReturnsEmptyString()
        {
            _registrationInfo.UserName = null;

            var result = _registrationInfo.UserName;

            Assert.IsEmpty(result);
        }

        [Test]
        public void Password_IsNull_ReturnsEmptyString()
        {
            _registrationInfo.Password = null;

            var result = _registrationInfo.Password;

            Assert.IsEmpty(result);
        }

        [Test]
        public void Email_IsNull_ReturnsEmptyString()
        {
            _registrationInfo.Email = null;

            var result = _registrationInfo.Email;

            Assert.IsEmpty(result);
        }
    }
}