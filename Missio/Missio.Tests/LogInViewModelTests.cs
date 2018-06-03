using Mission.Model.Data;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class LogInViewModelTests
    {
        private LogInViewModel _logInViewModel;
        private IAttemptToLogin _attemptToLogin;
        private IGoToView _fakeGoToView;

        [SetUp]
        public void SetUp()
        {
            _attemptToLogin = Substitute.For<IAttemptToLogin>();
            _fakeGoToView = Substitute.For<IGoToView>();
            _logInViewModel = new LogInViewModel(_attemptToLogin, _fakeGoToView);
        }

        [Test]
        public void GetPassword_FieldIsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            var password = _logInViewModel.Password;
            //Assert
            Assert.AreEqual("", password);
        }

        [Test]
        public void GetUserName_FieldIsNull_ReturnsEmptyString()
        {
            //Arrange

            //Act
            var name = _logInViewModel.UserName;
            //Assert
            Assert.AreEqual("", name);
        }

        [Test]
        [TestCase("Paco", "ElPass")]
        [TestCase("Jorge", "999")]
        [TestCase("UnMen", "password")]
        public void LogInCommand_GivenUserNameAndPassword_AttemptsToLogin(string userName, string password)
        {
            //Arrange
            _logInViewModel.UserName = userName;
            _logInViewModel.Password = password;
            //Act
            _logInViewModel.LogInCommand.Execute(null);
            //Assert
            _attemptToLogin.Received(1)
                .AttemptToLoginWithUser(Arg.Is<User>(x => x.UserName == userName && x.Password == password));
        }

        [Test]
        public void GoToRegistrationPageCommand_NormalExecute_GoesToRegistrationPage()
        {
            //Arrange

            //Act
            _logInViewModel.GoToRegistrationPageCommand.Execute(null);
            //Assert
            _fakeGoToView.Received(1).GoToView("Registration page");
        }
    }
}