using System;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using DomainTests;
using NSubstitute;
using NUnit.Framework;
using ViewModels;
using ViewModels.Factories;

namespace ViewModelTests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private static NewsFeedViewModel MakeNewsFeedViewModel(IPublicationPageFactory publicationPageFactory = null, NameAndPassword nameAndPassword = null, ICommentsPageFactory commentsPageFactory = null)
        {
            if(publicationPageFactory == null)
                publicationPageFactory = Substitute.For<IPublicationPageFactory>();
            if(nameAndPassword == null)
                nameAndPassword = new NameAndPassword("Francisco Greco", "ElPass");
            if(commentsPageFactory == null) 
                commentsPageFactory = Substitute.For<ICommentsPageFactory>();
            var fakeRepository = Substitute.For<IPostRepository>();
            var orderedPosts = Utils.MakeSortedDummyPost();
            fakeRepository.GetMostRecentPostsInOrder(nameAndPassword).Returns(orderedPosts);
            return new NewsFeedViewModel(fakeRepository, publicationPageFactory, commentsPageFactory, nameAndPassword);
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
        public void GoToPublicationPageCommand_Should_GoToPublicationPage()
        {
            var publicationPageFactory = Substitute.For<IPublicationPageFactory>();
            var nameAndPassword = new NameAndPassword("", "");
            var newsFeedViewModel = MakeNewsFeedViewModel(publicationPageFactory, nameAndPassword);

            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            publicationPageFactory.Received(1).CreateAndNavigateToPage(nameAndPassword, Arg.Any<Action>());
        }

        [Test]
        public void GoToPublicationPageCommand_Should_RefreshPostsOnCallback()
        {
            var publicationPageFactory = Substitute.For<IPublicationPageFactory>();
            publicationPageFactory.CreateAndNavigateToPage(Arg.Any<NameAndPassword>(), Arg.Do<Action>(x => x()));
            var nameAndPassword = new NameAndPassword("", "");
            var newsFeedViewModel = MakeNewsFeedViewModel(publicationPageFactory, nameAndPassword);
            newsFeedViewModel.Posts.Clear();

            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            Assert.AreEqual(1, newsFeedViewModel.Posts.Count);
        }

        [Test]
        public void GoToCommentsCommand_GivenPost_GoesToCommentsPage()
        {
            var commentsPageFactory = Substitute.For<ICommentsPageFactory>();
            var post = Utils.MakeDummyPost();
            var newsFeedViewModel = MakeNewsFeedViewModel(Substitute.For<IPublicationPageFactory>(), new NameAndPassword("", ""), commentsPageFactory);

            newsFeedViewModel.GoToCommentsCommand.Execute(post);

            commentsPageFactory.Received().CreateAndNavigateToPage(post);
        }

        [Test]
        public void Constructor_NormalConstructor_UpdatesPosts()
        {
            var newsFeedViewModel = MakeNewsFeedViewModel();

            Assert.AreEqual(1, newsFeedViewModel.Posts.Count);
        }
    }
}