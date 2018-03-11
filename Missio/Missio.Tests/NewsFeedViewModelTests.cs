using System;
using System.Collections.ObjectModel;
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
        private IOnUserLoggedIn OnUserLoggedIn;

        [SetUp]
        public void SetUp()
        {
            FakeNewsFeedPostsUpdater = Substitute.For<INewsFeedPostsUpdater>();
            OnUserLoggedIn = Substitute.For<IOnUserLoggedIn>();
            NewsFeedViewModel = new NewsFeedViewModel(FakeNewsFeedPostsUpdater, OnUserLoggedIn);
        }

        [Test]
        public void OnUserLoggedIn_GivenUser_UpdatesPosts()
        {
            //Arrange
            var fakePost = new NewsFeedPost();
            FakeNewsFeedPostsUpdater.When(x => x.UpdatePosts(Arg.Any<ObservableCollection<NewsFeedPost>>()))
                .Do(x => x.Arg<ObservableCollection<NewsFeedPost>>().Add(fakePost));
            //Act
            OnUserLoggedIn.OnUserLoggedIn+= Raise.Event<Action>();
            //Assert
            Assert.Contains(fakePost, NewsFeedViewModel.Posts);
        }

        [Test]
        public void UpdatePosts_GivenPosts_UpdatesPosts()
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