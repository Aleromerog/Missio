using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Missio.NewsFeed;
using Missio.PostPublication;
using Missio.Posts;
using Missio.Users;
using NSubstitute;
using NUnit.Framework;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private static NewsFeedViewModel MakeNewsFeedViewModel(INavigation navigation = null, NameAndPassword nameAndPassword = null)
        {
            if(navigation == null)
                navigation = Substitute.For<INavigation>();
            if(nameAndPassword == null)
                nameAndPassword = new NameAndPassword("Francisco Greco", "ElPass");
            var fakeRepository = Substitute.For<IPostRepository>();
            var orderedPosts = Utils.MakeSortedDummyPost();
            fakeRepository.GetMostRecentPostsInOrder(nameAndPassword).Returns(orderedPosts);
            return new NewsFeedViewModel(fakeRepository, navigation, nameAndPassword);
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
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            var fakeNavigation = Substitute.For<INavigation>();
            var nameAndPassword = new NameAndPassword("", "");
            var newsFeedViewModel = MakeNewsFeedViewModel(fakeNavigation, nameAndPassword);

            newsFeedViewModel.GoToPublicationPageCommand.Execute(null);

            fakeNavigation.Received(1).GoToPage<PublicationPage>(nameAndPassword);
        }

        [Test]
        public void Constructor_NormalConstructor_UpdatesPosts()
        {
            var newsFeedViewModel = MakeNewsFeedViewModel();

            Assert.AreEqual(1, newsFeedViewModel.Posts.Count);
        }
    }
}