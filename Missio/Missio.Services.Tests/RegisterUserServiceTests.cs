using System;
using Microsoft.EntityFrameworkCore;
using Missio.LocalDatabase;
using Missio.Users;
using NUnit.Framework;
using StringResources;

namespace MissioServer.Tests
{
    [TestFixture] 
    public class RegisterUserServiceTests
    {
        private static MissioContext MakeMissioContext()
        {
            var databaseOptions = new DbContextOptionsBuilder<MissioContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var misioContext = new MissioContext(databaseOptions);
            return misioContext;
        }

        private RegisterUserService MakeRegisterUserService(MissioContext missioContext)
        {
            return new RegisterUserService(missioContext);
        }

        [Test]
        public void RegisterUser_UserNameIsTooShort_ThrowsException()
        {
            var registerUserService = MakeRegisterUserService(MakeMissioContext());

            var exception = Assert.Throws<UserRegistrationException>(() => registerUserService.RegisterUser("ABC", "Password", "someEmail@gmail.com"));

            Assert.Contains(AppResources.UserNameTooShortMessage, exception.ErrorMessages);
        }

        [Test]
        public void RegisterUser_UserNameAlreadyInUse_ThrowsException()
        {
            var missioContext = MakeMissioContext();
            var repeatedName = "SomeName";
            missioContext.Users.Add(new User(repeatedName));
            var registerUserService = MakeRegisterUserService(missioContext);

            var exception = Assert.Throws<UserRegistrationException>(() => registerUserService.RegisterUser(repeatedName, "Password", "someEmail@gmail.com"));

            Assert.Contains(AppResources.UserNameAlreadyInUseMessage, exception.ErrorMessages);
        }

        [Test]
        public void RegisterUser_PasswordIsTooShort_ThrowsException()
        {
            var registerUserService = MakeRegisterUserService(MakeMissioContext());

            var exception = Assert.Throws<UserRegistrationException>(() => registerUserService.RegisterUser("ABCD", "ABC", "someEmail@gmail.com"));

            Assert.Contains(AppResources.PasswordTooShortMessage, exception.ErrorMessages);
        }

        [Test]
        public void RegisterUser_EverythingOk_RegistersUser()
        {
            throw new NotImplementedException();
        }
    }
}
