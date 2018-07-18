using Missio.LogIn;
using Missio.LogInRes;
using Missio.Navigation;
using Missio.Users;
using NSubstitute;
using NUnit.Framework;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.LogInTests
{
    [TestFixture]
    public class LogInViewModelTests
    {
        private LogInViewModel<RegistrationPage> _logInViewModel;
        private INavigation _fakeNavigation;
        private ISetLoggedInUser _fakeSetLoggedInUser;
        private IDisplayAlertOnCurrentPage _fakeDisplayAlertOnCurrentPage;
        private IValidateUser _fakeUserValidator;

        [SetUp]
        public void SetUp()
        {
            _fakeNavigation = Substitute.For<INavigation>();
            _fakeSetLoggedInUser = Substitute.For<ISetLoggedInUser>();
            _fakeDisplayAlertOnCurrentPage = Substitute.For<IDisplayAlertOnCurrentPage>();
            _fakeUserValidator = Substitute.For<IValidateUser>();
            _logInViewModel = new LogInViewModel<RegistrationPage>(_fakeNavigation, _fakeUserValidator, _fakeDisplayAlertOnCurrentPage,
                _fakeSetLoggedInUser);
        }

        [Test]
        public void Constructor_NormalConstructor_InitializesUserWithEmptyFields()
        {
            Assert.IsEmpty(_logInViewModel.UserName);
            Assert.IsEmpty(_logInViewModel.Password);
        }

        [Test]
        public void UserNameSetter_ValueIsNull_IsSetToEmptyString()
        {
            _logInViewModel.UserName = null;

            Assert.IsEmpty(_logInViewModel.UserName);
        }

        [Test]
        public void PasswordSetter_ValueIsNull_IsSetToEmptyString()
        {
            _logInViewModel.Password = null;

            Assert.IsEmpty(_logInViewModel.Password);
        }

        [Test]
        public void GoToRegistrationPageCommand_NormalExecute_GoesToRegistrationPage()
        {
            _logInViewModel.GoToRegistrationPageCommand.Execute(null);

            _fakeNavigation.Received(1).GoToPage<RegistrationPage>();
        }

        [Test]
        public void LogInCommand_ValidUser_SetsLoggedInUserAndGoesToNextPage()
        {
            var user = new User("Someone", "");
            _logInViewModel.User = user;

            _logInViewModel.LogInCommand.Execute(null);

            _fakeUserValidator.Received(1).ValidateUser(user);
            _fakeSetLoggedInUser.Received(1).LoggedInUser = user;
            _fakeNavigation.Received(1).GoToPage<MainTabbedPage>();
        }

        [Test]
        public void AttemptToLogin_InvalidPassword_DisplaysAlert()
        {
            var user = new User("Someone", "");
            _logInViewModel.User = user;
            _fakeUserValidator.When(x => x.ValidateUser(user)).Throw<InvalidPasswordException>();

            _logInViewModel.LogInCommand.Execute(null);

            _fakeDisplayAlertOnCurrentPage.Received(1).DisplayAlert(new InvalidPasswordException().AlertTextMessage);
        }

        [Test]
        public void AttemptToLogin_InvalidUserName_DisplaysAlert()
        {
            var user = new User("Someone", "");
            _logInViewModel.User = user;
            _fakeUserValidator.When(x => x.ValidateUser(user)).Throw<InvalidUserNameException>();

            _logInViewModel.LogInCommand.Execute(null);

            _fakeDisplayAlertOnCurrentPage.Received(1).DisplayAlert(new InvalidUserNameException().AlertTextMessage);
        }
    }
}