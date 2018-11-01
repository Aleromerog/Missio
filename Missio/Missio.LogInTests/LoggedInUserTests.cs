using Missio.Users;
using NUnit.Framework;

namespace Missio.LogInTests
{
    [TestFixture]
    public class LoggedInUserTests
    {
        private LoggedInUser _loggedInUser;

        [SetUp]
        public void SetUp()
        {
            _loggedInUser = new LoggedInUser();
        }

        [Test]
        public void SetUser_GivenUser_SetsUser()
        {
            var userName = "A name";
            _loggedInUser.UserName = userName;

            Assert.AreEqual(userName, _loggedInUser.UserName);
        }

        [Test]
        public void SetPassword_GivenPassword_SetsPassword()
        {
            var password = "A password";
            _loggedInUser.Password = password;

            Assert.AreEqual(password, _loggedInUser.Password);
        }
    }
}