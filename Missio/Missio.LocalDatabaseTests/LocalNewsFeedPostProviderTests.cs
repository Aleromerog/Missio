using System.Collections.Generic;
using Missio.LocalDatabase;
using Missio.LogIn;
using Missio.NewsFeed;
using Missio.NewsFeedTests;
using Missio.Users;
using NSubstitute;
using NUnit.Framework;

namespace Missio.LocalDatabaseTests
{
    [TestFixture]
    public class LocalNewsFeedPostProviderTests
    {
        private LocalNewsFeedPostDatabase _localNewsFeedPostDatabase;
        private IGetLoggedInUser _fakeGetLoggedInUser;

        [SetUp]
        public void SetUp()
        {
            _fakeGetLoggedInUser = Substitute.For<IGetLoggedInUser>();
            _localNewsFeedPostDatabase = new LocalNewsFeedPostDatabase(_fakeGetLoggedInUser);
            _fakeGetLoggedInUser.LoggedInUser.Returns(LocalUserDatabase.ValidUsers[0]);
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.ExtraPosts))]
        public void SetUserPosts_GivenPosts_SetsUserPosts(List<NewsFeedPost> newPosts)
        {
            //Arrange
            
            //Act
            _localNewsFeedPostDatabase.SetMostRecentPosts(newPosts);
            //Assert
            Assert.That(_localNewsFeedPostDatabase.GetMostRecentPostsInOrder(), Is.EquivalentTo(newPosts));
        }

        [Test]
        public void GetMostRecentPostsInOrder_NonExistingUser_ReturnsEmptyList()
        {
            //Arrange
            _fakeGetLoggedInUser.LoggedInUser.Returns(new User("", ""));
            //Act
            var posts = _localNewsFeedPostDatabase.GetMostRecentPostsInOrder();
            //Assert
            Assert.AreEqual(0, posts.Count);
        }

        [Test]
        public void GetMostRecentPostsInOrder_ValidUser_ReturnsPostsInOrder()
        {
            //Arrange
            _fakeGetLoggedInUser.LoggedInUser.Returns(LocalUserDatabase.ValidUsers[0]);
            //Act
            var posts = _localNewsFeedPostDatabase.GetMostRecentPostsInOrder();
            //Assert
            Assert.AreEqual(3, posts.Count);
            Assert.IsAssignableFrom<StickyPost>(posts[0]);
        }

        [Test]
        public void PublishPost_GivenPost_AddsPost()
        {
            //Arrange
            var post = new TextOnlyPost("Some user", "The content of the post");
            //Act
            _localNewsFeedPostDatabase.PublishPost(post);
            //Assert
            Assert.Contains(post, _localNewsFeedPostDatabase.GetMostRecentPostsInOrder());
        }
    }
}