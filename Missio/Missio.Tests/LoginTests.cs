using Mission.Model.LocalProviders;
using NUnit.Framework;
using StringResources;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class LoginTests
    {
        private IApp app;
        private readonly Platform platform;

        private static object[] GetLogIncorrectPasswordTestsCases()
        {
            var testData = new object[LocalUserDatabase.ValidUsers.Count];
            for (int i = 0; i < LocalUserDatabase.ValidUsers.Count; i++)
            {
                var user = LocalUserDatabase.ValidUsers[i];
                testData[i] = new object[] { user.UserName, "" };
            }
            return testData;
        }

        private static object[] GetLogInIncorrectUserNameTestCases()
        {
            return LocalUserDatabase.GetListOfUsersInTestForm(LocalUserDatabase.InvalidUsers);
        }

        private static object[] GetLogInSuccessfulTestsCases()
        {
            return LocalUserDatabase.GetListOfUsersInTestForm(LocalUserDatabase.ValidUsers);
        }

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
        [TestCaseSource(nameof(GetLogIncorrectPasswordTestsCases))]
        public void LogIn_GivenUserName_DisplaysIncorrectPassword(string userName, string password)
        {
            // Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            // Act
            app.Tap(c => c.Marked("LogInButton"));
            // Assert
            app.WaitForElement(c => c.Text(AppResources.IncorrectPasswordMessage));
        }

        [Test]
        [TestCaseSource(nameof(GetLogInIncorrectUserNameTestCases))]
        public void LogIn_GivenUserName_DisplaysIncorrectUserName(string userName, string password)
        {
            // Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            // Act
            app.Tap(c => c.Marked("LogInButton"));
            // Assert
            app.WaitForElement(c => c.Text(AppResources.IncorrectUserNameMessage));
        }

        [Test]
        [TestCaseSource(nameof(GetLogInSuccessfulTestsCases))]
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

