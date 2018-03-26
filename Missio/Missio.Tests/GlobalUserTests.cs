using System;
using Mission.Model.Data;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    public class GlobalUserTests
    {
        private GlobalUser GlobalUser;

        [SetUp]
        public void SetUp()
        {
            GlobalUser = new GlobalUser();
        }

        [Test]
        public void GetUser_NoUserLoggedIn_ThrowsException()
        {
            //Arrange

            //Act and assert
            // ReSharper disable once UnusedVariable
            Assert.Throws<InvalidOperationException>(() => { var User =  GlobalUser.LoggedInUser; } );
        }

        [Test]
        public void SetUser_GivenUser_SetsUser()
        {
            //Arrange
            User user = new User("Some user ", "Pass");
            //Act
            GlobalUser.LoggedInUser = user;
            //Assert
            Assert.AreEqual(user, GlobalUser.LoggedInUser);
        }

        [Test]
        public void SetUser_GivenUser_FiresOnUserLoggedIn()
        {
            //Arrange
            bool wasEventRaised = false;
            GlobalUser.OnUserLoggedIn += () => wasEventRaised = true;
            User user = new User("Some user ", "Pass");
            //Act
            GlobalUser.LoggedInUser = user;
            //Assert
            Assert.IsTrue(wasEventRaised);
        }
    }
}