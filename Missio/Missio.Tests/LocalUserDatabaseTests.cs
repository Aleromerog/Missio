using Mission.Model.Data;
using Mission.Model.Exceptions;
using Mission.Model.LocalProviders;
using NUnit.Framework;

namespace Missio.Tests
{
    [TestFixture]
    public class LocalUserDatabaseTests
    {
        private LocalUserDatabase _localUserDatabase;

        private static object[] newUsers =
        {
            new User("New user 1", "New user 1 pass"), 
            new User("New user 2", "New user 2 pass"), 
        };


        private static object[] incorrectUserNames =
        {
            new User("Incorrect username 1", ""),
            new User("Incorrect username 2", ""),
        };

        private static object[] incorrectUserPasswords =
        {
            new User(LocalUserDatabase.ValidUsers[0].UserName, "Invalid pass"),
            new User(LocalUserDatabase.ValidUsers[1].UserName, "Invalid pass"),
        };

        [SetUp]
        public void SetUp()
        {
            _localUserDatabase = new LocalUserDatabase();
        }

        [Test]
        [TestCaseSource(nameof(newUsers))]
        public void AddUser_GivenUser_AddsUser(User newUser)
        {
            //Arrange
            
            //Act
            _localUserDatabase.AddUser(newUser);
            //Assert
            Assert.DoesNotThrow(() => _localUserDatabase.ValidateUser(newUser));
        }

        [Test]
        [TestCaseSource(nameof(incorrectUserNames))]
        public void ValidateUser_IncorrectUserName_ThrowsException(User incorrectUser)
        {
            //Arrange
            
            //Act and assert
            Assert.Throws<InvalidUserNameException>(() => _localUserDatabase.ValidateUser(incorrectUser));
        }

        [Test]
        [TestCaseSource(nameof(incorrectUserPasswords))]
        public void ValidateUser_IncorrectPassword_ThrowsException(User incorrectUser)
        {
            //Arrange

            //Act and assert
            Assert.Throws<InvalidPasswordException>(() => _localUserDatabase.ValidateUser(incorrectUser));
        }
    }
}