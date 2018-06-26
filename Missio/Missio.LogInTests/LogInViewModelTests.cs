using Missio.LogIn;
using Missio.Navigation;
using Missio.User;
using NSubstitute;
using NUnit.Framework;

namespace Missio.LogInTests
{
    [TestFixture]
    public class LogInViewModelTests
    {
        private LogInViewModel _logInViewModel;
        private IGoToView _fakeGoToView;
        private ISetLoggedInUser _fakeSetLoggedInUser;
        private IDisplayAlertOnCurrentPage _fakeDisplayAlertOnCurrentPage;
        private IValidateUser _fakeUserValidator;

        [SetUp]
        public void SetUp()
        {
            _fakeGoToView = Substitute.For<IGoToView>();
            _fakeSetLoggedInUser = Substitute.For<ISetLoggedInUser>();
            _fakeDisplayAlertOnCurrentPage = Substitute.For<IDisplayAlertOnCurrentPage>();
            _fakeUserValidator = Substitute.For<IValidateUser>();
            _logInViewModel = new LogInViewModel(_fakeGoToView, _fakeUserValidator, _fakeDisplayAlertOnCurrentPage,
                _fakeSetLoggedInUser);
        }

        [Test]
        public void Constructor_NormalConstructor_InitializesUserWithEmptyFields()
        {
            //Assert
            Assert.IsEmpty(_logInViewModel.UserName);
            Assert.IsEmpty(_logInViewModel.Password);
        }

        [Test]
        public void UserNameSetter_ValueIsNull_IsSetToEmptyString()
        {
            //Arrange

            //Act
            _logInViewModel.UserName = null;
            //Assert
            Assert.IsEmpty(_logInViewModel.UserName);
        }

        [Test]
        public void PasswordSetter_ValueIsNull_IsSetToEmptyString()
        {
            //Arrange

            //Act
            _logInViewModel.Password = null;
            //Assert
            Assert.IsEmpty(_logInViewModel.Password);
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

        [Test]
        public void LogInCommand_ValidUser_SetsLoggedInUserAndGoesToNextPage()
        {
            //Arrange
            var user = new User.User("Someone", "");
            _logInViewModel.User = user;
            //Act
            _logInViewModel.LogInCommand.Execute(null);
            //Assert
            _fakeUserValidator.Received(1).ValidateUser(user);
            _fakeSetLoggedInUser.Received(1).LoggedInUser = user;
            _fakeGoToView.Received(1).GoToView("Main tabbed page");
        }

        [Test]
        public void AttemptToLogin_InvalidPassword_DisplaysAlert()
        {
            //Arrange
            var user = new User.User("Someone", "");
            _logInViewModel.User = user;
            _fakeUserValidator.When(x => x.ValidateUser(user)).Throw<InvalidPasswordException>();
            //Act
            _logInViewModel.LogInCommand.Execute(null);
            //Assert
            _fakeDisplayAlertOnCurrentPage.Received(1).DisplayAlert(new InvalidPasswordException().AlertTextMessage);
        }

        [Test]
        public void AttemptToLogin_InvalidUserName_DisplaysAlert()
        {
            var user = new User.User("Someone", "");
            _logInViewModel.User = user;
            _fakeUserValidator.When(x => x.ValidateUser(user)).Throw<InvalidUserNameException>();
            //Act
            _logInViewModel.LogInCommand.Execute(null);
            //Assert
            _fakeDisplayAlertOnCurrentPage.Received(1).DisplayAlert(new InvalidUserNameException().AlertTextMessage);
        }
    }
}