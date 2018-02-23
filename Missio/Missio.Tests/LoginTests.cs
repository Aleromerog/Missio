using StringResources;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class LoginTests
    {
        private IApp app;
        private readonly Platform platform;

        private readonly object[] LogInErrorTestsCases =
        {
            new object[] { "Non existing user 1", "", AppResources.IncorrectUserNameMessage },
            new object[] { "Non existing user 2", "", AppResources.IncorrectUserNameMessage },
            new object[] { "Existing user 1", "", AppResources.IncorrectPasswordMessage },
            new object[] { "Existing user 2", "", AppResources.IncorrectPasswordMessage },
        };

        private readonly object[] LogInSuccessfulTestsCases =
        {
            new object[] { "Existing user 1", "ValidPassword" },
            new object[] { "Existing user 2", "ValidPassword" },
        };


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
        [TestCaseSource(nameof(LogInErrorTestsCases))]
        public void LogIn_GivenUserName_DisplaysExpectedMessage(string userName, string password, string expectedMessage)
        {
            // Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            // Act
            app.Tap(c => c.Marked("LogInButton"));
            // Assert
            app.WaitForElement(c => c.Text(expectedMessage));
        }

        [Test]
        [TestCaseSource(nameof(LogInSuccessfulTestsCases))]
        public void LogIn_ValidUserNameAndPassword_DisplaysNewsFeed(string userName, string password)
        {
            //Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            //Act
            app.Tap(c => c.Marked("LogInButton"));
            //Assert
            app.WaitForElement(c => c.Marked("NewsFeedContentPage"));
        }
    }
}

