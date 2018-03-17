using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    public class LocalNewsFeedPostsUpdaterTests
    {
        private LocalNewsFeedPostsUpdater LocalNewsFeedPostProvider;
        private IGetLoggedInUser GetLoggedInUser;
        private INewsFeedPostsProvider NewsFeedPostProvider;

        [SetUp]
        public void SetUp()
        {
            GetLoggedInUser = Substitute.For<IGetLoggedInUser>();
            NewsFeedPostProvider = Substitute.For<INewsFeedPostsProvider>();
            LocalNewsFeedPostProvider = new LocalNewsFeedPostsUpdater(GetLoggedInUser, NewsFeedPostProvider);
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
            LocalNewsFeedPostProvider.UpdatePosts(currentPosts);
            //Assert
            Assert.That(currentPosts, Is.EquivalentTo(postsToAdd));
        }
    }

    public class ExtraNewsFeedPosts
    {
        public static readonly object[] extraPosts =
        {
            new List<NewsFeedPost> {new TextOnlyPost("Francisco Greco", "Hello there")},
            new List<NewsFeedPost> {new TextOnlyPost("Jorge Romero", "<3")},
        };
    }

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