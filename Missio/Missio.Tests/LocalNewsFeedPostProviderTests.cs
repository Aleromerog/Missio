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
        private LocalNewsFeedPostProvider LocalNewsFeedPostProvider;
        private IGetLoggedInUser fakeGetLoggedInUser;

        [SetUp]
        public void SetUp()
        {
            fakeGetLoggedInUser = Substitute.For<IGetLoggedInUser>();
            LocalNewsFeedPostProvider = new LocalNewsFeedPostProvider(fakeGetLoggedInUser);
            fakeGetLoggedInUser.LoggedInUser.Returns(LocalUserDatabase.ValidUsers[0]);
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.extraPosts))]
        public void SetUserPosts_GivenPosts_SetsUserPosts(List<NewsFeedPost> newPosts)
        {
            //Arrange

            //Act
            LocalNewsFeedPostProvider.SetMostRecentPosts(newPosts);
            //Assert
            Assert.That(LocalNewsFeedPostProvider.GetMostRecentPosts(), Is.EquivalentTo(newPosts));
        }

        [Test]
        public void PublishPost_GivenPost_AddsPost()
        {
            //Arrange
            var post = new TextOnlyPost("Some user", "The content of the post");
            //Act
            LocalNewsFeedPostProvider.PublishPost(post);
            //Assert
            Assert.Contains(post, LocalNewsFeedPostProvider.GetMostRecentPosts());
        }
    }
}