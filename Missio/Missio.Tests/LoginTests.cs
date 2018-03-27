using NUnit.Framework;
using StringResources;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("UITests")]
    public class LoginTests
    {
        private IApp app;
        private readonly Platform platform;

        public LoginTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.GetLogIncorrectPasswordTestsCases))]
        public void LogIn_GivenUserName_DisplaysIncorrectPassword(string userName, string password)
        {
            // Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            app.DismissKeyboard();
            // Act
            app.Tap(c => c.Marked("LogInButton"));
            // Assert
            app.WaitForElement(c => c.Text(AppResources.IncorrectPasswordMessage));
        }

        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.GetInvalidUsers))]
        public void LogIn_GivenUserName_DisplaysIncorrectUserName(string userName, string password)
        {
            // Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            app.DismissKeyboard();
            // Act
            app.Tap(c => c.Marked("LogInButton"));
            // Assert
            app.WaitForElement(c => c.Text(AppResources.IncorrectUserNameMessage));
        }

        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.GetValidUsers))]
        public void LogIn_ValidUserNameAndPassword_DisplaysNewsFeed(string userName, string password)
        {
            //Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            app.DismissKeyboard();
            //Act
            app.Tap(c => c.Marked("LogInButton"));
            //Assert
            app.WaitForElement(c => c.Marked("NewsFeedContentPage"));
        }

        [Test]
        public void LogIn_GoToRegistrationPage_GoesToRegistrationPage()
        {
            //Arrange
            
            //Act
            app.Tap(c => c.Marked("RegisterButton"));
            //Assert
            app.WaitForElement(c => c.Marked("RegistrationPage"));
        }
    }
}

