using Mission.Model.Data;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class LogInViewModelTests
    {
        private LogInViewModel LogInViewModel;
        private IAttemptToLogin LocalAttemptToLogin;

        [SetUp]
        public void SetUp()
        {
            LocalAttemptToLogin = Substitute.For<IAttemptToLogin>();
            LogInViewModel = new LogInViewModel(LocalAttemptToLogin);
        }

        [Test]
        [TestCase("Paco", "ElPass")]
        [TestCase("Jorge", "999")]
        [TestCase("UnMen", "password")]
        public void LogIn_GivenUserNameAndPassword_AttemptsToLogin(string userName, string password)
        {
            //Arrange
            LogInViewModel.UserName = userName;
            LogInViewModel.Password = password;
            //Act
            LogInViewModel.LogIn();
            //Assert
            LocalAttemptToLogin
                .Received(1)
                .AttemptToLoginWithUser(Arg.Is<User>(x => x.UserName == userName && x.Password == password));
        }
    }
}