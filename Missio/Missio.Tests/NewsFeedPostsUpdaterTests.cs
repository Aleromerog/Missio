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
        private NewsFeedPostsUpdater _newsFeedPostUpdater;
        private IGetMostRecentPosts _newsFeedPostProvider;

        [SetUp]
        public void SetUp()
        {
            _newsFeedPostProvider = Substitute.For<IGetMostRecentPosts>();
            _newsFeedPostUpdater = new NewsFeedPostsUpdater(_newsFeedPostProvider);
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.ExtraPosts))]
        public void UpdatePosts_GivenCollection_ClearsCollectionAndAddsPosts(List<NewsFeedPost> postsToAdd)
        {
            //Arrange
            var currentPosts = new ObservableCollection<NewsFeedPost>();
            _newsFeedPostProvider.GetMostRecentPosts().Returns(postsToAdd);
            //Act
            _newsFeedPostUpdater.UpdatePosts(currentPosts);
            //Assert
            Assert.That(currentPosts, Is.EquivalentTo(postsToAdd));
        }
    }
}