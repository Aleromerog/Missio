using System;
using Mission.Model.Services;
using NSubstitute;
using NUnit.Framework;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class NewsFeedViewModelTests
    {
        private NewsFeedViewModel _newsFeedViewModel;
        private INewsFeedPostsUpdater _newsFeedPostsUpdater;
        private IOnUserLoggedIn _onUserLoggedIn;
        private IGoToView _goToView;

        [SetUp]
        public void SetUp()
        {
            _newsFeedPostsUpdater = Substitute.For<INewsFeedPostsUpdater>();
            _onUserLoggedIn = Substitute.For<IOnUserLoggedIn>();
            _goToView = Substitute.For<IGoToView>();
            _newsFeedViewModel = new NewsFeedViewModel(_newsFeedPostsUpdater, _onUserLoggedIn, _goToView);
        }

        [Test]
        public void OnUserLoggedIn_GivenUser_UpdatesPosts()
        {
            //Arrange
            //Act
            _onUserLoggedIn.OnUserLoggedIn += Raise.Event<Action>();
            //Assert
            _newsFeedPostsUpdater.Received(1).UpdatePosts(_newsFeedViewModel.Posts);
        }

        [Test]
        public void UpdatePosts_GivenPosts_UpdatesPosts()
        {
            //Arrange
            //Act
            _newsFeedViewModel.UpdatePosts();
            //Assert
            _newsFeedPostsUpdater.Received(1).UpdatePosts(_newsFeedViewModel.Posts);
        }

        [Test]
        public void UpdatePosts_IsRefreshingSetToTrue_SetsIsRefreshingToFalse()
        {
            //Arrange
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
}
}