using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NSubstitute;
using NUnit.Framework;
using ViewModel;
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

    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private NewsFeedViewModel NewsFeedViewModel;
        private UserInformation UserInformation;
        private INewsFeedPostsProvider FakeNewsFeedPostsProvider;

        [SetUp]
        public void SetUp()
        {
            FakeNewsFeedPostsProvider = Substitute.For<INewsFeedPostsProvider>();
            UserInformation = new UserInformation();
            NewsFeedViewModel = new NewsFeedViewModel(UserInformation, FakeNewsFeedPostsProvider);
        }

        private static object[] GetUsers()
        {
            return FakeUserValidator.ValidUsers.Cast<object>().ToArray();
        }

        [Test]
        [TestCaseSource(nameof(GetUsers))]
        public void OnUserLoggedIn_GivenUser_UpdatesPosts(User user)
        {
            //Arrange
            var posts = new ObservableCollection<NewsFeedPost> { new NewsFeedPost(), new NewsFeedPost() };
            FakeNewsFeedPostsProvider.GetMostRecentPosts(user).Returns(posts);
            //Act
            UserInformation.LoggedInUser = user;
            //Assert
            Assert.AreEqual(posts, NewsFeedViewModel.NewsFeedPosts);
        }
    }

}