using System;
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
        private INewsFeedPostsUpdater NewsFeedPostsUpdater;
        private IOnUserLoggedIn OnUserLoggedIn;
        private IGoToView _goToView;

        [SetUp]
        public void SetUp()
        {
            NewsFeedPostsUpdater = Substitute.For<INewsFeedPostsUpdater>();
            OnUserLoggedIn = Substitute.For<IOnUserLoggedIn>();
            _goToView = Substitute.For<IGoToView>();
            NewsFeedViewModel = new NewsFeedViewModel(NewsFeedPostsUpdater, OnUserLoggedIn, _goToView);
        }

        [Test]
        public void OnUserLoggedIn_GivenUser_UpdatesPosts()
        {
            //Arrange
            //Act
            OnUserLoggedIn.OnUserLoggedIn += Raise.Event<Action>();
            //Assert
            NewsFeedPostsUpdater.Received(1).UpdatePosts(NewsFeedViewModel.Posts);
        }

        [Test]
        public void UpdatePosts_GivenPosts_UpdatesPosts()
        {
            //Arrange
            //Act
            NewsFeedViewModel.UpdatePosts();
            //Assert
            NewsFeedPostsUpdater.Received(1).UpdatePosts(NewsFeedViewModel.Posts);
        }

        [Test]
        public void GoToPublicationPageCommand_NormalCall_GoesToPublicationPage()
        {
            //Arrange
            
            //Act
            NewsFeedViewModel.GoToPublicationPageCommand.Execute(null);
            //Assert
            _goToView.Received(1).GoToView("Publication page");
    }
}
}