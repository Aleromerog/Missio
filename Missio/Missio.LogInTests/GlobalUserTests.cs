using System;
using Missio.LogIn;
using Missio.Users;
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
            // ReSharper disable once UnusedVariable
            Assert.Throws<InvalidOperationException>(() => { var user =  _globalUser.LoggedInUser; } );
        }

        [Test]
        public void SetUser_GivenUser_SetsUser()
        {
            var user = new User("Some user ", "Pass");

            _globalUser.LoggedInUser = user;

            Assert.AreEqual(user, _globalUser.LoggedInUser);
        }
    }
}