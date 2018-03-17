using System.Collections.Generic;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NUnit.Framework;

namespace Missio.Tests
{
    [TestFixture]
    public class LocalNewsFeedPostProviderTests
    {
        private LocalNewsFeedPostProvider LocalNewsFeedPostProvider;

        private static object[] GetPostsToSet()
        {
            return new object[]
            {
                new[]
                {
                    ExtraNewsFeedPosts.extraPosts[0],
                    new User("Jorge Romero", "Pass")

                },
                new[]
                {
                    ExtraNewsFeedPosts.extraPosts[1],
                    new User("Francisco Greco", "Pass")
                }
            };
        }

        [SetUp]
        public void SetUp()
        {
            LocalNewsFeedPostProvider = new LocalNewsFeedPostProvider();
        }

        [Test]
        [TestCaseSource(nameof(GetPostsToSet))]
        public void SetUserPosts_GivenPosts_SetsUserPosts(List<NewsFeedPost> newPosts, User user)
        {
            //Arrange

            //Act
            LocalNewsFeedPostProvider.SetMostRecentPosts(user, newPosts);
            //Assert
            Assert.That(LocalNewsFeedPostProvider.GetMostRecentPosts(user), Is.EquivalentTo(newPosts));
        }
    }
}