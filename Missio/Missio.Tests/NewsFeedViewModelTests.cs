using System.Collections.ObjectModel;
using System.Linq;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private NewsFeedViewModel NewsFeedViewModel;
        private INewsFeedPostsUpdater FakeNewsFeedPostsUpdater;

        [SetUp]
        public void SetUp()
        {
            FakeNewsFeedPostsUpdater = Substitute.For<INewsFeedPostsUpdater>();
            NewsFeedViewModel = new NewsFeedViewModel(FakeNewsFeedPostsUpdater);
        }

        private static object[] GetUsers()
        {
            return FakeUserValidator.ValidUsers.Cast<object>().ToArray();
        }

        [Test]
        [TestCaseSource(nameof(GetUsers))]
        public void UpdatePosts_GivenPosts_UpdatesPosts(User user)
        {
            //Arrange
            var fakePost = new NewsFeedPost();
            FakeNewsFeedPostsUpdater.When(x => x.UpdatePosts(Arg.Any<ObservableCollection<NewsFeedPost>>()))
                .Do(x => x.Arg<ObservableCollection<NewsFeedPost>>().Add(fakePost));
            //Act
            NewsFeedViewModel.UpdatePosts();
            //Assert
            Assert.Contains(fakePost, NewsFeedViewModel.Posts);
        }
    }
}