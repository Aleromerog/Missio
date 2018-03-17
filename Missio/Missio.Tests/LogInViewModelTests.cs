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
        private IAttemptToLogin AttemptToLogin;

        [SetUp]
        public void SetUp()
        {
            AttemptToLogin = Substitute.For<IAttemptToLogin>();
            LogInViewModel = new LogInViewModel(AttemptToLogin);
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
            AttemptToLogin
                .Received(1)
                .AttemptToLoginWithUser(Arg.Is<User>(x => x.UserName == userName && x.Password == password));
        }
    }
}