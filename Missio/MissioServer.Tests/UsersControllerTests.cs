using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Missio.Registration;
using Missio.Users;
using NUnit.Framework;
using MissioServer.Controllers;
using MissioServer.Services;
using MissioServer.Services.Services;
using StringResources;

namespace MissioServer.Tests
{
    [TestFixture] 
    public class UsersControllerTests
    {
        private static UsersController MakeUsersController(MissioContext missioContext = null)
        {
            if(missioContext == null)
                missioContext = Utils.MakeMissioContext();
            var passwordService = new MockPasswordService();
            var userService = new UsersService(missioContext, passwordService);
            var registerUserService = new RegisterUserService(missioContext, passwordService);
            return new UsersController(userService, registerUserService);
        }

        [Test]
        public async Task ValidateUser_InvalidUserName_ReturnsErrorMessage()
        {
            var usersController = MakeUsersController();

            var result = (ObjectResult) await usersController.ValidateUser("Not valid name", "");

            Assert.AreEqual(401, result.StatusCode);
            Assert.AreEqual(AppResources.InvalidUserName, result.Value);
        }

        [Test]
        public async Task ValidateUser_InvalidPassword_ReturnsErrorMessage()
        {
            var usersController = MakeUsersController();

            var result = (ObjectResult)await usersController.ValidateUser("Francisco Greco", "");

            Assert.AreEqual(401, result.StatusCode);
            Assert.AreEqual(AppResources.InvalidPassword, result.Value);
        }

        [Test]
        public async Task ValidateUser_UserIsValid_ReturnsOk()
        {
            var usersController = MakeUsersController();

            Assert.IsInstanceOf<OkResult>(await usersController.ValidateUser("Francisco Greco", "ElPass"));
        }

        [Test]
        public async Task RegisterUser_UserNameIsTooShort_ThrowsException()
        {
            var usersController = MakeUsersController();
            var registration = new CreateUserDTO("ABC", "Password", "someEmail@gmail.com");

            var result = (ObjectResult) await usersController.RegisterUser(registration);

            Assert.Contains(AppResources.UserNameTooShortMessage, (List<string>) result.Value);
        }

        [Test]
        public void RegisterUser_UserNameAlreadyInUse_ThrowsException()
        {
            var usersController = MakeUsersController();
            var registration = new CreateUserDTO("Francisco Greco", "Password", "someEmail@gmail.com");

            var exception = Assert.ThrowsAsync<UserRegistrationException>(() => usersController.RegisterUser(registration));

            Assert.Contains(AppResources.UserNameAlreadyInUseMessage, exception.ErrorMessages);
        }

        [Test]
        public void RegisterUser_PasswordIsTooShort_ThrowsException()
        {
            var usersController = MakeUsersController();
            var registration = new CreateUserDTO("ABCD", "ABC", "someEmail@gmail.com");

            var exception = Assert.ThrowsAsync<UserRegistrationException>(() => usersController.RegisterUser(registration));

            Assert.Contains(AppResources.PasswordTooShortMessage, exception.ErrorMessages);
        }

        [Test]
        [TestCase("ValidName", "Password", "someEmail@gmail.com")]
        public async Task RegisterUser_EverythingOk_RegistersUser(string name, string password, string email)
        {
            var missioContext = Utils.MakeMissioContext();
            var usersController = MakeUsersController(missioContext);
            var registration = new CreateUserDTO(name, password, email);

            await usersController.RegisterUser(registration);

            Assert.IsTrue(missioContext.Users.Any(x => x.UserName == name && x.HashedPassword == "Hashed" + password && x.Email == email));
        }
    }
}
