using System.Collections.Generic;
using Missio.Tests;
using Missio.Users;
using Missio.UserTests;
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
            var validUsers = UserTestUtils.GetValidUsers();
            var testData = new object[validUsers.Count];
            for (var i = 0; i < testData.Length; i++)
            {
                testData[i] = new object[] { validUsers[i], UserTestUtils.GetUserPostsContents(validUsers[i]) };
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