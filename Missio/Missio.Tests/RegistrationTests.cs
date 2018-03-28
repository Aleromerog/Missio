using NUnit.Framework;
using StringResources;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("UITests")]
    public class RegistrationTests
    {
        private IApp app;
        private readonly Platform platform;

        public RegistrationTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void SetUp()
        {
            app = AppInitializer.StartApp(platform);
            app.Tap(c => c.Marked("RegisterButton"));
        }

        [Test]
        public void RegisterCommand_UserNameIsTooShort_DisplaysAlert()
        {
            //Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), "AB");
            app.DismissKeyboard();
            //Act
            app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            app.WaitForElement(c => c.Text(AppResources.UserNameTooShortMessage));
        }

        [Test]
        public void RegisterCommand_PasswordIsTooShort_DisplaysAlert()
        {
            //Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            app.EnterText(c => c.Marked("PasswordEntry"), "AB");
            app.EnterText(c => c.Marked("ConfirmPasswordEntry"), "AB");
            app.DismissKeyboard();
            //Act
            app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            app.WaitForElement(c => c.Text(AppResources.PasswordTooShortMessage));
        }

        [Test]
        public void RegisterCommand_PasswordsDontMatch_DisplaysAlert()
        {
            //Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            app.EnterText(c => c.Marked("PasswordEntry"), "AB");
            app.EnterText(c => c.Marked("ConfirmPasswordEntry"), "A");
            app.DismissKeyboard();
            //Act
            app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            app.WaitForElement(c => c.Text(AppResources.PasswordsDontMatchMessage));
        }

        [Test]
        public void RegisterCommand_UserNameAlreadyInUse_DisplaysAlert()
        {
            //Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), "Jorge Romero");
            app.DismissKeyboard();
            //Act
            app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            app.WaitForElement(c => c.Text(AppResources.UserNameAlreadyInUseMessage));
        }

        [Test]
        public void RegisterCommand_EverythingIsOkay_DisplaysSuccessMessageAndGoesBack()
        {
            //Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            app.EnterText(c => c.Marked("PasswordEntry"), "Some password");
            app.EnterText(c => c.Marked("ConfirmPasswordEntry"), "Some password");
            app.DismissKeyboard();
            //Act
            app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            app.WaitForElement(c => c.Text(AppResources.RegistrationSuccessfulMessage));
            app.WaitForElement(c => c.Marked("LogInPage"));
        }
    }
}