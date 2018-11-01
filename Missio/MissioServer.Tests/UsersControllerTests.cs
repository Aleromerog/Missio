using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Missio.Users;
using NUnit.Framework;
using MissioServer.Controllers;
using MissioServer.Services;
using MissioServer.Services.Services;
using NSubstitute;
using StringResources;
using static System.Linq.Enumerable;

namespace MissioServer.Tests
{
    [TestFixture] 
    public class UsersControllerTests
    {
        private static UsersController MakeUsersController(MissioContext missioContext = null, IWebClientService webClientService = null)
        {
            if(missioContext == null)
                missioContext = Utils.MakeMissioContext();
            if(webClientService == null)
                webClientService = Substitute.For<IWebClientService>();
            var passwordService = new MockPasswordService();
            var userService = new UsersService(missioContext, passwordService);
            var registerUserService = new RegisterUserService(missioContext, passwordService, webClientService);
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
        public async Task RegisterUser_UserNameAlreadyInUse_ThrowsException()
        {
            var usersController = MakeUsersController();
            var registration = new CreateUserDTO("Francisco Greco", "Password", "someEmail@gmail.com");

            var result = (ObjectResult)await usersController.RegisterUser(registration);

            Assert.Contains(AppResources.UserNameAlreadyInUseMessage, (List<string>) result.Value);
        }

        [Test]
        public async Task RegisterUser_PasswordIsTooShort_ThrowsExceptionAsync()
        {
            var usersController = MakeUsersController();
            var registration = new CreateUserDTO("ABCD", "ABC", "someEmail@gmail.com");

            var result = (ObjectResult) await usersController.RegisterUser(registration);

            Assert.Contains(AppResources.PasswordTooShortMessage, (List<string>) result.Value);
        }

        [Test]
        [TestCase("ValidName", "Password", "someEmail@gmail.com")]
        public async Task RegisterUser_EverythingOkButPictureIsNull_SetsDefaultPicture(string name, string password, string email)
        {
            var webClientService = Substitute.For<IWebClientService>();
            var defaultPicture = new byte[1];
            webClientService.DownloadData(Arg.Any<string>()).Returns(defaultPicture);
            var missioContext = Utils.MakeMissioContext();
            var usersController = MakeUsersController(missioContext, webClientService);
            var registration = new CreateUserDTO(name, password, email);

            await usersController.RegisterUser(registration);

            Assert.IsTrue(missioContext.Users.Any(x => x.UserName == name && x.Email == email && x.Picture == defaultPicture));
            Assert.IsTrue(missioContext.UsersCredentials.Any(x => x.User.UserName == name && x.HashedPassword == "Hashed" + password));
        }

        [Test]
        [TestCase("ValidName", "Password", "someEmail@gmail.com")]
        public async Task RegisterUser_EverythingOk_RegistersUser(string name, string password, string email)
        {
            var missioContext = Utils.MakeMissioContext();
            var usersController = MakeUsersController(missioContext);
            var picture = new byte[1];
            var registration = new CreateUserDTO(name, password, email, picture);

            await usersController.RegisterUser(registration);

            Assert.IsTrue(missioContext.Users.Any(x => x.UserName == name && x.Email == email && x.Picture == picture));
            Assert.IsTrue(missioContext.UsersCredentials.Any(x => x.User.UserName == name && x.HashedPassword == "Hashed" + password));
        }
    }
}
