using Missio.Tests;
using Missio.Users;
using NUnit.Framework;
using StringResources;
using Xamarin.UITest;

namespace Missio.RegistrationTests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("UITests")]
    public class RegistrationUserInterfaceTests
    {
        private IApp _app;
        private readonly Platform _platform;

        public RegistrationUserInterfaceTests(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void SetUp()
        {
            _app = AppInitializer.StartApp(_platform);
            _app.Tap(c => c.Marked("RegisterButton"));
        }

        [Test]
        public void RegisterCommand_UserNameIsTooShort_DisplaysAlert()
        {
            //Arrange
            _app.EnterText(c => c.Marked("UserNameEntry"), "AB");
            _app.DismissKeyboard();
            //Act
            _app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            _app.WaitForElement(c => c.Text(AppResources.UserNameTooShortMessage));
        }

        [Test]
        public void RegisterCommand_PasswordIsTooShort_DisplaysAlert()
        {
            //Arrange
            _app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            _app.EnterText(c => c.Marked("PasswordEntry"), "AB");
            _app.DismissKeyboard();
            //Act
            _app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            _app.WaitForElement(c => c.Text(AppResources.PasswordTooShortMessage));
        }

        [Test]
        public void RegisterCommand_UserNameAlreadyInUse_DisplaysAlert()
        {
            //Arrange
            _app.EnterText(c => c.Marked("UserNameEntry"), "Jorge Romero");
            _app.EnterText(c => c.Marked("PasswordEntry"), "Some password");
            _app.DismissKeyboard();
            //Act
            _app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            _app.WaitForElement(c => c.Text(AppResources.UserNameAlreadyInUseMessage));
        }

        [Test]
        public void RegisterCommand_EverythingIsOkay_DisplaysSuccessMessageAndGoesBack()
        {
            //Arrange
            _app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            _app.EnterText(c => c.Marked("PasswordEntry"), "Some password");
            _app.DismissKeyboard();
            //Act
            _app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            _app.WaitForElement(c => c.Text(AppResources.RegistrationSuccessfulMessage));
            _app.WaitForElement(c => c.Marked("LogInPage"));
        }

        [Test]
        public void RegisterCommand_TryToLogInWithCreatedUser_SuccessfullyLogsIn()
        {
            //Arrange
            _app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            _app.EnterText(c => c.Marked("PasswordEntry"), "Some password");
            _app.DismissKeyboard();
            _app.Tap(c => c.Marked("RegisterButton"));
            _app.Tap(c => c.Text(AppResources.Ok));
            _app.WaitForElement(c => c.Marked("LogInPage"));
            //Act
            _app.LogInWithUser(new User("Some username", "Some password"));
            //Assert
            _app.WaitForElement(c => c.Marked("NewsFeedPage"));
        }
    }
}