using System.Collections.Generic;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class LocalNewsFeedPostProviderTests
    {
        private LocalNewsFeedPostDatabase _localNewsFeedPostDatabase;
        private IGetLoggedInUser fakeGetLoggedInUser;

        [SetUp]
        public void SetUp()
        {
            fakeGetLoggedInUser = Substitute.For<IGetLoggedInUser>();
            _localNewsFeedPostDatabase = new LocalNewsFeedPostDatabase(fakeGetLoggedInUser);
            fakeGetLoggedInUser.LoggedInUser.Returns(LocalUserDatabase.ValidUsers[0]);
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.extraPosts))]
        public void SetUserPosts_GivenPosts_SetsUserPosts(List<NewsFeedPost> newPosts)
        {
            //Arrange
            
            //Act
            _localNewsFeedPostDatabase.SetMostRecentPosts(newPosts);
            //Assert
            Assert.That(_localNewsFeedPostDatabase.GetMostRecentPosts(), Is.EquivalentTo(newPosts));
        }

        [Test]
        public void GetMostRecentPosts_NonExistingUser_ReturnsEmptyList()
        {
            //Arrange
            fakeGetLoggedInUser.LoggedInUser.Returns(new User("", ""));
            //Act
            var posts = _localNewsFeedPostDatabase.GetMostRecentPosts();
            //Assert
            Assert.AreEqual(0, posts.Count);
        }

        [Test]
        public void PublishPost_GivenPost_AddsPost()
        {
            //Arrange
            var post = new TextOnlyPost("Some user", "The content of the post");
            //Act
            _localNewsFeedPostDatabase.PublishPost(post);
            //Assert
            Assert.Contains(post, _localNewsFeedPostDatabase.GetMostRecentPosts());
        }
    }
}