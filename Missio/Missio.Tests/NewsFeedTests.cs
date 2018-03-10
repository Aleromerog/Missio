using System.Collections.Generic;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class NewsFeedTests
    {
        private IApp app;
        private readonly Platform platform;

        public NewsFeedTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        private static object[] GetOnAppearTestData()
        {
            var testData = new object[FakeUserValidator.ValidUsers.Count];
            for (var i = 0; i < FakeUserValidator.ValidUsers.Count; i++)
            {
                var user = FakeUserValidator.ValidUsers[i];
                testData[i] = new object[] { user, FakeNewsFeedPostProvider.GetMostRecentPostsAsStrings(user) };
            }
            return testData;
        }

        [Test]
        [TestCaseSource(nameof(GetOnAppearTestData))]
        public void OnAppear_GivenNewsFeedPosts_DisplaysPosts(User user, List<string> expectedPosts)
        {
            //Arrange and act
            AppInitializer.TryToLogIn(app, user);

            //Assert
            foreach (var expectedPost in expectedPosts)
            {
                var posts = app.Query(c => c.Text(expectedPost));
                Assert.AreEqual(1, posts.Length);
            }
        }
    }
}