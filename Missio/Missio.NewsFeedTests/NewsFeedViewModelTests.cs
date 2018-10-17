using System.Collections.Generic;
using Missio.LocalDatabase;
using Missio.LogIn;
using Missio.NewsFeed;
using Missio.Posts;
using Missio.Users;
using Missio.UserTests;
using NSubstitute;
using NUnit.Framework;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private static NewsFeedViewModel MakeNewsFeedViewModel(INavigation navigation, ILoggedInUser loggedInUser)
        {
            var fakeRepository = Substitute.For<IPostRepository>();
            fakeRepository.GetMostRecentPostsInOrder(Arg.Any<User>()).Returns(new List<IPost>());
            return new NewsFeedViewModel(fakeRepository, navigation, loggedInUser);
        }

        private static NewsFeedViewModel MakeNewsFeedViewModel(IPostRepository postsRepository, INavigation navigation, ILoggedInUser loggedInUser)
        {
            return new NewsFeedViewModel(postsRepository, navigation, loggedInUser);
        }

        private static ILoggedInUser MakeLoggedInUser()
        {
            var loggedInUser = Substitute.For<ILoggedInUser>();
            loggedInUser.LoggedInUser.Returns(UserTestUtils.FranciscoUser);
            return loggedInUser;
        }

        [Test]
        public void UpdatePosts_IsRefreshingSetToTrue_SetsIsRefreshingToFalse()
        {
            var newsFeedViewModel = MakeNewsFeedViewModel(Substitute.For<INavigation>(), MakeLoggedInUser());
            newsFeedViewModel.IsRefreshing = true;

            newsFeedViewModel.UpdatePosts();

            Assert.IsFalse(newsFeedViewModel.IsRefreshing);
        }

        [Test]
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            var fakeNavigation = Substitute.For<INavigation>();
            var newsFeedViewModel = MakeNewsFeedViewModel(fakeNavigation, MakeLoggedInUser());

            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            fakeNavigation.Received(1).GoToPage<PublicationPage>();
        }

        [Test]
        public void Constructor_NormalConstructor_UpdatesPosts()
        {
            var loggedInUser = MakeLoggedInUser();
            var postsRepository = Substitute.For<IPostRepository>();
            var expectedPosts = new List<IPost> { new Post() };
            postsRepository.GetMostRecentPostsInOrder(loggedInUser.LoggedInUser).Returns(expectedPosts);

            var newsFeedViewModel = MakeNewsFeedViewModel(postsRepository, Substitute.For<INavigation>(), loggedInUser);

            CollectionAssert.AreEqual(newsFeedViewModel.Posts, expectedPosts);
        }
    }
}