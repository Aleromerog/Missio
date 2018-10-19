using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Missio.Users;
using NUnit.Framework;
using MissioServer.Controllers;
using StringResources;

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
        public async Task GetUserIfValid_InvalidUserName_ReturnsUserNameDoesNotExist()
        {
            var missioContext = MakeMissioContext();
            var usersController = MakeUsersController(missioContext);

            var result = (ObjectResult)(await usersController.IsUserValid("NonExistingUsername", "")).Result;

            Assert.AreEqual(401, result.StatusCode);
            Assert.AreEqual(AppResources.InvalidUserName, result.Value);
        }

        [Test]
        public async Task IsUserValid_InvalidPassword_ReturnsIncorrectPassword()
        {
            var missioContext = MakeMissioContext();
            missioContext.Users.Add(new User("ValidName", "Password"));
            missioContext.SaveChanges();
            var usersController = MakeUsersController(missioContext);

            var result = (ObjectResult) (await usersController.IsUserValid("ValidName", "")).Result;

            Assert.AreEqual(401, result.StatusCode);
            Assert.AreEqual(AppResources.InvalidPassword, result.Value);
        }

        [Test]
        public async Task IsUserValid_DataIsValid_ReturnsSuccessfulLogin()
        {
            var missioContext = MakeMissioContext();
            var user = new User("ValidName", "Password");
            missioContext.Users.Add(user);
            missioContext.SaveChanges();
            var usersController = MakeUsersController(missioContext);

            var result = await usersController.IsUserValid("ValidName", "Password");

            Assert.AreEqual(user, result.Value);
        }
    }
}
