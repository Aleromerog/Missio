using System.Collections.Generic;
using Missio.NewsFeed;
using Missio.Posts;
using NSubstitute;
using NUnit.Framework;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private static NewsFeedViewModel<PublicationPage> MakeNewsFeedViewModel(IGetMostRecentPosts getMostRecentPosts, INavigation navigation)
        {
            return new NewsFeedViewModel<PublicationPage>(getMostRecentPosts, navigation);
        }

        private static IGetMostRecentPosts MakeFakeGetMostRecentPosts(List<IPost> posts)
        {
            var fakeGetMostRecentPosts = Substitute.For<IGetMostRecentPosts>();
            fakeGetMostRecentPosts.GetMostRecentPostsInOrder().Returns(posts);
            return fakeGetMostRecentPosts;
        }

        [Test]
        public void UpdatePosts_IsRefreshingSetToTrue_SetsIsRefreshingToFalse()
        {
            var fakeGetMostRecentPosts = MakeFakeGetMostRecentPosts(new List<IPost>());
            var newsFeedViewModel = MakeNewsFeedViewModel(fakeGetMostRecentPosts, Substitute.For<INavigation>());
            newsFeedViewModel.IsRefreshing = true;

            newsFeedViewModel.UpdatePosts();

            Assert.IsFalse(newsFeedViewModel.IsRefreshing);
        }

        [Test]
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            var fakeGetMostRecentPosts = MakeFakeGetMostRecentPosts(new List<IPost>());
            var fakeNavigation = Substitute.For<INavigation>();
            var newsFeedViewModel = MakeNewsFeedViewModel(fakeGetMostRecentPosts, fakeNavigation);

            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            fakeNavigation.Received(1).GoToPage<PublicationPage>();
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.ExtraPosts))]
        public void Constructor_NormalConstructor_UpdatesPosts(List<IPost> postsToAdd)
        {
            var fakeGetMostRecentPosts = MakeFakeGetMostRecentPosts(postsToAdd);
            var newsFeedViewModel = MakeNewsFeedViewModel(fakeGetMostRecentPosts, Substitute.For<INavigation>());

            Assert.That(newsFeedViewModel.Posts, Is.EquivalentTo(postsToAdd));
        }
    }
}