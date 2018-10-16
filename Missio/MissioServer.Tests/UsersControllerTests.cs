using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Missio.LogIn;
using Missio.Users;
using NUnit.Framework;
using MissioServer.Controllers;

namespace MissioServer.Tests
{
    [TestFixture] 
    public class UsersControllerTests
    {
        private static MissioContext MakeMissioContext()
        {
            var databaseOptions = new DbContextOptionsBuilder<MissioContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var misioContext = new MissioContext(databaseOptions);
            return misioContext;
        }

        private static UsersController MakeUsersController(MissioContext missioContext)
        {
            return new UsersController(missioContext);
        }

        [Test]
        public async Task IsUserValid_InvalidUserName_ReturnsUserNameDoesNotExist()
        {
            var missioContext = MakeMissioContext();
            var usersController = MakeUsersController(missioContext);

            var result = await usersController.IsUserValid("NonExistingUsername", "");

            Assert.AreEqual(LogInStatus.InvalidUserName, result);
        }

        [Test]
        public async Task IsUserValid_InvalidPassword_ReturnsIncorrectPassword()
        {
            var missioContext = MakeMissioContext();
            missioContext.Users.Add(new User("ValidName", "Password", ""));
            missioContext.SaveChanges();
            var usersController = MakeUsersController(missioContext);

            var result = await usersController.IsUserValid("ValidName", "");

            Assert.AreEqual(LogInStatus.InvalidPassword, result);
        }

        [Test]
        public async Task IsUserValid_DataIsValid_ReturnsSuccessfulLogin()
        {
            var missioContext = MakeMissioContext();
            missioContext.Users.Add(new User("ValidName", "Password", ""));
            missioContext.SaveChanges();
            var usersController = MakeUsersController(missioContext);

            var result = await usersController.IsUserValid("ValidName", "Password");

            Assert.AreEqual(LogInStatus.Successful, result);
        }
    }
}
