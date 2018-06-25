using System.Collections.Generic;
using Missio.LocalDatabase;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("UITests")]
    public class NewsFeedTests
    {
        private IApp _app;
        private readonly Platform _platform;

        public NewsFeedTests(Platform platform)
        {
            this._platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        private static object[] GetOnAppearTestData()
        {
            var testData = new object[LocalUserDatabase.ValidUsers.Count];
            for (var i = 0; i < LocalUserDatabase.ValidUsers.Count; i++)
            {
                var user = LocalUserDatabase.ValidUsers[i];
                testData[i] = new object[] {user, LocalNewsFeedPostDatabase.GetMostRecentPostsAsStrings(user)};
            }
            return testData;
        }

        [Test]
        [TestCaseSource(nameof(GetOnAppearTestData))]
        public void OnAppear_GivenNewsFeedPosts_DisplaysPosts(User.User user, List<string> expectedPosts)
        {
            //Arrange and act
            _app.LogInWithUser(user);

            //Assert
            foreach (var expectedPost in expectedPosts)
            {
                _app.WaitForElement(c => c.Text(expectedPost));
            }
        }

        [Test]
        public void AddPostButton_NormalClick_GoesToPublicationPage()
        {
            //Arrange
            _app.LogInWithDefaultUser();
            //Act
            _app.Tap(c => c.Button("AddPostButton"));
            //Assert
            _app.WaitForElement(c => c.Marked("PublicationPage"));
        }
    }
}