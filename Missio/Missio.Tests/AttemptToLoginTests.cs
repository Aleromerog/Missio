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
        private AttemptToLogIn AttemptToLogIn;
        private IValidateUser fakeUserValidator;
        private IDisplayAlertOnCurrentPage displayAlertOnCurrentPage;
        private ISetLoggedInUser setLoggedInUser;
        private IGoToView _goToView;

        [SetUp]
        public void SetUp()
        {
            fakeUserValidator = Substitute.For<IValidateUser>();
            displayAlertOnCurrentPage = Substitute.For<IDisplayAlertOnCurrentPage>();
            setLoggedInUser = Substitute.For<ISetLoggedInUser>();
            _goToView = Substitute.For<IGoToView>();
            AttemptToLogIn = new AttemptToLogIn(fakeUserValidator, displayAlertOnCurrentPage,
                _goToView, setLoggedInUser);
        }

        [Test]
        public void AttemptToLogin_ValidUser_SetsLoggedInUserAndGoesToNextPage()
        {
            //Arrange
            var user = new User("Someone", "");
            //Act
            AttemptToLogIn.AttemptToLoginWithUser(user);
            //Assert
            fakeUserValidator.Received(1).ValidateUser(user);
            setLoggedInUser.Received(1).LoggedInUser = user;
            _goToView.Received(1).GoToView("News feed page");
        }

        [Test]
        public void AttemptToLogin_InvalidPassword_DisplaysAlert()
        {
            //Arrange
            var user = new User("Someone", "");
            fakeUserValidator.When(x => x.ValidateUser(user)).Throw<InvalidPasswordException>();
            //Act
            AttemptToLogIn.AttemptToLoginWithUser(user);
            //Assert
            displayAlertOnCurrentPage.Received(1).DisplayAlert(AppResources.IncorrectPasswordTitle, AppResources.IncorrectPasswordMessage, AppResources.Ok);
        }

        [Test]
        public void AttemptToLogin_InvalidUserName_DisplaysAlert()
        {
            var user = new User("Someone", "");
            fakeUserValidator.When(x => x.ValidateUser(user)).Throw<InvalidUserNameException>();
            //Act
            AttemptToLogIn.AttemptToLoginWithUser(user);
            //Assert
            displayAlertOnCurrentPage.Received(1).DisplayAlert(AppResources.IncorrectUserNameTitle, AppResources.IncorrectUserNameMessage, AppResources.Ok);
        }
    }
}