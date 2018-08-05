using System.Collections.Generic;
using Missio.LocalDatabase;
using Missio.Tests;
using Missio.Users;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.NewsFeedTests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("UITests")]
    public class NewsFeedUserInterfaceTests
    {
        private IApp _app;
        private readonly Platform _platform;

        public NewsFeedUserInterfaceTests(Platform platform)
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
                testData[i] = new object[] {user, LocalNewsFeedPostRepository.GetMostRecentPostsAsStrings(user)};
            }
            return testData;
        }

        [Test]
        [TestCaseSource(nameof(GetOnAppearTestData))]
        public void OnAppear_GivenNewsFeedPosts_DisplaysPosts(User user, List<string> expectedPosts)
        {
            _app.LogInWithUser(user);

            foreach (var expectedPost in expectedPosts)
                _app.WaitForElement(c => c.Text(expectedPost));
        }

        [Test]
        public void AddPostButton_NormalClick_GoesToPublicationPage()
        {
            _app.LogInWithDefaultUser();

            _app.Tap(c => c.Button("AddPostButton"));

            _app.WaitForElement(c => c.Marked("PublicationPage"));
        }

        [Test]
        public void BackButton_IsPressed_StaysOnCurrentView()
        {
           _app.LogInWithDefaultUser();
            
           _app.Back();

           _app.WaitForNoElement(c => c.Marked("LogInPage"));
        }
    }
}