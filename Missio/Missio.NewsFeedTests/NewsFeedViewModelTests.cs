using System;
using System.Collections.Generic;
using Missio.LogIn;
using Missio.Navigation;
using Missio.NewsFeed;
using NSubstitute;
using NUnit.Framework;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private NewsFeedViewModel _newsFeedViewModel;
        private IGetMostRecentPosts _fakeGetMostRecentPosts;
        private IOnUserLoggedIn _onUserLoggedIn;
        private IGoToView _goToView;

        [SetUp]
        public void SetUp()
        {
            _fakeGetMostRecentPosts = Substitute.For<IGetMostRecentPosts>();
            _onUserLoggedIn = Substitute.For<IOnUserLoggedIn>();
            _goToView = Substitute.For<IGoToView>();
            _newsFeedViewModel = new NewsFeedViewModel(_fakeGetMostRecentPosts, _onUserLoggedIn, _goToView);
        }

        [Test]
        public void UpdatePosts_IsRefreshingSetToTrue_SetsIsRefreshingToFalse()
        {
            //Arrange
            _fakeGetMostRecentPosts.GetMostRecentPostsInOrder().Returns(new List<IPost>());
            _newsFeedViewModel.IsRefreshing = true;
            //Act
            _newsFeedViewModel.UpdatePosts();
            //Assert
            Assert.IsFalse(_newsFeedViewModel.IsRefreshing);
        }

        [Test]
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            //Arrange
            
            //Act
            _newsFeedViewModel.GoToPublicationPageCommand.Execute(null);
            //Assert
            _goToView.Received(1).GoToView("Publication page");
        }

        [Test]
        [TestCaseSource(typeof(ExtraNewsFeedPosts), nameof(ExtraNewsFeedPosts.ExtraPosts))]
        public void OnUserLoggedIn_UserLoggedIn_UpdatesPosts(List<IPost> postsToAdd)
        {
            //Arrange
            _fakeGetMostRecentPosts.GetMostRecentPostsInOrder().Returns(postsToAdd);
            //Act
            _onUserLoggedIn.OnUserLoggedIn += Raise.Event<Action>();
            //Assert
            Assert.That(_newsFeedViewModel.Posts, Is.EquivalentTo(postsToAdd));
        }
    }
}