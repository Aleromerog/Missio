using Missio.ExternalDatabase;
using Missio.LocalDatabase;
using Missio.LogIn;
using Missio.Registration;
using NSubstitute;
using NUnit.Framework;

namespace Missio.Tests
{
    [TestFixture]
    public class ExternalDatabaseTests
    {
        private ExternalDatabase.ExternalDatabase _externalDatabase;
        private IGetChildQuery _fakeGetChildQuery;

        private static object[] _incorrectUserNames =
        {
            new User.User("Incorrect username 1", ""),
            new User.User("Incorrect username 2", ""),
        };

        private static object[] _incorrectUserPasswords =
        {
            new User.User(LocalUserDatabase.ValidUsers[0].UserName, "Invalid pass"),
            new User.User(LocalUserDatabase.ValidUsers[1].UserName, "Invalid pass"),
        };

        [SetUp]
        public void SetUp()
        {
            _fakeGetChildQuery = Substitute.For<IGetChildQuery>();
            _externalDatabase = new ExternalDatabase.ExternalDatabase(_fakeGetChildQuery);
        }

        [Test]
        [TestCaseSource(nameof(_incorrectUserNames))]
        public void ValidateUser_IncorrectUserName_ThrowsException(User.User incorrectUser)
        {
            //Arrange
            
            //Act and assert
            Assert.Throws<InvalidUserNameException>(() => _externalDatabase.ValidateUser(incorrectUser));
        }

        [Test]
        [TestCaseSource(nameof(_incorrectUserPasswords))]
        public void ValidateUser_IncorrectPassword_ThrowsException(User.User incorrectUser)
        {
            //Arrange

            //Act and assert
            Assert.Throws<InvalidPasswordException>(() => _externalDatabase.ValidateUser(incorrectUser));
        }

        [Test]
        [TestCase("Jorge Romero", true)]
        [TestCase("Ultron", false)]
        [TestCase("Thanos", false)]
        public void DoesUserExist_GivenUsername_ReturnsIfUserExists(string userName, bool doesUserExist)
        {
            //Arrange
            
            //Act
            var result = _externalDatabase.DoesUserExist(userName);
            //Assert
            Assert.AreEqual(doesUserExist, result);
        }

        [Test]
        [TestCase("New user name", "Some pass", "someEmail")]
        [TestCase("New another user name", "Another pass", "anotherEmail")]
        public void RegisterUser_GivenData_RegistersUser(string userName, string password, string email)
        {
            //Arrange
            
            //Act
            _externalDatabase.RegisterUser(new RegistrationInfo(userName, password, email));
            //Assert
            Assert.IsTrue(_externalDatabase.DoesUserExist(userName));
        }
    }
}