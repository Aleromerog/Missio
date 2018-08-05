using System.Collections.Generic;
using Missio.LocalDatabase;
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
        private static NewsFeedViewModel MakeNewsFeedViewModel(IPostRepository postRepository, INavigation navigation)
        {
            return new NewsFeedViewModel(postRepository, navigation);
        }

        private static IPostRepository MakeFakePostRepository(List<IPost> posts)
        {
            var fakePostRepository = Substitute.For<IPostRepository>();
            fakePostRepository.GetMostRecentPostsInOrder().Returns(posts);
            return fakePostRepository;
        }

        [Test]
        public void UpdatePosts_IsRefreshingSetToTrue_SetsIsRefreshingToFalse()
        {
            var fakePostRepository = MakeFakePostRepository(new List<IPost>());
            var newsFeedViewModel = MakeNewsFeedViewModel(fakePostRepository, Substitute.For<INavigation>());
            newsFeedViewModel.IsRefreshing = true;

            newsFeedViewModel.UpdatePosts();

            Assert.IsFalse(newsFeedViewModel.IsRefreshing);
        }

        [Test]
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            var fakePostRepository = MakeFakePostRepository(new List<IPost>());
            var fakeNavigation = Substitute.For<INavigation>();
            var newsFeedViewModel = MakeNewsFeedViewModel(fakePostRepository, fakeNavigation);

            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            fakeNavigation.Received(1).GoToPage<PublicationPage>();
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.ExtraPosts))]
        public void Constructor_NormalConstructor_UpdatesPosts(List<IPost> postsToAdd)
        {
            var fakePostRepository = MakeFakePostRepository(postsToAdd);

            var newsFeedViewModel = MakeNewsFeedViewModel(fakePostRepository, Substitute.For<INavigation>());

            Assert.That(newsFeedViewModel.Posts, Is.EquivalentTo(postsToAdd));
        }
    }
}