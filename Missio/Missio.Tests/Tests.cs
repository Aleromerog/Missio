using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        private IApp app;
        private readonly Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        [TestCase("Non existing user 1")]
        [TestCase("Non existing user 2")]
        public void LogIn_InvalidUserName_DisplaysNonAvailableUserAlert(string invalidUserName)
        {
            // Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), invalidUserName);
            // Act
            app.Tap(c => c.Marked("LogInCommand"));
            // Assert
            app.WaitForElement(c => c.Text("There does not exist a user with the given name"));
        }
    }
}

