using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using DomainTests;
using Missio.Navigation;
using NSubstitute;
using NUnit.Framework;
using ViewModels;
using ViewModels.Views;

namespace ViewModelTests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private static NewsFeedViewModel MakeNewsFeedViewModel(INavigation navigation = null, INameAndPasswordService nameAndPasswordService = null)
        {
            if (nameAndPasswordService == null)
                nameAndPasswordService = Substitute.For<INameAndPasswordService>();
            var fakeRepository = Substitute.For<IPostRepository>();
            var orderedPosts = Utils.MakeSortedDummyPost();
            fakeRepository.GetMostRecentPostsInOrder(Arg.Any<NameAndPassword>()).Returns(orderedPosts);
            return new NewsFeedViewModel(fakeRepository, navigation, nameAndPasswordService);
        }

        [Test]
        public async Task UpdatePosts_IsRefreshingSetToTrue_SetsIsRefreshingToFalse()
        {
            var newsFeedViewModel = MakeNewsFeedViewModel();
            newsFeedViewModel.IsRefreshing = true;

            await newsFeedViewModel.UpdatePosts();

            Assert.IsFalse(newsFeedViewModel.IsRefreshing);
        }

        [Test]
        public void Constructor_Should_UpdatePosts()
        {
            var newsFeedViewModel = MakeNewsFeedViewModel();

            Assert.AreEqual(1, newsFeedViewModel.Posts.Count);
        }

        [Test]
        public void GoToPublicationPageCommand_Should_GoToPublicationPage()
        {
            var navigation = Substitute.For<INavigation>();
            var publicationPageViewModel = Substitute.For<IPublicationPageViewModel>();
            navigation.GoToPage<PublicationPage, IPublicationPageViewModel>().Returns(publicationPageViewModel);
            var newsFeedViewModel = MakeNewsFeedViewModel(navigation);

            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            navigation.Received(1).GoToPage<PublicationPage, IPublicationPageViewModel>();
        }

        [Test]
        public void GoToPublicationPageCommand_Should_RefreshPostsOnCallback()
        {
            var navigation = Substitute.For<INavigation>();
            var publicationPageViewModel = Substitute.For<IPublicationPageViewModel>();
            navigation.GoToPage<PublicationPage, IPublicationPageViewModel>().Returns(publicationPageViewModel);
            var newsFeedViewModel = MakeNewsFeedViewModel(navigation);
            newsFeedViewModel.Posts.Clear();
            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            publicationPageViewModel.PostPublishedEvent+= Raise.Event();

            Assert.AreEqual(1, newsFeedViewModel.Posts.Count);
        }

        [Test]
        public void GoToCommentsCommand_GivenPost_GoesToCommentsPage()
        {
            var navigation = Substitute.For<INavigation>();
            var commentsViewModel = Substitute.For<ICommentsViewModel>();
            navigation.GoToPage<CommentsPage, ICommentsViewModel>().Returns(commentsViewModel);
            var post = Utils.MakeDummyPost();
            var newsFeedViewModel = MakeNewsFeedViewModel(navigation);

            newsFeedViewModel.GoToCommentsCommand.Execute(post);

            navigation.Received().GoToPage<CommentsPage, ICommentsViewModel>();
            commentsViewModel.Received().Post = post;
        }

        [Test]
        public async Task GoToLikesCommand_Should_NavigateToLikesPageAsync()
        {
            var navigation = Substitute.For<INavigation>();
            var likesViewModel = Substitute.For<ILikesViewModel>();
            navigation.GoToPage<LikesPage, ILikesViewModel>().Returns(likesViewModel);
            var post = Utils.MakeDummyPost();
            var newsFeedViewModel = MakeNewsFeedViewModel(navigation);

            newsFeedViewModel.GoToLikesCommand.Execute(post);

            await navigation.Received().GoToPage<LikesPage, ILikesViewModel>();
            likesViewModel.Received().Post = post;
        }
    }
}