using Mission.Model.Data;
using Mission.Model.Exceptions;
using Mission.Model.LocalProviders;
using NSubstitute;
using NUnit.Framework;
using StringResources;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class AttemptToLoginTests
    {
        private AttemptToLogIn _attemptToLogIn;
        private IValidateUser _fakeUserValidator;
        private IDisplayAlertOnCurrentPage _displayAlertOnCurrentPage;
        private ISetLoggedInUser _setLoggedInUser;
        private IGoToView _goToView;

        [SetUp]
        public void SetUp()
        {
            _fakeUserValidator = Substitute.For<IValidateUser>();
            _displayAlertOnCurrentPage = Substitute.For<IDisplayAlertOnCurrentPage>();
            _setLoggedInUser = Substitute.For<ISetLoggedInUser>();
            _goToView = Substitute.For<IGoToView>();
            _attemptToLogIn = new AttemptToLogIn(_fakeUserValidator, _displayAlertOnCurrentPage,
                _goToView, _setLoggedInUser);
        }

        [Test]
        public void AttemptToLogin_ValidUser_SetsLoggedInUserAndGoesToNextPage()
        {
            //Arrange
            var user = new User("Someone", "");
            //Act
            _attemptToLogIn.AttemptToLoginWithUser(user);
            //Assert
            _fakeUserValidator.Received(1).ValidateUser(user);
            _setLoggedInUser.Received(1).LoggedInUser = user;
            _goToView.Received(1).GoToView("Main tabbed page");
        }

        [Test]
        public void AttemptToLogin_InvalidPassword_DisplaysAlert()
        {
            //Arrange
            var user = new User("Someone", "");
            _fakeUserValidator.When(x => x.ValidateUser(user)).Throw<InvalidPasswordException>();
            //Act
            _attemptToLogIn.AttemptToLoginWithUser(user);
            //Assert
            _displayAlertOnCurrentPage.Received(1).DisplayAlert(AppResources.IncorrectPasswordTitle, AppResources.IncorrectPasswordMessage, AppResources.Ok);
        }

        [Test]
        public void AttemptToLogin_InvalidUserName_DisplaysAlert()
        {
            var user = new User("Someone", "");
            _fakeUserValidator.When(x => x.ValidateUser(user)).Throw<InvalidUserNameException>();
            //Act
            _attemptToLogIn.AttemptToLoginWithUser(user);
            //Assert
            _displayAlertOnCurrentPage.Received(1).DisplayAlert(AppResources.IncorrectUserNameTitle, AppResources.IncorrectUserNameMessage, AppResources.Ok);
        }
    }
}