using System.Collections.ObjectModel;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NUnit.Framework;

namespace Missio.Tests
{
    [TestFixture]
    public class LocalNewsFeedPostProviderTests
    {
        private LocalNewsFeedPostProvider LocalNewsFeedPostProvider;

        private static readonly object[] newsFeedPosts =
        {
            new object[]
            {
                new ObservableCollection<NewsFeedPost> {new TextOnlyPost("Francisco Greco", "Hello there")},
                new User("Jorge Romero", "Pass")
            },
            new object[]
            {
                new ObservableCollection<NewsFeedPost> {new TextOnlyPost("Jorge Romero", "<3")},
                new User("Francisco Greco", "Pass")
            }
        };

        [SetUp]
        public void SetUp()
        {
            LocalNewsFeedPostProvider = new LocalNewsFeedPostProvider();
        }

        [Test]
        [TestCaseSource(nameof(newsFeedPosts))]
        public void SetUserPosts_GivenPosts_SetsUserPosts(ObservableCollection<NewsFeedPost> newPosts, User user)
        {
            //Arrange

            //Act
            LocalNewsFeedPostProvider.SetMostRecentPosts(user, newPosts);
            //Assert
            Assert.That(LocalNewsFeedPostProvider.GetMostRecentPosts(user), Is.EquivalentTo(newPosts));
        }
    }
}