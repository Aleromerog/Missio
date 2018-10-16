using Missio.Tests;
using Missio.Users;
using Missio.UserTests;
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
            _app.EnterText(c => c.Marked("UserNameEntry"), "AB");
            _app.DismissKeyboard();

            _app.Tap(c => c.Marked("RegisterButton"));

            _app.WaitForElement(c => c.Text(AppResources.UserNameTooShortMessage));
        }

        [Test]
        public void RegisterCommand_PasswordIsTooShort_DisplaysAlert()
        {
            _app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            _app.EnterText(c => c.Marked("PasswordEntry"), "AB");
            _app.DismissKeyboard();

            _app.Tap(c => c.Marked("RegisterButton"));

            _app.WaitForElement(c => c.Text(AppResources.PasswordTooShortMessage));
        }

        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.GetValidUsersAsObjects))]
        public void RegisterCommand_UserNameAlreadyInUse_DisplaysAlert(User user)
        {
            _app.EnterText(c => c.Marked("UserNameEntry"), user.UserName);
            _app.EnterText(c => c.Marked("PasswordEntry"), user.Password);
            _app.DismissKeyboard();

            _app.Tap(c => c.Marked("RegisterButton"));

            _app.WaitForElement(c => c.Text(AppResources.UserNameAlreadyInUseMessage));
        }

        [Test]
        public void RegisterCommand_EverythingIsOkay_DisplaysSuccessMessageAndGoesBack()
        {
            _app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            _app.EnterText(c => c.Marked("PasswordEntry"), "Some password");
            _app.DismissKeyboard();

            _app.Tap(c => c.Marked("RegisterButton"));

            _app.WaitForElement(c => c.Text(AppResources.RegistrationSuccessfulMessage));
            _app.WaitForElement(c => c.Marked("LogInPage"));
        }

        [Test]
        public void RegisterCommand_TryToLogInWithCreatedUser_SuccessfullyLogsIn()
        {
            _app.EnterText(c => c.Marked("UserNameEntry"), "Some username");
            _app.EnterText(c => c.Marked("PasswordEntry"), "Some password");
            _app.DismissKeyboard();
            _app.Tap(c => c.Marked("RegisterButton"));
            _app.Tap(c => c.Text(AppResources.Ok));
            _app.WaitForElement(c => c.Marked("LogInPage"));

            _app.LogInWithUser(new User("Some username", "Some password", ""));

            _app.WaitForElement(c => c.Marked("NewsFeedPage"));
        }
    }
}