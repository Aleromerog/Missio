using Missio.Tests;
using Missio.UserTests;
using NUnit.Framework;
using StringResources;
using Xamarin.UITest;

namespace Missio.LogInTests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("UITests")]
    public class LogInUserInterfaceTests
    {
        private IApp _app;
        private readonly Platform _platform;

        public LogInUserInterfaceTests(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.GetLogIncorrectPasswordTestsCases))]
        public void LogIn_GivenUserName_DisplaysIncorrectPassword(string userName, string password)
        {
            _app.EnterText(c => c.Marked("UserNameEntry"), userName);
            _app.EnterText(c => c.Marked("PasswordEntry"), password);
            _app.DismissKeyboard();

            _app.Tap(c => c.Marked("LogInButton"));

            _app.WaitForElement(c => c.Text(AppResources.IncorrectPasswordMessage));
        }
        
        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.GetInvalidUsers))]
        public void LogIn_GivenUserName_DisplaysIncorrectUserName(string userName, string password)
        {
            _app.EnterText(c => c.Marked("UserNameEntry"), userName);
            _app.EnterText(c => c.Marked("PasswordEntry"), password);
            _app.DismissKeyboard();

            _app.Tap(c => c.Marked("LogInButton"));

            _app.WaitForElement(c => c.Text(AppResources.IncorrectUserNameMessage));
        }

        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.GetValidUsers))]
        public void LogIn_ValidUserNameAndPassword_DisplaysNewsFeed(string userName, string password)
        {
            _app.EnterText(c => c.Marked("UserNameEntry"), userName);
            _app.EnterText(c => c.Marked("PasswordEntry"), password);
            _app.DismissKeyboard();

            _app.Tap(c => c.Marked("LogInButton"));

            _app.WaitForElement(c => c.Marked("NewsFeedPage"));
        }

        [Test]
        public void LogIn_GoToRegistrationPage_GoesToRegistrationPage()
        {
            _app.Tap(c => c.Marked("RegisterButton"));

            _app.WaitForElement(c => c.Marked("RegistrationPage"));
        }
    }
}

