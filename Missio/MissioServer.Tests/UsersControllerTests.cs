using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Missio.Registration;
using Missio.Users;
using NUnit.Framework;
using MissioServer.Controllers;
using MissioServer.Services;
using MissioServer.Services.Services;
using NSubstitute;
using StringResources;

namespace MissioServer.Tests
{
    [TestFixture] 
    public class UsersControllerTests
    {
        private static MissioContext MakeMissioContext()
        {
            var databaseOptions = new DbContextOptionsBuilder<MissioContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var passwordHasher = Substitute.For<IPasswordHasher<User>>();
            passwordHasher.HashPassword("ElPass").Returns("GrecoHashedPassword");
            var missioContext = new MissioContext(databaseOptions, passwordHasher, Substitute.For<IWebClientService>());
            missioContext.Database.EnsureCreated();
            return missioContext;
        }

        private static IPasswordHasher<User> MakePasswordServiceGivenPassword(string hashedPassword, string correctPassword)
        {
            var passwordService = Substitute.For<IPasswordHasher<User>>();
            passwordService.VerifyHashedPassword(hashedPassword, correctPassword).Returns(PasswordVerificationResult.Success);
            return passwordService;
        }

        private static UsersController MakeUsersController(MissioContext missioContext, IPasswordHasher<User> passwordService, IRegisterUserService registerUserService = null)
        {
            if(registerUserService == null)
                registerUserService = Substitute.For<IRegisterUserService>();
            return new UsersController(missioContext, passwordService, registerUserService);
        }

        [Test]
        public async Task GetUserIfValid_InvalidUserName_ReturnsUserNameDoesNotExist()
        {
            var usersController = MakeUsersController(MakeMissioContext(), Substitute.For<IPasswordHasher<User>>());

            var result = (ObjectResult)(await usersController.GetUserIfUserValid("NonExistingUsername", "")).Result;

            Assert.AreEqual(401, result.StatusCode);
            Assert.AreEqual(AppResources.InvalidUserName, result.Value);
        }

        [Test]
        public async Task IsUserValid_InvalidPassword_ReturnsIncorrectPassword()
        {
            var usersController = MakeUsersController(MakeMissioContext(), Substitute.For<IPasswordHasher<User>>());

            var result = (ObjectResult) (await usersController.GetUserIfUserValid("Francisco Greco", "")).Result;

            Assert.AreEqual(401, result.StatusCode);
            Assert.AreEqual(AppResources.InvalidPassword, result.Value);
        }

        [Test]
        public async Task IsUserValid_DataIsValid_ReturnsSuccessfulLogin()
        {
            var correctPassword = "El Pass";
            var passwordService = MakePasswordServiceGivenPassword("GrecoHashedPassword", correctPassword);
            var usersController = MakeUsersController(MakeMissioContext(), passwordService);

            var result = await usersController.GetUserIfUserValid("Francisco Greco", correctPassword);

            Assert.AreEqual("Francisco Greco", result.Value.UserName);
        }

        [Test]
        public async Task RegisterUser_GivenInfo_CallsRegisterService()
        {
            var registration = new RegistrationDTO();
            var fakeRegistrationService = Substitute.For<IRegisterUserService>();
            var usersController = MakeUsersController(MakeMissioContext(), Substitute.For<IPasswordHasher<User>>(), fakeRegistrationService);

            await usersController.RegisterUser(registration);

            await fakeRegistrationService.Received().RegisterUser(registration);
        }

        [Test]
        public async Task RegisterUser_ExceptionThrown_ReturnsBadRequest()
        {
            var registration = new RegistrationDTO();
            var fakeRegistrationService = Substitute.For<IRegisterUserService>();
            var errors = new List<string> { "An error message " };
            fakeRegistrationService.When(x => x.RegisterUser(registration)).Do(x => throw new UserRegistrationException(errors));
            var usersController = MakeUsersController(MakeMissioContext(), Substitute.For<IPasswordHasher<User>>(), fakeRegistrationService);

            var result = (ObjectResult) await usersController.RegisterUser(registration);

            Assert.AreEqual(errors, result.Value);
        }
    }
}
