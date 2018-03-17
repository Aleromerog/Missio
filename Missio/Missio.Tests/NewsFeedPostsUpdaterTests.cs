using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    public class NewsFeedPostsUpdaterTests
    {
        private NewsFeedPostsUpdater _newsFeedPostProvider;
        private IGetLoggedInUser GetLoggedInUser;
        private INewsFeedPostsProvider NewsFeedPostProvider;

        [SetUp]
        public void SetUp()
        {
            GetLoggedInUser = Substitute.For<IGetLoggedInUser>();
            NewsFeedPostProvider = Substitute.For<INewsFeedPostsProvider>();
            _newsFeedPostProvider = new NewsFeedPostsUpdater(GetLoggedInUser, NewsFeedPostProvider);
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.extraPosts))]
        public void UpdatePosts_GivenCollection_ClearsCollectionAndAddsPosts(List<NewsFeedPost> postsToAdd)
        {
            //Arrange
            var currentPosts = new ObservableCollection<NewsFeedPost>();
            var currentUser = new User("Some user", "");
            GetLoggedInUser.LoggedInUser.Returns(currentUser);
            NewsFeedPostProvider.GetMostRecentPosts(currentUser).Returns(postsToAdd);
            //Act
            _newsFeedPostProvider.UpdatePosts(currentPosts);
            //Assert
            Assert.That(currentPosts, Is.EquivalentTo(postsToAdd));
        }
    }
}