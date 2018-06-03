using System;
using Mission.Model.Data;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
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
            User user = new User("Some user ", "Pass");
            //Act
            _globalUser.LoggedInUser = user;
            //Assert
            Assert.AreEqual(user, _globalUser.LoggedInUser);
        }

        [Test]
        public void SetUser_GivenUser_FiresOnUserLoggedIn()
        {
            //Arrange
            bool wasEventRaised = false;
            _globalUser.OnUserLoggedIn += () => wasEventRaised = true;
            User user = new User("Some user ", "Pass");
            //Act
            _globalUser.LoggedInUser = user;
            //Assert
            Assert.IsTrue(wasEventRaised);
        }
    }
}