using System;
using Missio.LogIn;
using NUnit.Framework;

namespace Missio.LogInTests
{
    public class GlobalUserTests
    {
        private GlobalUser _globalUser;

        [SetUp]
        public void SetUp()
        {
            _globalUser = new GlobalUser();
        }

        [Test]
        public void GetUser_NoUserLoggedIn_ThrowsException()
        {
            //Arrange

            //Act and assert
            // ReSharper disable once UnusedVariable
            Assert.Throws<InvalidOperationException>(() => { var user =  _globalUser.LoggedInUser; } );
        }

        [Test]
        public void SetUser_GivenUser_SetsUser()
        {
            //Arrange
            var user = new User.User("Some user ", "Pass");
            //Act
            _globalUser.LoggedInUser = user;
            //Assert
            Assert.AreEqual(user, _globalUser.LoggedInUser);
        }

        [Test]
        public void SetUser_GivenUser_FiresOnUserLoggedIn()
        {
            //Arrange
            var wasEventRaised = false;
            _globalUser.OnUserLoggedIn += () => wasEventRaised = true;
            var user = new User.User("Some user ", "Pass");
            //Act
            _globalUser.LoggedInUser = user;
            //Assert
            Assert.IsTrue(wasEventRaised);
        }
    }
}