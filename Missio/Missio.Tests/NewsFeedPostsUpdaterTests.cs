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
        private IGetMostRecentPosts NewsFeedPostProvider;

        [SetUp]
        public void SetUp()
        {
            NewsFeedPostProvider = Substitute.For<IGetMostRecentPosts>();
            _newsFeedPostProvider = new NewsFeedPostsUpdater(NewsFeedPostProvider);
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.extraPosts))]
        public void UpdatePosts_GivenCollection_ClearsCollectionAndAddsPosts(List<NewsFeedPost> postsToAdd)
        {
            //Arrange
            var currentPosts = new ObservableCollection<NewsFeedPost>();
            NewsFeedPostProvider.GetMostRecentPosts().Returns(postsToAdd);
            //Act
            _newsFeedPostProvider.UpdatePosts(currentPosts);
            //Assert
            Assert.That(currentPosts, Is.EquivalentTo(postsToAdd));
        }
    }
}