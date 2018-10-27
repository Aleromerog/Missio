using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Missio.Registration;
using Missio.Users;
using MissioServer.Services.Services;
using NSubstitute;
using NUnit.Framework;
using StringResources;

namespace MissioServer.Services.Tests
{
    [TestFixture] 
    public class RegisterUserServiceTests
    {
        private static MissioContext MakeMissioContext()
        {
            var databaseOptions = new DbContextOptionsBuilder<MissioContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var missioContext = new MissioContext(databaseOptions, Substitute.For<IPasswordHasher<User>>(), Substitute.For<IWebClientService>());
            missioContext.Database.EnsureCreated();
            return missioContext;
        }

        private RegisterUserService MakeRegisterUserService(MissioContext missioContext, IPasswordHasher<User> passwordService = null)
        {
            if(passwordService == null)
                passwordService = Substitute.For<IPasswordHasher<User>>();
            return new RegisterUserService(missioContext, passwordService);
        }

        [Test]
        public void RegisterUser_UserNameIsTooShort_ThrowsException()
        {
            var registerUserService = MakeRegisterUserService(MakeMissioContext());
            var registration = new RegistrationDTO("ABC", "Password", "someEmail@gmail.com");

            var exception = Assert.ThrowsAsync<UserRegistrationException>(() => registerUserService.RegisterUser(registration));

            Assert.Contains(AppResources.UserNameTooShortMessage, exception.ErrorMessages);
        }

        [Test]
        public void RegisterUser_UserNameAlreadyInUse_ThrowsException()
        {
            var registerUserService = MakeRegisterUserService(MakeMissioContext());
            var registration = new RegistrationDTO("Francisco Greco", "Password", "someEmail@gmail.com");

            var exception = Assert.ThrowsAsync<UserRegistrationException>(() => registerUserService.RegisterUser(registration));

            Assert.Contains(AppResources.UserNameAlreadyInUseMessage, exception.ErrorMessages);
        }

        [Test]
        public void RegisterUser_PasswordIsTooShort_ThrowsException()
        {
            var registerUserService = MakeRegisterUserService(MakeMissioContext());
            var registration = new RegistrationDTO("ABCD", "ABC", "someEmail@gmail.com");

            var exception = Assert.ThrowsAsync<UserRegistrationException>(() => registerUserService.RegisterUser(registration));

            Assert.Contains(AppResources.PasswordTooShortMessage, exception.ErrorMessages);
        }

        [Test]
        [TestCase("ValidName", "Password", "someEmail@gmail.com")]
        public async Task RegisterUser_EverythingOk_RegistersUser(string name, string password, string email)
        {
            var missioContext = MakeMissioContext();
            var hashedPassword = "HashedPassword";
            var passwordService = Substitute.For<IPasswordHasher<User>>();
            passwordService.HashPassword(password).Returns(hashedPassword);
            var registerUserService = MakeRegisterUserService(missioContext, passwordService);
            var registration = new RegistrationDTO("ValidName", "Password", "someEmail@gmail.com");

            await registerUserService.RegisterUser(registration);

            Assert.IsTrue(missioContext.Users.Any(x => x.UserName == name && x.HashedPassword == hashedPassword && x.Email == email));
        }
    }
}
